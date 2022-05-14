using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 5f;

    public float moveDistance = 0;

    private float currentPos;
    private float lastPos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.down * speed;

        lastPos = currentPos = transform.position.y;
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
}
