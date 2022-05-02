using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Object", menuName = "LevelObject/Object")]
public class LevelObject : ScriptableObject
{
    public int code;
    public new string name;
    public Sprite sprite;
    [SerializeField] private Sprite[] sprites;
    public bool sliced = false;
    public int score;

    public Texture2D GetTexture2D()
    {
        if (sliced)
            return TextureCropper.GetCroppedTexture(sprite);
        else
            return sprite.texture;
    }

    //public Texture2D GetCroppedTexture()
    //{
    //    var ct = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

    //    var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
    //                                     (int)sprite.textureRect.y,
    //                                     (int)sprite.textureRect.width,
    //                                     (int)sprite.textureRect.height);

    //    ct.SetPixels(pixels);
    //    ct.Apply();

    //    return ct;
    //}

    public Sprite GetSprite(int stageNum)
    {
        return (sprites.Length > 0) ? sprites[stageNum] : sprite;
    }
}


