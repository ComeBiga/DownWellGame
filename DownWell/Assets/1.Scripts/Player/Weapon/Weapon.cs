using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [HideInInspector] public bool shootable = false;
    public float shotRebound;

    protected GameObject player;

    public abstract void Attack();

    public abstract void Effect();

    public virtual void Init(GameObject player) { this.player = player; }

    public virtual bool IsShootable()
    {
        return shootable;
    }
}
