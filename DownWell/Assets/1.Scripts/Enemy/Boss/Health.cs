using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    int max;
    public int Max { get { return max; } }

    [SerializeField]int current;
    public int Current { get { return current; } }

    public Health(int max)
    {
        this.max = max;
        current = max;
    }

    public int Gain(int amount)
    {
        current += amount;
        current = (current > max) ? max : current;

        return current;
    }

    public int Lose(int amount)
    {
        current -= amount;
        current = (current < 0) ? 0 : current;

        return current;
    }

    /// <summary>
    /// ���� ü�� ����
    /// </summary>
    /// <returns>ü�� ������� ��ȯ</returns>
    public int CurrentRatio()
    {
        return (int)(current / max) * 100;
    }
}
