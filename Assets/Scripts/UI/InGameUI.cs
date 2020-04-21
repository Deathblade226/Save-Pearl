using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] Player player;
    [SerializeField] Canvas PauseMenu = null;
    [SerializeField] Canvas Inventory = null;
    [SerializeField] Canvas OverwritePopup = null;

    bool choice = false;
    public bool Choice
    {
        get { return choice; }
        set
        {
            choice = value;
            OverwritePopup.enabled = false;
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && Game.Instance.IsPlaying && !GetComponentInChildren<InstructionsUI>().IsOpen && !Inventory.enabled)
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
        PauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        PauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenInventory()
    {
        Inventory.enabled = true && isPaused;
    }

    public void CloseInventory()
    {
        Inventory.enabled = false && isPaused;
    }

    public void OpenInstructions()
    {
        if (isPaused)
        {
            GetComponentInChildren<InstructionsUI>().OpenInstructions(PauseMenu);
        }
    }

    public void ReturnToMenu()
    {
        if (isPaused)
        {
            StartCoroutine("PromptForMenuOrQuit", "Menu");
        }
    }

    public void Quit()
    {

        if(isPaused) StartCoroutine("PromptForMenuOrQuit", "Menu");
    }

    IEnumerator PromptForMenuOrQuit(string quitOrMenu)
    {
        OverwritePopup.enabled = true;
        while (OverwritePopup.enabled)
        {
            yield return null;
        }
        StopCoroutine("PromptForMenuOrQuit");
        if (choice)
        {
            if (quitOrMenu == "Menu")
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.None;
                Game.Instance.IsPlaying = false;
                Game.Instance.Data = null;
                Game.Instance.gameObject.GetComponent<SceneManagerObject>().LoadSceneAsyncByName("Title Scene");
            }
            else if (quitOrMenu == "Quit")
            {
                Game.Instance.gameObject.GetComponent<SceneManagerObject>().ExitGame();
            }
        }
        yield return null;
    }
}
