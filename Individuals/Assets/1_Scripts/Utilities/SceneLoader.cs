using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public string sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
