using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyActionJump : EnemyAct
{
    public float jumpSpeed = 1f;
    public bool onAnimationEvent = false;

    public UnityEvent onJump;

    public override bool Act(Rigidbody2D rigidbody)
    {
        Debug.Log("Jump");

        if(!onAnimationEvent)
            Jump();

        onJump.Invoke();

        return true;
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpSpeed);
    }
}
