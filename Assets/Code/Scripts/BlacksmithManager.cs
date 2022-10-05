using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlacksmithManager : MonoBehaviour
{
    GameObject _message;
    int price = 40;
    public float BuyLevel()
    {
        return 10;
    }
    public void EnableMessage()
    {
        if(Singleton.Instance.Game.CoinAmount >= price)
        {
            _message.SetActive(true);
        }

    }
}
