using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] Canvas[] Instructions = null;
    private int CurrentPage { get; set; }

    public void ChangePage(int pageNumber)
    {
        Instructions[pageNumber].enabled = true;
        Instructions[CurrentPage].enabled = false;
        CurrentPage = pageNumber;
    }

    public void CloseInstructions()
    {
        Instructions[CurrentPage].enabled = false;
    }
}
