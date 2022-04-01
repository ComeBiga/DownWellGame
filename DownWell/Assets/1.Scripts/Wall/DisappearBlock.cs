using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            GetComponent<Block>().Destroy();
    }
}