using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    public float leapSpeed;
    public float knuckBackSpeed;

    public float invincibleTime;

    bool isInvincible = false;
    public bool IsInvincible { get; }

    public void Damaged(Enemy enemy)
    {
        if (isInvincible) return;

        Debug.Log("Player Damaged");

        if(!isInvincible) GetComponent<PlayerHealth>().LoseHealth();

        GetComponent<PlayerController>().LeapOff(leapSpeed);

        Vector3 knuckbackDir = transform.position - enemy.transform.position;
        int direction = knuckbackDir.x > 0 ? 1 : -1;
        //Debug.Log(direction);
        GetComponent<PlayerController>().KnuckBack(knuckBackSpeed, direction);


        StartCoroutine(BecomeInvincible());
        Camera.main.GetComponent<CameraShake>().Shake();
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .3f);
        //GetComponent<PlayerStepping>().hitBox.GetComponent<CircleCollider2D>().enabled = false;

        //yield return new WaitForSeconds(invincibleTime / 2);

        //GetComponent<PlayerStepping>().hitBox.GetComponent<CircleCollider2D>().enabled = true;

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

    }
}
