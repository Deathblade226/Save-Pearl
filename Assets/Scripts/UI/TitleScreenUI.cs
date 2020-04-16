using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] Canvas TitleScreen = null;
    [SerializeField] Canvas FileSelectScreen = null;
    [SerializeField] Canvas OverwritePopup = null;
    [SerializeField] Canvas ProfilePopup = null;
    [SerializeField] GameObject[] outputs = new GameObject[3];

    public FileInfo FileInfo { get; set; }

    private int selectedProfile = -1;

    public int SelectedProfile
    {
        get { return selectedProfile; }
        set
        {
            selectedProfile = value;
            if (value != -1)
            {
                SelectProfile(value);
            }
        }
    }

    private int difficulty = -1;

    public int Difficulty
    {
        get { return difficulty; }
        set
        {
            difficulty = value;
        }
    }

    bool choice = false;
    public bool Choice
    {
        get { return choice; }
        set
        {
            choice = value;
            OverwritePopup.enabled = false;
            if (value)
            {
                StartCoroutine("PromptForProfile");
            }
            else
            {
                LoadGame();
            }
        }
    }

    private string characterName;

    public string CharacterName
    {
        get
        {
            return characterName;
        }
        set
        {
            characterName = value;
        }
    }

    public void Start()
    {
        FileInfo = SaveSystem.LoadObject<FileInfo>("SavePearlFileInfo.info");
        for (int i = 0; i < outputs.Length; i++)
        {
            outputs[i].GetComponentsInChildren<Text>().Where(text => text.name.Contains("Name")).First().text = FileInfo.Names[i];
            string display = "";
            switch (FileInfo.Difficulties[i])
            {
                case 0:
                    display = "Easy";
                    break;
                case 1:
                    display = "Medium";
                    break;
                case 2:
                    display = "Hard";
                    break;
                default:
                    break;
            }
            outputs[i].GetComponentsInChildren<Text>().Where(text => text.name.Contains("Difficulty")).First().text = display;
        }

        TitleScreen.enabled = true;
        FileSelectScreen.enabled = false;
        OverwritePopup.enabled = false;
        ProfilePopup.enabled = false;

    }

    public void ChangeToFileSelect()
    {
        FileSelectScreen.enabled = true;
        TitleScreen.enabled = false;
    }

    public void ChangeToTitle()
    {
        TitleScreen.enabled = true;
        FileSelectScreen.enabled = false;
    }

    public void StartNewGame()
    {
        TitleScreen.enabled = false;
        FileSelectScreen.enabled = false;
        OverwritePopup.enabled = false;
        ProfilePopup.enabled = false;

        Game.Instance.IsPlaying = true;
    }

    public void LoadGame()
    {
        Game.Instance.Data = SaveSystem.LoadObject<DataSaver>($"SavePearlFile{SelectedProfile}GameData.gme");
    }

    public void SelectProfile(int profileNumber)
    {
        if (FileInfo.Names[profileNumber].Equals("New File"))
        {
            StartCoroutine("PromptForProfile");
        }
        else
        {
            StartCoroutine("PromptForOverwrite");
        }
    }

    public void CreateProfile(int profileNumber, int difficulty, string characterName)
    {
        FileInfo.Names[profileNumber] = characterName;
        FileInfo.Difficulties[profileNumber] = difficulty;
        SaveSystem.SaveObject(FileInfo, "SavePearlFileInfo.info");
        DataSaver data = new DataSaver(CharacterName, difficulty);
        SaveSystem.SaveObject(data, $"SavePearlFile{profileNumber + 1}GameData.gme");
        Game.Instance.Data = data;
        StartNewGame();
    }

    public void CancelProfileCreation()
    {
        SelectedProfile = -1;
        Difficulty = -1;

        ProfilePopup.enabled = false;
    }

    public void Instructions()
    {
        //TODO Implement Instructions Popup
    }

    IEnumerator PromptForOverwrite()
    {
        OverwritePopup.enabled = true;
        while (OverwritePopup.enabled)
        {
            yield return null;
        }
        StopCoroutine("PromptForOverwrite");
        yield return null;
    }

    IEnumerator PromptForProfile()
    {
        ProfilePopup.enabled = true;
        while (ProfilePopup.enabled)
        {
            yield return null;
        }
        StopCoroutine("PromptForProfile");
        yield return null;
    }

    public void NameCharacterFromInput(InputField inputField)
    {
        if (inputField.text.Length > 0)
        {
            CharacterName = inputField.text;
        }
        else
        {
            ProfilePopup.GetComponentsInChildren<Text>().Where(text => text.name == "Name Text").First().text = $"Please enter a valid character name.\n(Can't be empty)";
        }
    }

    public void ValidateProfile()
    {
        if (CharacterName.Trim().Length > 0 && difficulty > -1)
        {
            ProfilePopup.enabled = false;
            CreateProfile(SelectedProfile, Difficulty, CharacterName);
        }
    }
}
