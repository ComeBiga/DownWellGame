using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [HideInInspector] public bool shootable = false;
    public float shotRebound;

    public abstract void Attack();

    public abstract void Effect();

    public virtual void Init() { }

    public virtual bool IsShootable()
    {
        return shootable;
    }
}
