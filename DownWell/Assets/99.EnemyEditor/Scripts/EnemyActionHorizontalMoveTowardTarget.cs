using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionHorizontalMoveTowardTarget : EnemyAct
{
    [Header("Dash")]
    public float speed = 1f;
    public float dashTime = 1f;
    private int dashDir = 0;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Init()
    {
        base.Init();

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }

    protected override void StartAct()
    {
        Debug.Log("StartAct");
        dashDir = (target.position.x - transform.position.x < 0) ? -1 : 1;
        Debug.Log(dashDir);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashDir, GetComponent<Rigidbody2D>().velocity.y);
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        Debug.Log(GetComponent<Rigidbody2D>().velocity);

        Animation();

        return false;
    }

    void Animation()
    {
        GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
    }
}
