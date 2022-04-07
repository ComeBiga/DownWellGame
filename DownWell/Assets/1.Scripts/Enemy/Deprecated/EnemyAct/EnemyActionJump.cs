using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionJump : EnemyAct
{
    public float horizontalSpeed = 0;
    public float verticalSpeed = 1f;

    protected override void StartAct()
    {
        Jump();
        Debug.Log("JumpStart");
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        return false;
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(EnemyAct.direction * horizontalSpeed, verticalSpeed);
    }
}
