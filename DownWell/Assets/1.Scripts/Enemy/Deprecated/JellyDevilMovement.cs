using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyDevilMovement : EnemyMovement
{
    protected Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        MoveToTarget();

        Animation();
    }

    void Animation()
    {
        var dir = target.position.x - transform.position.x;

        GetComponent<SpriteRenderer>().flipX = (dir < 0) ? true : false;
    }

    protected void MoveToTarget()
    {
        Vector3 direction = target.position - transform.position;
        //transform.position += direction.normalized * speed * Time.deltaTime;

        GetComponent<Rigidbody2D>().velocity = Vector2.one * direction.normalized * speed;
    }
}
