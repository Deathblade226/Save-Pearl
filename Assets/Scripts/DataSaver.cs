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

    private int progress;

    public int Progress
    {
        get { return progress; }
        set { progress = value; }
    }

    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public DataSaver(string name, int difficulty, Player player)
    {
        Name = name;
        Difficulty = difficulty;
        Player = player;
    }

    public DataSaver()
    {
        Name = "New File";
        Difficulty = -1;
        Player = new Player();
    }
}
