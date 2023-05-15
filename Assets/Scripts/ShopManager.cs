using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private bool hpBoostEnabled, speedBoostEnabled, cdBoostEnabled;
    public GameManager _gameManager;

    public void buyHPBoost()
    {
        if (hpBoostEnabled && (PlayerPrefs.GetFloat("Coins",0) >= 400f ))
        {
            _gameManager.takeCoins(400);
            PlayerPrefs.SetFloat("HP", 150);
            hpBoostEnabled = false;
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
    
    public void buySpeedBoost()
    {
        if (speedBoostEnabled && (PlayerPrefs.GetFloat("Coins",0) >= 500f ))
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
        if (cdBoostEnabled && (PlayerPrefs.GetFloat("Coins",0) >= 750f ))
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
