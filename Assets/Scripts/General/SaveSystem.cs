using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    public static void SaveObject<T>(T saveableObject, string fileName = "File.txt")
    {
        if (typeof(T).IsSerializable)
        {
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

    public static T LoadObject<T>(string fileName = "File.txt")
    {
        if (typeof(T).IsSerializable)
        {
            string path = $"{Application.persistentDataPath}\\{fileName}";
            FileStream stream = new FileStream(path, FileMode.Open);
            object loadedObject = binaryFormatter.Deserialize(stream);
            stream.Close();
            T castedObject;
            if (loadedObject is T)
            {
                castedObject = (T)loadedObject;
                return castedObject;
            }
            else
            {
                Debug.LogError($"Object of type {typeof(T)} is not the same as {loadedObject.GetType()}");
                return default(T);
            }
        }
        else
        {
            Debug.LogError($"Object of type {typeof(T)} is not Serializable");
            return default(T);
        }
    }
}
