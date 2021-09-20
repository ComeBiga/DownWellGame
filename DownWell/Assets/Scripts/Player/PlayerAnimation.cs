using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Run(h);

        SpriteFilpX(h);
    }

    void Run(float h)
    {
        anim.SetFloat("Horizontal", h);
    }

    void SpriteFilpX(float h)
    {
        if (h < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        if (h > 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
