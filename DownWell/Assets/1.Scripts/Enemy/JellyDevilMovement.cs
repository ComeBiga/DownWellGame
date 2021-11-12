using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyDevilMovement : MonoBehaviour
{
    Transform target;
    //public float activeRangeOffset = 3f;

    public float speed = 3f;
    public float Speed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.CheckTargetRange(transform))
        {
            Vector3 direction = target.position - transform.position;
            //transform.position += direction.normalized * speed * Time.deltaTime;

            GetComponent<Rigidbody2D>().velocity = Vector2.one * direction.normalized * Speed;
        }
    }
}
