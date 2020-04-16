using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileInfo
{
    private string[] names;
    public string[] Names
    {
        get { return names; }
        set { names = value; }
    }

    private int[] difficulties;

    public int[] Difficulties
    {
        get { return difficulties; }
        set { difficulties = value; }
    }    

    public FileInfo(string[] names, int[] difficulties)
    {
        Names = names;
        Difficulties = difficulties;
    }

    public FileInfo()
    {
        Difficulties = new int[3] { -1,-1,-1 };
        Names = new string[3] { "New File", "New File", "New File" };
    }
}