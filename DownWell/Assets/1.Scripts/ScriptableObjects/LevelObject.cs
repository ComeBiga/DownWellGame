using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Object", menuName = "LevelObject/Object")]
public class LevelObject : ScriptableObject
{
    public int code;
    public new string name;
    public Sprite sprite;
    public int score;
}
