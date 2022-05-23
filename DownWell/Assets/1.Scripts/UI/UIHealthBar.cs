using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public List<Image> fills;

    private PlayerHealth playerHP;
    private int lastHP;

    private void Start()
    {

    }

    public void Init()
    {
        playerHP = PlayerManager.instance.playerObject.GetComponent<PlayerHealth>();
        lastHP = playerHP.CurrentHealth;

        for(int i = 0; i < fills.Count; i++)
            fills[i].color = Color.white;
    }

    public void OnChange()
    {
        if(playerHP.CurrentHealth > lastHP)
        {
            Increase();
        }
        else if(playerHP.CurrentHealth < lastHP)
        {
            Decrease();
        }
        else
        {
            return;
        }
    }

    public void Increase(int amount = 1)
    {
        var currentHP = playerHP.CurrentHealth;

        fills[lastHP].color = Color.white;

        lastHP = currentHP;
    }

    public void Decrease(int amount = 1)
    {
        var currentHP = playerHP.CurrentHealth;

        fills[currentHP].color = Color.clear;

        lastHP = currentHP;
    }
}
