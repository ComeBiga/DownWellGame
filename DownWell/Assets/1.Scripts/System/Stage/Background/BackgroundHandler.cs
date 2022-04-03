using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
    private BackgroundInfo info;

    public BackgroundHandler()
    {

    }

    public void SetInfo(BackgroundInfo info)
    {
        this.info = info;
    }

    public static Sprite GetRandomBase(BackgroundInfo info)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        var baseCount = info._base.Length;

        return info._base[rand.Next(baseCount)];
    }

    public static bool Decorate(BackgroundInfo.BackgroundDeco deco, out Sprite tile)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        if (rand.Next(1000) < deco.prob)
        {
            tile = deco.sprite;
            return true;
        }

        tile = null;
        return false;
    }
}
