using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel;

    void Start()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }
}
