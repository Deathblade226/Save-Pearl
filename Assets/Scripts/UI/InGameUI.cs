using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private bool isPaused;
    //[SerializeField] Player player;
    [SerializeField] Canvas PauseMenu;
    [SerializeField]

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

    }    
    
    public void CloseInventory()
    {

    }

    public void OpenInstructions()
    {

    }

    public void ReturnToMenu()
    {

    }
}
