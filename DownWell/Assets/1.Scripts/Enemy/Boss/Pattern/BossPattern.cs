using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BossPattern
{
    public List<BossAction> actions;

    public void Act()
    {
        GetRandomAction().Take();
    }

    BossAction GetRandomAction()
    {
        string seed = (Random.value + Time.time).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        var rn = rand.Next(actions.Count);

        return actions[rn];
    }
}
