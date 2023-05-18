using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject gameCanvas, pauseCanvas;
    private bool pausedGame = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if (!pausedGame)
        {
            gameCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
            pausedGame = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        gameCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        pausedGame = false;
        Time.timeScale = 1;
    }
}
