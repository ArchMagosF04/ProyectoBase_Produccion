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

    AudioManager audioManager;

    [SerializeField] private GameObject cheatButtons;
    private bool areCheatsEnabled;

    private void Awake()
    {
        loadingScreen.SetActive(false);
        codexButton.interactable = false;

        if(PlayerPrefs.GetInt("UnlockedLevel")  >= 2 )
        {
            codexButton.interactable=true;
        }

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                if (!areCheatsEnabled)
                {
                    cheatButtons.SetActive(true);
                    areCheatsEnabled = true;
                    //cheatButtons.transform.position = new Vector3(cheatButtons.transform.position.x,cheatButtons.transform.position.y + 180f,cheatButtons.transform.position.z);
                }
                else
                {
                    cheatButtons.SetActive(false);
                    areCheatsEnabled = false;
                    //cheatButtons.transform.position = new Vector3(cheatButtons.transform.position.x, cheatButtons.transform.position.y - 180f, cheatButtons.transform.position.z);
                }
            } 
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

    public void GainProgress(int levelIndex)
    {
        PlayerPrefs.SetInt("ReachedIndex", levelIndex + 1);
        PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
        PlayerPrefs.Save();
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
