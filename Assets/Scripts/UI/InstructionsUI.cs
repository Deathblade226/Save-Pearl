using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] Canvas Instructions = null;

    public void ChangePage(int pageNumber)
    {
        Instructions.enabled = true;
    }

    public void CloseInstructions()
    {
        Instructions.enabled = false;
    }
}
