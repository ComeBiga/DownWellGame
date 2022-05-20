using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public List<Image> fills;

    private int remain;

    private void Start()
    {
        remain = fills.Count;

        Debug.Log($"remain fill ui : {remain}");
    }

    public void Increase(int amount = 1)
    {
        if (remain >= fills.Count) return;

        fills[++remain].color = Color.white;
    }

    public void Decrease(int amount = 1)
    {
        if (remain <= 0) return;

        fills[--remain].color = Color.clear;
    }
}
