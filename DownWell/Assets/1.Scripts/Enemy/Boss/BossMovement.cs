using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField] private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        rigidbody.velocity = new Vector3(0, -speed, 0);

        //StartCoroutine(EMove());
    }

    private IEnumerator EMove()
    {
        while(true)
        {

            yield return null;
        }
    }
}
