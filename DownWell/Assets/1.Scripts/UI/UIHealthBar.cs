using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public List<Image> fills;
    [SerializeField] private Sprite[] fillColors;
    [SerializeField] private Color[] colorLevels;
    private int colorIndex;

    private PlayerHealth playerHP;
    private int lastHP;

    public bool useColorLevels = true;

    public void Init()
    {
        playerHP = PlayerManager.instance.playerObject.GetComponent<PlayerHealth>();
        lastHP = playerHP.CurrentHealth;

        colorIndex = CalculateFillColorIndex(playerHP.CurrentHealth) - 1;

        if(!useColorLevels) SetFillColorAll(fillColors[colorIndex]);
        else SetFillColorAll(colorLevels[colorIndex]);
    }

    public bool CanIncrease()
    {
        var _colorIndex = CalculateFillColorIndex(lastHP);
        if(_colorIndex >= colorLevels.Length)
            return false;

        return true;
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

        var fillIndex = lastHP % 3;
        colorIndex = CalculateFillColorIndex(lastHP);
        fills[fillIndex].color = Color.white;
        if(!useColorLevels) fills[fillIndex].sprite = fillColors[colorIndex];
        else fills[fillIndex].color = colorLevels[colorIndex];

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
            {
                if(!useColorLevels) fills[fillIndex + i].sprite = fillColors[colorIndex - 1];
                else fills[fillIndex + i].color = colorLevels[colorIndex - 1];
            }
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
    
    private void SetFillColorAll(Color color)
    {
        foreach(var f in fills)
        {
            f.color = color;
        }
    }
}
