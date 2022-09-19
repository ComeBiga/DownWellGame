 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class ObjectSelector
{
    protected ObjectSelector next;
    protected int min;
    protected int max;
    protected GameObject[] objects;

    protected static Tilemap tm_Wall;
    protected static TilemapRenderer tmr_Wall;
    protected static Vector3 position;
    protected static Transform parent;
    protected static StageDatabase currentStage;

    public ObjectSelector(int min, int max, params GameObject[] objects)
    {
        this.min = min;
        this.max = max;
        this.objects = objects;
    }

    public ObjectSelector(int code, params GameObject[] objects)
    {
        this.min = code;
        this.max = code;
        this.objects = objects;
    }

    public void SetNext(ObjectSelector next)
    {
        this.next = next;
    }

    public void SetTileMap(Tilemap wall, TilemapRenderer renderer)
    {
        tm_Wall = wall;
        tmr_Wall = renderer;
    }

    public void SetObject(params GameObject[] objects)
    {
        this.objects = objects;
    }
    
    public void SetObject(List<GameObject> objects)
    {
        this.objects = objects.ToArray();
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

