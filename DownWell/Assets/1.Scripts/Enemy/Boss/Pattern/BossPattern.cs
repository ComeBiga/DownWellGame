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
        var action = GetRandomAction();
        //Debug.Log($"In Act() : {action}");
        action.TakeAction();
        current = action;
        //Debug.Log($"current action:{action}");
    }

    public void CancelAction()
    {
        current.CancelTake();
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
