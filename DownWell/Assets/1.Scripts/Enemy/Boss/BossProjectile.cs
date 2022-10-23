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
    private Camera mainCamera;
    private float width;
    private float height;

    protected virtual void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(1 << 3);

        RotateToTarget(PlayerManager.instance.transform.position);

        mainCamera = Camera.main;
        height = mainCamera.orthographicSize * 2;
        width = height / 16 * 9;
    }

    private void Update()
    {
        TakeDamage();

        var position = transform.position;
        var cameraPos = mainCamera.transform.position;
        if (cameraPos.x - width / 2 > position.x || cameraPos.x + width / 2 < position.x || cameraPos.y + height / 2 < position.y )//|| cameraPos.y - height / 2 > position.y)
            Destroy(this.gameObject);
    }

    public void RotateToTarget(Vector3 target)
    {
        Vector3 vectorToTarget = PlayerManager.instance.playerObject.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
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
        if (angle == 0) return;

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
        var top = Camera.main.transform.position.y - Camera.main.orthographicSize;

        while (true)
        {
            if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize
                || transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize
                || transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * 9 / 16
                || transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * 9 / 16)
                break;

            transform.localPosition += new Vector3(direction.x, direction.y) * speed * Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
    }

    protected virtual void TakeDamage()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        if (colliders.Count > 0)
        {
            foreach (var collider in colliders)
            {
                //if(collider.tag == "Wall")
                //{
                //    Destroy(this.gameObject);
                //    GetComponent<Effector>().Generate("Hit");
                //    return;
                //}
                //if (collider.tag == "Block")
                //{
                //    Destroy(this.gameObject);
                //    GetComponent<Effector>().Generate("Hit");
                //    return;
                //}
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
