using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] Canvas[] Instructions = null;
    private Canvas PreviousScreen = null;
    private int CurrentPage { get; set; } = -1;

    public void OpenInstructions(Canvas previousScreen)
    {
        PreviousScreen = previousScreen;
        Instructions[0].enabled = true;
        PreviousScreen.enabled = false;
    }

    public void ChangePage(int pageNumber)
    {
        Instructions[pageNumber].enabled = true;
        Instructions[CurrentPage].enabled = false;
        CurrentPage = pageNumber;
    }

    public void CloseInstructions()
    {
        foreach(Canvas canvas in Instructions)
        {
            canvas.enabled = false;
        }
    }
}
