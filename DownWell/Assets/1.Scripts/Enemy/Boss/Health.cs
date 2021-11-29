using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    int max;
    public int Max { get { return max; } }

    int current;
    public int Current { get { return current; } }

    public Health(int max)
    {
        this.max = max;
        current = max;
    }

    int Gain(int amount)
    {
        current += amount;
        current = (current > max) ? max : current;

        return current;
    }

    int lose(int amount)
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
