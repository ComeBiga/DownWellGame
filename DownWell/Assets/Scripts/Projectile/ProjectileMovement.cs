using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 5f;

    public float moveDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaDistance = Vector3.down * speed * Time.deltaTime;
        transform.position += deltaDistance;

        moveDistance += Mathf.Abs(deltaDistance.y);
    }
}
