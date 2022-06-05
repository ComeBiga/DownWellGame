using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Text coinTxt;
    public Text gameover_coin;

    public int coin = 0;

    public int Current { get { return coin; } }

    void Update()
    {
        coinTxt.text = coin.ToString();
        gameover_coin.text = coinTxt.text;
    }

    public void Gain(int amount = 1)
    {
        coin += amount;
    }
}
