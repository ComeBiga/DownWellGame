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
        float h;
#if UNITY_EDITOR
        if (InputManager.instance.mouseClick)
            h = InputManager.instance.horizontal;
        else
            h = Input.GetAxis("Horizontal");
#elif UNITY_ANDROID
        h = InputManager.instance.horizontal;
#endif


        Run(h);

        SpriteFilpX(h);
    }

    void Run(float h)
    {
        anim.SetFloat("Horizontal", h);
    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
    }

    void SpriteFilpX(float h)
    {
        if (h < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        if (h > 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
