using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSelector
{
    protected ObjectSelector next;
    protected int min;
    protected int max;

    protected static Vector3 position;
    protected static Transform parent;
    protected static StageDatabase currentStage;

    public ObjectSelector(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public ObjectSelector(int code)
    {
        this.min = code;
        this.max = code;
    }

    public void SetNext(ObjectSelector next)
    {
        this.next = next;
    }

    public GameObject InstantiateObject(int tileCode, Vector3 _position, Transform _parent, StageDatabase _currentStage)
    {
        position = _position;
        parent = _parent;
        currentStage = _currentStage;

        if (tileCode >= min && tileCode <= max)
        {
            return Select(tileCode);
        }
        else
        {
            return Next(tileCode);
        }
    }

    private GameObject InstantiateObject(int tileCode)
    {
        return InstantiateObject(tileCode, position, parent, currentStage);
    }

    protected abstract GameObject Select(int tileCode);

    protected GameObject Find(int tileCode)
    {
        return currentStage.MapObjects.Find(o => o.GetComponent<Wall>().info.code == tileCode);
    }

    protected GameObject Next(int tileCode)
    {
        if(next != null)
        {
            return next.InstantiateObject(tileCode);
        }
        else
        {
            return null;
        }
    }

    protected GameObject Instantiate(GameObject go)
    {
        if (go != null)
            return GameObject.Instantiate(go, position, Quaternion.identity, parent);
        else
            return null;
    }
}

