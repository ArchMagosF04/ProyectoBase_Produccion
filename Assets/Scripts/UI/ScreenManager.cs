using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject levelCompleteMenu;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    [SerializeField] private int nextSceneNumber;

    private bool isPaused=false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
    }

    public void PauseMenuButton()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsynchronously(SceneManager.GetActiveScene().name));
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        string levelName = "MainMenu";
        StartCoroutine(LoadSceneAsynchronously(levelName));
        Debug.Log("Go to menu");
    }

    public void GameOverScreen()
    {
        deathMenu.SetActive(true);
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0;
        levelCompleteMenu.SetActive(true);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        string levelName = "Level " + nextSceneNumber;
        StartCoroutine(LoadSceneAsynchronously(levelName));
        Debug.Log("Next level " + nextSceneNumber);
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
