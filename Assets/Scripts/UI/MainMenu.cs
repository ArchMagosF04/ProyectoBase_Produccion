using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    [SerializeField] private Button codexButton;


    private void Awake()
    {
        loadingScreen.SetActive(false);
        codexButton.interactable = false;

        if(PlayerPrefs.GetInt("UnlockedLevel")  == 2 )
        {
            codexButton.interactable=true;
        }
    }

    public void Exit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.DeleteKey("ReachedIndex");
    }

    public void Restart()
    {
        StartCoroutine(LoadSceneAsynchronously(SceneManager.GetActiveScene().name));
    }

    public void ReturnToMenu()
    {
        string levelName = "MainMenu";
        StartCoroutine(LoadSceneAsynchronously(levelName));
    }

    public void CodexScene()
    {
        string levelName = "Codex";
        StartCoroutine (LoadSceneAsynchronously(levelName));
    }

    IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
