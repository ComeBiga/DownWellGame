using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinCount : MonoBehaviour
{
    public Text coinTxt;
    public Text gameover_coin;

    public int coin = 0;

    private void Start()
    {
        coin = 0;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && coin != 0) coin = 0;
        coinTxt.text = coin.ToString();
        gameover_coin.text = coinTxt.text;
    }

    public void Gain(int amount = 1)
    {
        coin += amount;
    }
}
