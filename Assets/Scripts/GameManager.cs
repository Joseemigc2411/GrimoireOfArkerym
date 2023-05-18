using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentCoins;
    public float playerMaxHP = 100, runSpeed = 1f, cooldown = 0.5f;
    
    public TextMeshProUGUI currentCoinsText;
    public bool firstGame = false;
    private int currentCounter;
    
    
   
    void Start()
    {
        if (PlayerPrefs.GetInt("GameCounter", 0) == 0)
        {
            PlayerPrefs.SetInt("GameCounter", 1);
            firstGame = true;
            Debug.Log("variable seteada");
            
            if (firstGame)
            {
                Debug.Log("entro al bucle de inicio de juego");
                PlayerPrefs.SetFloat("Health", playerMaxHP);
                PlayerPrefs.SetFloat("Speed", runSpeed);
                PlayerPrefs.SetFloat("Cooldown", cooldown);
                PlayerPrefs.SetInt("Counter", currentCounter);
                firstGame = false;
            }
        }
        
        
    }

   
    void Update()
    {
        currentCoins = PlayerPrefs.GetInt("monedas", 0);
        currentCoinsText.text = currentCoins.ToString("0");
    }

    #region Coin Management Region
        
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
    

    #endregion

    #region Scene Management Methods

        public void playGame()
        {
            if (PlayerPrefs.GetInt("Counter", 0) == 4)
            {
                PlayerPrefs.SetInt("Counter", 0);
                adScene();
                  
            }
            else
            {
                SceneManager.LoadScene("GameplayScene");
                adCounter(1);
            }

        }
        
        public void mainMenu()
        {
            SceneManager.LoadScene("MenuScene");
            Time.timeScale = 1;
        }

        public void adScene()
        {
            SceneManager.LoadScene("AdScene");
        }
        
        public void shopScene()
        {
            SceneManager.LoadScene("ShopScene");
        }
        
        public void adCounter(int counterSum)
        {
            currentCounter = PlayerPrefs.GetInt("Counter", 0);
            currentCounter += counterSum;
            PlayerPrefs.SetInt("Counter", currentCounter);
            Debug.Log(PlayerPrefs.GetInt("Counter", 0));
        }

    #endregion
}
