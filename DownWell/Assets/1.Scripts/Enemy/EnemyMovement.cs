using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform target;
    //public float activeRangeOffset = 3f;

    public float speed = 3f;

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

            GetComponent<Rigidbody2D>().velocity = Vector2.one * direction.normalized * speed;
        }
    }

    //bool CheckTargetRange()
    //{
    //    float height = Camera.main.orthographicSize * 2;
    //    float width = height * (9 / 16);

    //    float h_tarTothis = Mathf.Abs(GameManager.instance.playerPrefab.transform.position.y - transform.position.y);

    //    if (h_tarTothis < height / 2 + activeRangeOffset)
    //        return true;


    //    return false;
    //}
}
