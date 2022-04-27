using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerCombatStepping
{
    private GameObject hitBox;

    // Rigidbody
    public float stepUpSpeed = 7f;
    public float unshootableTime = 1f;
    private bool shootable = true;

    // Enemy Collision
    private Collider2D[] enemyColliders;
    private ContactFilter2D enemyFilter;
    private LayerMask enemyLayerMask;

    Coroutine coroutine;

    // Event
    public event System.Action onStep;

    public PlayerCombatStepping(GameObject hitBox)
    {
        this.hitBox = hitBox;
        stepUpSpeed = 7f;
        unshootableTime = .5f;

        enemyFilter = new ContactFilter2D();
        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = LayerMask.GetMask("Enemy");
    }

    public void Loop()
    {
        var hitNum = hitBox.GetComponent<CircleCollider2D>().OverlapCollider(enemyFilter, enemyColliders);

        foreach (var enemyCollider in enemyColliders)
        {
            if (enemyCollider != null)
            {
                enemyCollider.GetComponent<Enemy>().Die();

                onStep.Invoke();

                // onStep, 탄창 클래스에서 참조
                //if (!reloaded) Reload();

                // onStep
                //StartCoroutine(SteppingUp());
            }
        }

        // PlayerPhysics
        //if (playerBound) LeapOff(leapSpeed);
    }

    IEnumerator SteppingUp()
    {
        shootable = false;
        yield return new WaitForSeconds(unshootableTime);
        shootable = true;
    }

    public void LeapOff(float stepUpSpeed)
    {
        //rigidbody.velocity = new Vector2(rigidbody.velocity.x, stepUpSpeed);
        //controller.jumping = true;
    }
}
