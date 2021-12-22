using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAct : MonoBehaviour
{

    //public bool Update()
    //{
    //    if (!GameManager.instance.CheckTargetRange(transform)) return false;

    //    Act(null);

    //    return false;
    //}

    public abstract bool Act(Rigidbody2D rigidbody);
}
