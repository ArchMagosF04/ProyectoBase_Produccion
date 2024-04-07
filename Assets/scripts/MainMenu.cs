using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
       SceneManager.LoadScene("Level 1");
    }

    public void Exit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
