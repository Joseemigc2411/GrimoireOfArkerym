using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private bool hpBoostEnabled=true, speedBoostEnabled=true, cdBoostEnabled=true;
    public GameManager _gameManager;

    public void buyHPBoost()
    {
        if (hpBoostEnabled && (PlayerPrefs.GetInt("monedas") > 400 ))
        {
            Debug.Log("He comprado un boost de vida");
            _gameManager.takeCoins(400);
            PlayerPrefs.SetFloat("Health", 150);
            hpBoostEnabled = false;
        }
        else
        {
            Debug.Log("Not enough coins");
            
        }
    }
    
    public void buySpeedBoost()
    {
        if (speedBoostEnabled && (PlayerPrefs.GetInt("monedas") > 500 ))
        {
            
            _gameManager.takeCoins(500);
            PlayerPrefs.SetFloat("Speed", 1.25f);
            speedBoostEnabled = false;
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
    
    public void buyCDBoost()
    {
        if (cdBoostEnabled && (PlayerPrefs.GetInt("monedas") > 750 ))
        {
            _gameManager.takeCoins(750);
            PlayerPrefs.SetFloat("Cooldown", 0.3f);
            speedBoostEnabled = false;
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
    
    
}
