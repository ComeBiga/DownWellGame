using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepping : MonoBehaviour
{
    public GameObject hitBox;
    public LayerMask enemyLayerMask;
    public float leapSpeed = 7f;
    ContactFilter2D enemyFilter;
    Collider2D[] enemyColliders;

    private void Start()
    {
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayerMask);

        enemyColliders = new Collider2D[3];
    }

    private void Update()
    {
        StepOn();
    }

    void StepOn()
    {
        var hitNum = hitBox.GetComponent<CircleCollider2D>().OverlapCollider(enemyFilter, enemyColliders);

        bool playerBound = false;

        foreach (var enemyCollider in enemyColliders)
        {
            //Debug.Log(enemyCollider);

            if (enemyCollider != null)
            {
                playerBound = true;
                enemyCollider.GetComponent<Enemy>().Die();
            }
        }

        if (playerBound) GetComponent<PlayerController>().LeapOff(leapSpeed);
    }
}
