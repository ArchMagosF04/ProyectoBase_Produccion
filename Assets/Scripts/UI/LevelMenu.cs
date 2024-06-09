using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public GameObject loadingScreen;
    public Slider loadingBar;

    private void Awake()
    {
        ButtonsToArray();

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (unlockedLevel > 6)
            unlockedLevel = 6;
        Debug.Log("Progress:" + unlockedLevel);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0;i < unlockedLevel; i++) 
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    public void OpenLevel(int levelId)
    {
        string levelName="Level "+ levelId;
        StartCoroutine(LoadSceneAsynchronously(levelName));
    }

    IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            loadingBar.value=operation.progress;
            yield return null;
        }
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for(int i = 0; i < childCount ; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }
    }
}
