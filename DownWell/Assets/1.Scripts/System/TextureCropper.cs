using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCropper
{
    public static Texture2D GetTexture2D(Sprite sprite)
    {
        return TextureCropper.GetCroppedTexture(sprite);
    }

    public static Texture2D GetCroppedTexture(Sprite sprite)
    {
        var ct = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                         (int)sprite.textureRect.y,
                                         (int)sprite.textureRect.width,
                                         (int)sprite.textureRect.height);

        ct.SetPixels(pixels);
        ct.Apply();

        return ct;
    }
}
