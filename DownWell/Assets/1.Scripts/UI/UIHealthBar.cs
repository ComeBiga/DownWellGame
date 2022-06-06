using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public List<Image> fills;
    [SerializeField] private Sprite[] fillColors;
    private int colorIndex;

    private PlayerHealth playerHP;
    private int lastHP;

    public void Init()
    {
        playerHP = PlayerManager.instance.playerObject.GetComponent<PlayerHealth>();
        lastHP = playerHP.CurrentHealth;

        colorIndex = CalculateFillColorIndex(playerHP.CurrentHealth) - 1;

        SetFillColorAll(fillColors[colorIndex]);
    }

    public void OnChange()
    {
        //colorIndex = CalculateFillColorIndex(playerHP.CurrentHealth);

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

        colorIndex = CalculateFillColorIndex(lastHP);
        var fillIndex = lastHP % 3;
        Debug.Log($"fillIndex : {fillIndex}, colorIndex {colorIndex}");
        fills[fillIndex].color = Color.white;
        fills[fillIndex].sprite = fillColors[colorIndex];

        lastHP = currentHP;
    }

    public void Decrease(int amount = 1)
    {
        var currentHP = playerHP.CurrentHealth;

        colorIndex = CalculateFillColorIndex(currentHP);

        var fillIndex = currentHP % 3;

        for (int i = 0; i < lastHP - currentHP; i++)
        {
            if (i >= fills.Count) break;

            if (colorIndex < 1)
                fills[fillIndex + i].color = Color.clear;
            else
                fills[fillIndex + i].sprite = fillColors[colorIndex - 1];
        }

        lastHP = currentHP;
    }

    private int CalculateFillColorIndex(int hpCount)
    {
        return hpCount / 3;
    }

    private void SetFillColorAll(Sprite sprite)
    {
        foreach(var f in fills)
        {
            f.sprite = sprite;
        }
    }
}
