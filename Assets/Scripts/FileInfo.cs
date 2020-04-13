using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileInfo
{
    public string[] names;

    public FileInfo(string[] names)
    {
        this.names = names;
    }

    public FileInfo()
    {
        names = new string[3] { "New File", "New File", "New File" };
    }
}