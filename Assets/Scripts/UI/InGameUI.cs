﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private bool isPaused;
    //[SerializeField] Player player;
    [SerializeField] Canvas PauseMenu = null;
    [SerializeField] Canvas Inventory = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenInventory()
    {
        Inventory.enabled = true;
    }

    public void CloseInventory()
    {
        Inventory.enabled = false;
    }

    public void OpenInstructions()
    {
        GetComponentInChildren<InstructionsUI>().OpenInstructions(PauseMenu);
    }

    public void ReturnToMenu()
    {
        Game.Instance.gameObject.GetComponent<SceneManagerObject>().LoadSceneAsyncByName("TitleScene");
    }    
    
    public void Quit()
    {
        Game.Instance.gameObject.GetComponent<SceneManagerObject>().ExitGame();
    }
}
