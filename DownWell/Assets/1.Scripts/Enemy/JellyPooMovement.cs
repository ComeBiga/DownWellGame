using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPooMovement : EnemyMovement
{
    [Header("JellyPoo Value")]
    public BoxCollider2D sensorCollider;
    public LayerMask targetLayer;
    ContactFilter2D targetFilter = new ContactFilter2D();

    [Header("Dash")]
    public float dashTime = 1f;
    float timer = 0;

    bool dashing = false;
    int dashDir = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        //collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, targetLayer);
        //collision.CalculateRaySpacing();

        targetFilter.SetLayerMask(targetLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        if (dashing) Dash();
        else SenseTarget();
    }

    void SenseTarget()
    {
        Collider2D[] target = new Collider2D[1];
        var count = sensorCollider.OverlapCollider(targetFilter, target);

        if (count > 0)
        {
            dashDir = (target[0].transform.position.x - transform.position.x < 0) ? -1 : 1;
            dashing = true;
        }
        else
            dashing = false;
    }

    void Dash()
    {
        rigidbody.velocity = new Vector2(speed * dashDir, rigidbody.velocity.y);

        Invoke("CoolDown", dashTime);
    }

    void CoolDown()
    {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        dashing = false;
    }
}
