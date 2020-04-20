using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    public DataSaver Data { get; set; }
    public SceneManagerObject SceneManager { get; set; }
    public bool IsPlaying { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            SceneManager = GetComponent<SceneManagerObject>();
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

        FindObjectsOfType<Canvas>().ToList().ForEach(canvas =>
        {
            canvas.enabled = canvas.name == "Title Screen";
        });
    }
}
