using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BossPattern
{
    public List<BossAction> actions;
    BossAction current;

    public void Act()
    {
        //Debug.Log("In Act()");
        var action = GetRandomAction();
        action.Take();
    }

    BossAction GetRandomAction()
    {
        string seed = (Random.value + Time.time).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        //Debug.Log("Before Random");
        var rn = rand.Next(actions.Count);
        //Debug.Log("After Random");

        //Debug.Log(rn);
        return actions[rn];
    }
}
