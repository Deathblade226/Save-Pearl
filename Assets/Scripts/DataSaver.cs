using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSaver
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int difficulty;

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    public DataSaver(string name, int difficulty)
    {
        Name = name;
        Difficulty = difficulty;
    }

    public DataSaver()
    {
        Name = "New File";
        Difficulty = -1;
    }
}
