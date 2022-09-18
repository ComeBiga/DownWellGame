using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public Text coinTxt;
    public TextMeshProUGUI coinTMP;
    public Text gameover_coin;
    public TextMeshProUGUI gameover_coinTMP;

    public int coin = 0;

    public int Current { get { return coin; } }

    void Update()
    {
        coinTxt.text = coin.ToString();
        coinTMP.text = coin.ToString();
        gameover_coin.text = coinTxt.text;
        gameover_coinTMP.text = coinTMP.text;
    }

    public void Gain(int amount = 1)
    {
        coin += amount;
    }
}
