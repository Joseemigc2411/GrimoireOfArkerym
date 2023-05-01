using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    int currentCoins = 0;

    public TextMeshProUGUI currentCoinsText;

    public bool firstGame = true;

   
    void Start()
    {
        if (firstGame)
        {
            PlayerPrefs.SetInt("Coins", currentCoins);
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
}
