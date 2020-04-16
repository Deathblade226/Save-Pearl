using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public DataSaver Data { get; set; }
    public static Game Instance { get; private set; }
    public bool IsPlaying { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        if (!File.Exists($"{Application.persistentDataPath}\\SavePearlFileInfo.info"))
        {
            FileInfo info = new FileInfo();
            SaveSystem.SaveObject(info, "SavePearlFileInfo.info");
        }
    }
}
