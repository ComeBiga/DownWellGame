using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effector : MonoBehaviour
{
    public List<Effect> effects;

    public void Generate(string name)
    {
        var fx = effects.Find(f => f.name == name);
        if(fx != null) Instantiate(fx.fx, fx.transform.position + (Vector3)fx.offset, Quaternion.Euler(0, 0, fx.angle));
    }

    public void GenerateInParent(string name)
    {
        var fx = effects.Find(f => f.name == name);
        if (fx != null) Instantiate(fx.fx, fx.transform.position + (Vector3)fx.offset, Quaternion.Euler(0, 0, fx.angle), transform);
    }
}

[System.Serializable]
public class Effect
{
    public string name;
    public Transform transform;
    public GameObject fx;
    public float angle = 0;
    public Vector2 offset = Vector2.zero;
}
