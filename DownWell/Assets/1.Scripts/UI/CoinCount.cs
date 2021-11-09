using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Text coinTxt;

    void Update()
    {
        coinTxt.text = GameManager.instance.coin.ToString();
    }
}
