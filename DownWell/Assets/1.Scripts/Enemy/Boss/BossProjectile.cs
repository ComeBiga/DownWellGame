using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    public int damage = 1;
    public float speed = 3f;
    public float minMoveDistance = 10f;
    protected float moveDistance = 0;
    Vector2 direction;

    ContactFilter2D filter;

    protected virtual void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(1 << 3);
    }

    private void Update()
    {
        TakeDamage();
    }

    public void SetTarget(Transform target)
    {
        direction = (target.position - transform.position).normalized;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void RotateDirection(float angle)
    {
        var rotation = Quaternion.Euler(0, 0, angle);
        direction = (rotation * direction).normalized;
    }

    public void MoveToTarget()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    public void MoveToTargetByTransform()
    {
        StartCoroutine(MoveByTransform());
    }

    IEnumerator MoveByTransform()
    {
        while(true)
        {
            transform.localPosition += new Vector3(direction.x, direction.y) * speed * Time.deltaTime;

            yield return null;
        }
    }

    protected virtual void TakeDamage()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        if(colliders.Count > 0)
        {
            foreach(var collider in colliders)
            {
                if(collider.tag == "Wall")
                {
                    Destroy(this.gameObject);
                    GetComponent<Effector>().Generate("Hit");
                    return;
                }
                if (collider.tag == "Block")
                {
                    Destroy(this.gameObject);
                    GetComponent<Effector>().Generate("Hit");
                    return;
                }
                if (collider.tag == "Player")
                {
                    collider.GetComponent<PlayerCombat>().Damaged(transform);
                    Destroy(this.gameObject);
                    GetComponent<Effector>().Generate("Hit");
                    return;
                }
            }
        }
    }
}
