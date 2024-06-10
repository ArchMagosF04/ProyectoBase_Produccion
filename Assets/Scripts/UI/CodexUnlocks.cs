using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodexUnlocks : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text[] texts;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            texts[i].enabled = false;
        }

        if(unlockedLevel>=2)
        {
            buttons[0].interactable = true;
            buttons[1].interactable = true;
            buttons[2].interactable = true;
            buttons[8].interactable = true;

            texts[0].enabled = true;
            texts[1].enabled = true;
            texts[2].enabled = true;
            texts[8].enabled = true;
        }
        if (unlockedLevel>=3)
        {
            buttons[3].interactable = true;
            buttons[4].interactable = true;
            buttons[9].interactable = true;

            texts[3].enabled = true;
            texts[4].enabled = true;
            texts[9].enabled = true;
        }
        if(unlockedLevel >= 4)
        {
            buttons[5].interactable = true;

            texts[5].enabled = true;
        }
        if(unlockedLevel>=5)
        {
            buttons[6].interactable = true;
            buttons[10].interactable = true;

            texts[6].enabled = true;
            texts[10].enabled = true;
        }
        if(unlockedLevel>=6)
        {
            buttons[11].interactable = true;

            texts[11].enabled = true;
        }
        if(unlockedLevel>6)
        {
            buttons[7].interactable = true;

            texts[7].enabled = true;
        }
    }
}
