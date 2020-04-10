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
    public Game Instance { get; private set; }
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
        SceneManager.sceneLoaded += newSceneLoaded;

        if (!File.Exists($"{Application.persistentDataPath}\\SavePearlFileInfo.info"))
        {
            FileInfo info = new FileInfo();
            Instance.Save(info, "SavePearlFileInfo.info");
        }
        if (!File.Exists($"{Application.persistentDataPath}\\SavePearlFile1GameData.gme"))
        {
            DataSaver data = new DataSaver();
            Instance.Save(data, "SavePearlFile1GameData.gme");
        }
        if (!File.Exists($"{Application.persistentDataPath}\\SavePearlFile2GameData.gme"))
        {
            DataSaver data = new DataSaver();
            Instance.Save(data, "SavePearlFile2GameData.gme");
        }
        if (!File.Exists($"{Application.persistentDataPath}\\SavePearlFile3GameData.gme"))
        {
            DataSaver data = new DataSaver();
            Instance.Save(data, "SavePearlFile3GameData.gme");
        }
    }

    private void Save<T>(T saveableObject, string fileName)
    {
        if (typeof(T).IsSerializable)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = $"{Application.persistentDataPath}\\{fileName}";
            FileStream stream = new FileStream(path, FileMode.Create);
            binaryFormatter.Serialize(stream, saveableObject);
            stream.Close();
        }
        else
        {
            Debug.LogError($"Object of type {typeof(T)} is not Serializable");
        }
    }

    private void newSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        switch (loadedScene.name)
        {
            case "StartScene":
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Instance.Data = null;
                //Instance.player.GetComponentInChildren<Camera>().clearFlags = CameraClearFlags.SolidColor;
                //Instance.player.GetComponent<FirstPersonAIO>().enabled = false;
                //Instance.player.GetComponent<Rigidbody>().useGravity = false;
                break;
            case "GameScene":
                break;
            default:
                break;
        }
    }
}
