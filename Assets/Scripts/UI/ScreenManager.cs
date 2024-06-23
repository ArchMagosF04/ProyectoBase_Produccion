using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject comboList;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject levelCompleteMenu;
    public GameObject newDiaryEntry;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    [SerializeField] private int nextSceneNumber;
    public bool isLevelComplete;

    public bool isPaused=false;

    public bool isDialogueActive=false;

    AudioManager audioManager;

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

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        dropdown.value = PlayerPrefs.GetInt("GameDificulty", 1);
        DropdownChanged();
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
        comboList.SetActive(false);
        optionsMenu.SetActive(false);
        deathMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
        isLevelComplete=false;
        newDiaryEntry.SetActive(false);
    }

    public void DropdownChanged()
    {
        int index = dropdown.value;
        switch (index)
        {
            case 0:
                PlayerPrefs.SetFloat("PlayerDamageMultiplier", 1.25f);
                PlayerPrefs.SetFloat("EnemyDamageMultiplier", 0.75f);
                break;
            case 1:
                PlayerPrefs.SetFloat("PlayerDamageMultiplier", 1f);
                PlayerPrefs.SetFloat("EnemyDamageMultiplier", 1f);
                break;
            case 2:
                PlayerPrefs.SetFloat("PlayerDamageMultiplier", 1.25f);
                PlayerPrefs.SetFloat("EnemyDamageMultiplier", 1.25f);
                break;
        }
        PlayerPrefs.SetInt("GameDificulty", index);
    }

    public void PauseMenuButton()
    {
        if (isPaused)
        {
            comboList.SetActive(false);
            optionsMenu.SetActive(false);
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

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
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
        Time.timeScale = 0;
        deathMenu.SetActive(true);
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0;
        levelCompleteMenu.SetActive(true);
        isLevelComplete = true;
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        string levelName = "Level " + nextSceneNumber;
        StartCoroutine(LoadSceneAsynchronously(levelName));
        Debug.Log("Next level " + nextSceneNumber);
    }

    public void GoToCodex()
    {
        Time.timeScale = 1;
        string levelName = "Codex";
        StartCoroutine(LoadSceneAsynchronously(levelName));
    }

    public void ButtonSFX()
    {
        audioManager.PlaySFX(audioManager.select);
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
