using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : ObjectSelector
{
    private int ratio;

    public EnemySelector(int code, int ratio) : base(code)
    {
        this.ratio = ratio;
    }

    public EnemySelector(int min, int max, int ratio) : base(min, max)
    {
        this.ratio = ratio;
    }

    protected override GameObject Select(int tileCode)
    {
        if(CatDown.Random.Get().Next(100) < ratio)
        {
            //var obj = StageManager.instance.Current.EnemyObjects.Find(o => o.GetComponent<Enemy>().info.code == tileCode);
            return Instantiate(null);
        }

        return null;
    }
}
