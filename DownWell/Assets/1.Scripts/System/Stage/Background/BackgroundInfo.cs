using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundInfo
{
    public Sprite[] _base;
    public BackgroundDeco[] deco;

    [System.Serializable]
    public struct BackgroundDeco
    {
        public Sprite sprite;
        [Range(0, 100)]
        public int prob;
    }
}