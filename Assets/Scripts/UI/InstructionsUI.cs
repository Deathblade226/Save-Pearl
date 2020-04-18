using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] Canvas[] Instructions = null;
    private int CurrentPage { get; set; } = 0;

    public void ChangePage(int pageNumber)
    {
        Instructions[pageNumber].enabled = true;
    }

    public void CloseInstructions()
    {
        foreach(Canvas canvas in Instructions)
        {
            canvas.enabled = false;
        }
    }
}
