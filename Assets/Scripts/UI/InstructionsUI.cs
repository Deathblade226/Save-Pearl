using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] Canvas[] Instructions = null;
    private Canvas PreviousScreen = null;
    private int CurrentPage { get; set; } = -1;

    public bool IsOpen { get; set; } = false;

    public void OpenInstructions(Canvas previousScreen)
    {
        IsOpen = true;
        PreviousScreen = previousScreen;
        Instructions[0].enabled = true;
        CurrentPage = 0;
        PreviousScreen.enabled = false;
    }

    public void ChangePage(int pageNumber)
    {
        if (pageNumber != CurrentPage)
        {
            Instructions[pageNumber].enabled = true;
            Instructions[CurrentPage].enabled = false;
            CurrentPage = pageNumber;
        }
    }

    public void CloseInstructions()
    {
        foreach (Canvas canvas in Instructions)
        {
            canvas.enabled = false;
        }
        if (PreviousScreen != null)
        {
            PreviousScreen.enabled = true;
        }
        PreviousScreen = null;
        IsOpen = false;
    }
}
