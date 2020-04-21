using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] Player player;
    [SerializeField] Canvas PauseMenu = null;
    [SerializeField] Canvas Inventory = null;
    [SerializeField] Canvas OverwritePopup = null;
    ScrollView Items = null;
    List<Slider> Stats = null;

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

    private void Start()
    {
        //Items = GetComponentInChildren<ScrollView>();
        //Stats = GetComponentsInChildren<Slider>().Where(Slider => Slider.name.Contains("Stat")).ToList();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && Game.Instance.IsPlaying && !GetComponentInChildren<InstructionsUI>().IsOpen && !Inventory.enabled && !OverwritePopup.enabled)
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
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        PauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        PauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenInventory()
    {
        //foreach(Slider slider in Stats)
        //{
        //    switch(slider.name)
        //    {
        //        case "Health":
        //            slider.value = player.m_health;
        //            break;
        //        case "Strength":
        //            slider.value = player.Strength;
        //            break;
        //        case "Dexerity":
        //            slider.value = player.Dexterity;
        //            break;
        //        case "Jump":
        //            slider.value = player.JumpForce;
        //            break;
        //        case "Speed":
        //            slider.value = player.Speed;
        //            break;
        //        case "Armor":
        //            slider.value = player.m_damageReduction;
        //            break;
        //    }
        //}
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

        if(isPaused) StartCoroutine("PromptForMenuOrQuit", "Quit");
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
                UnityEngine.Cursor.lockState = CursorLockMode.None;
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
