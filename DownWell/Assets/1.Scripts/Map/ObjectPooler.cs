using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour 
{
    public GameObject objectToPool;
    public Transform parent;

    public List<GameObject> pool = new List<GameObject>();

    public GameObject this[int i]
    {
        get{ return pool[i]; }
        set{ pool.Add(value); }
    }

    public void Init(GameObject objectToPool, int count, Transform parent)
    {
        this.objectToPool = objectToPool;
        this.parent = parent;

        for (int i = 0; i < count; i++)
        {
            var newObj = Instantiate(objectToPool, Vector2.zero, Quaternion.identity, parent);
            newObj.SetActive(false);
            pool.Add(newObj);
        }
    }

    public void Pool(GameObject objectToPool)
    {
        pool.Add(objectToPool);
    }

    public void SetUnused(GameObject objectUnusing)
    {
        objectUnusing.SetActive(false);
    }

    public GameObject GetUnusedObject()
    {
        var obj = pool.FirstOrDefault(o => o.activeSelf == false);

        if(obj == null)
        {
            var newObj = Instantiate(objectToPool, parent);
            pool.Add(newObj);
            return newObj;
        }

        obj.SetActive(true);
        return obj;
    }
}
