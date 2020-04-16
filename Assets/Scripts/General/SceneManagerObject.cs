using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    [SerializeField] List<string> sceneNames = null;
    public void LoadSceneAsyncByName(string sceneName)
    {
        if (sceneNames.Contains(sceneName))
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void LoadSceneAsyncByIndex(int index)
    {
        if (sceneNames.Count != 0 && index >= 0 && index < sceneNames.Count)
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        if (sceneNames.Contains(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadSceneByIndex(int index)
    {
        if (sceneNames.Count != 0 && index >= 0 && index < sceneNames.Count)
        {
            SceneManager.LoadScene(index);
        }
    }

    public Scene CurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }
}
