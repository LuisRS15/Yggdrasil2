using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pausa;

    private bool isPaused = false;

    void Start()
    {    
        Pausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        Pausa.SetActive(true);

        Debug.Log("Juego pausado");
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        Pausa.SetActive(false);

        Debug.Log("Juego reanudado");
    }
}