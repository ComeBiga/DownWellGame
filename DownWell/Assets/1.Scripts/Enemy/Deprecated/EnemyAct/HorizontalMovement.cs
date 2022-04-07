using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : EnemyAct
{
    public float speed = 3;

    public float time = 3.0f;

    private float current = 0;


    public override bool Act(Rigidbody2D rigidbody)
    {
        Debug.Log("HorizontalMovement Act() speed : " + speed);

        current += Time.deltaTime;

        if (current > time)
        {
            current = 0;
            return true;
        }

        return false;
    }
}
