using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Text coinTxt;
    public Text gameover_coin;
    void Update()
    {
        coinTxt.text = GameManager.instance.coin.ToString();
        gameover_coin.text = coinTxt.text;
    }
}
