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

    private int[] progresses;

    public int[] Progresses
    {
        get { return progresses; }
        set { progresses = value; }
    }

    public FileInfo(string[] names, int[] difficulties, int[] progresses)
    {
        Names = names;
        Difficulties = difficulties;
        Progresses = progresses;
    }

    public FileInfo()
    {
        Difficulties = new int[3] { -1,-1,-1 };
        Names = new string[3] { "New File", "New File", "New File" };
        Progresses = new int[3] { 0, 0, 0 };
    }
}