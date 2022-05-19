using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeSplashed : MonoBehaviour
{
    Sprite sprite;
    Color color;

    public bool splashed = false;
    [SerializeField] private int ridCount = 5;
    [HideInInspector] public int currentRidCount;

    public bool useSpriteChange = false;
    public Sprite splashedSprite;
    public bool useColorChange = true;
    public Color splashedColor;

    public bool IsSplashed { get { return currentRidCount > 0; } }

    void Start()
    {
        currentRidCount = 0;

        sprite = GetComponent<SpriteRenderer>().sprite;
        color = GetComponent<SpriteRenderer>().color;
    }

    public void Splash()
    {
        splashed = true;
        currentRidCount = ridCount;

        if (useSpriteChange) GetComponent<SpriteRenderer>().sprite = splashedSprite;
        if (useColorChange) GetComponent<SpriteRenderer>().color = splashedColor;
    }

    public void Recover()
    {
        splashed = false;

        if (useSpriteChange) GetComponent<SpriteRenderer>().sprite = sprite;
        if (useColorChange) GetComponent<SpriteRenderer>().color = color;
    }

    public void Rid()
    {
        if (!splashed) return;

        currentRidCount--;

        if (currentRidCount == 0)
            Recover();
    }
}
