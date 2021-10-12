using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector : MonoBehaviour
{
    public List<Effect> effects;

    public void Generate(string name)
    {
        var fx = effects.Find(f => f.name == name);
        Instantiate(fx.fx, fx.transform.position, Quaternion.identity);
    }
}

[System.Serializable]
public class Effect
{
    public string name;
    public Transform transform;
    public GameObject fx;
}
