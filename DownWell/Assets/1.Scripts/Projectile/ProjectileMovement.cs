using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 5f;

    public float moveDistance = 0;

    private float currentPos;
    private float lastPos;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        lastPos = currentPos = transform.position.y;
    }

    public void Init()
    {
        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position.y;
        moveDistance += Mathf.Abs(lastPos - currentPos);

        lastPos = currentPos;

        //Vector3 deltaDistance = Vector3.down * speed * Time.deltaTime;
        //transform.position += deltaDistance;

        //moveDistance += Mathf.Abs(deltaDistance.y);
    }

    public void SetVelocity(Vector3 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
