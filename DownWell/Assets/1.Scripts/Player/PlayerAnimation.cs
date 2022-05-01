using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerController controller;
    PlayerPhysics physics;

    [SerializeField]
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        physics = GetComponent<PlayerPhysics>();

        physics.OnGrounded += OnGround;
    }

    // Update is called once per frame
    void Update()
    {
        float h = InputManager.instance.Horizontal;

        if (canMove)
        {
            Run(h);
            SpriteFilpX(h);
        }
        Jump();
    }
    void Run(float h)
    {
        anim.SetFloat("Horizontal", h);
    }

    void Jump()
    {
        if (!physics.Grounded) 
        {
            anim.SetBool("Grounded", false);
            anim.SetBool("Jump", true); 
        }
        else
        {
            anim.SetBool("Grounded", true);
            anim.SetBool("Jump", false);
        }
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

    private void OnGround()
    {
        GetComponent<Effector>().Generate("Land");
    }
}
