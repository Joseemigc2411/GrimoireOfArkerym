using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    int currentCoins = 0;
    public float playerMaxHP = 100;

    public TextMeshProUGUI currentCoinsText;

    public bool firstGame = true;

   
    void Start()
    {
        if (firstGame)
        {
            PlayerPrefs.SetInt("Coins", currentCoins);
            PlayerPrefs.SetFloat("Health", playerMaxHP);
            firstGame = false;
        }
        
    }

   
    void Update()
    {
        currentCoins = PlayerPrefs.GetInt("monedas", 0);
        currentCoinsText.text = currentCoins.ToString("0");
    }

    public void addCoins(int coinsGiven)
    {
        currentCoins = PlayerPrefs.GetInt("monedas", 0);
        currentCoins += coinsGiven;
        PlayerPrefs.SetInt("monedas", currentCoins);
    }

    public void takeCoins(int coinsTaken)
    {
        currentCoins = PlayerPrefs.GetInt("monedas", 0);
        currentCoins -= coinsTaken;
        PlayerPrefs.SetInt("monedas", currentCoins);
    }
    
    public void playGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
    
    public void mainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void retryCoins()
    {
        if (PlayerPrefs.GetInt("monedas", 0) >= 200) //Si las monedas del jugador no superan las 200, no pasara nada
        {
            takeCoins(200); //En caso de que tenga 200 monedas o mas, se las restamos.
            SceneManager.LoadScene("GameplayScene");
        }
        
    }
    
    public void adScene()
    {
        SceneManager.LoadScene("AdScene");
    }
}
