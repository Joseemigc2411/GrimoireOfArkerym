using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public Scrollbar loadingBar;
    public GameObject leaveButton;
    public float loadCD;
    float loadCurrentCD;

    private void Start()
    {
        leaveButton.SetActive(false);
    }

    void Update()
    {
        loadCurrentCD += Time.deltaTime;
        loadCurrentCD = Mathf.Clamp(loadCurrentCD, 0.0f, loadCD);
        loadingBar.size = loadCurrentCD / loadCD;

        if (Mathf.Approximately(loadingBar.size, 1.0f))
        {
            leaveButton.SetActive(true);
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
    
}
