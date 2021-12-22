using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowTarget : EnemyAct
{
    private Transform target;
    public float speed = 1f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return false;

        MoveToTarget();

        Animation();

        return false;
    }

    void Animation()
    {
        var dir = target.position.x - transform.position.x;

        GetComponent<SpriteRenderer>().flipX = (dir < 0) ? true : false;
    }

    void MoveToTarget()
    {
        Vector3 direction = target.position - transform.position;
        //transform.position += direction.normalized * speed * Time.deltaTime;

        GetComponent<Rigidbody2D>().velocity = Vector2.one * direction.normalized * speed;
    }
}
