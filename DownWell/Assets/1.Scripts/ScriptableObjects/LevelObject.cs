using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Object", menuName = "LevelObject/Object")]
public class LevelObject : ScriptableObject
{
    public int code;
    public new string name;
    public int score;

    [Header("Sprite")]
    public Sprite sprite;
    public bool sliced = false;

    public Texture2D GetTexture2D()
    {
        if (sliced)
            return TextureCropper.GetCroppedTexture(sprite);
        else
            return sprite.texture;
    }
}


