using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{
    InputManager input;
    PlayerPhysics physics;
    PlayerAttack attack;

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float maxFallSpeed = 10f;

    [HideInInspector] public bool cantMove = false;
    [HideInInspector] public bool jumping = false;
    bool shooting = false;

    float normalSpeed;
    float normalJumpSpeed;
    float splashedSpeed;
    float splashedJumpSpeed;

    float hAdditive = 1f;
    float vAdditive = 1f;

    // Start is called before the first frame update
    void Start()
    {
        input = InputManager.instance;
        physics = GetComponent<PlayerPhysics>();
        attack = GetComponent<PlayerAttack>();

        // Splashed
        normalSpeed = speed;
        normalJumpSpeed = jumpSpeed;
        splashedSpeed = speed * .7f;
        splashedJumpSpeed = jumpSpeed * .7f;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        MoveOnSplashed();

        Jump();

        Shoot();
    }

    void Shoot()
    {
        if (GetComponent<PlayerCombatStepping>().ShotLock || cantMove) return;

        if (physics.Grounded)
        {
            jumping = false;
            attack.CurrentWeapon.shootable = false;
            shooting = false;
        }
        else
        {
            attack.CurrentWeapon.shootable = true;
        }

        if (input.GetJumpButtonUp())
        {
            attack.CurrentWeapon.shootable = true;
        }

        if (attack.CurrentWeapon.shootable && input.GetJumpButtonDown())
        {
            shooting = true;
        }
        if (shooting && input.GetJumpButton())
        {
            GetComponent<PlayerAttack>().Shoot();
        }

    }

    public void UseGravity(bool value)
    {
        physics.UseGravity(value);
    }

    void HorizontalMove()
    {
        if (cantMove)
        {
            physics.Move(0);
            return;
        }

        physics.Move(input.Horizontal * hAdditive);

    }

    void Jump()
    {
        if (cantMove) return;

        // Basic Jump
        if(input.GetJumpButtonDown())
        {
            physics.Jump(vAdditive);
        }

        // Short Jump(Jump Canceling)
        if(input.GetJumpButtonUp())
        {
            physics.JumpCancel();
        }
        

    }

    public void MoveOnSplashed()
    {
        if (!BossStageManager.instance.IsBossStage)
        {
            hAdditive = 1f;
            vAdditive = 1f;

            return;
        }
        //if (physics.wallCollision == null) return;

        RaycastHit2D hit;

        if(physics.wallCollision.CheckCollision(CollisionDirection.DOWN, out hit))
        {
            if (hit.transform.GetComponent<BeSplashed>() != null && hit.transform.GetComponent<BeSplashed>().splashed)
            {
                speed = splashedSpeed;
                jumpSpeed = splashedJumpSpeed;
                hAdditive = .7f;
                vAdditive = .7f;
                return;
            }
        }
        else
        {
            speed = normalSpeed;
            jumpSpeed = normalJumpSpeed;
            hAdditive = 1f;
            vAdditive = 1f;
        }

    }
}
