using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionBlast : BossAction
{
    public GameObject hitBox;
    public Transform pos;
    [SerializeField] private float distance = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool attackAtCenter = false;

    public override void Take()
    {
        StartCoroutine(IBoxingAttack());   
    }

    // Move position left, right to shoot blast
    IEnumerator IBoxingAttack()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        //var posIndex = rand.Next(-1, 2) * 2;
        var posIndex = 0f;

        if (attackAtCenter)
        {
            posIndex = rand.Next(-1, 2);
            posIndex = (posIndex < 0) ? -distance : posIndex * distance;
        }
        else
        {
            posIndex = (rand.Next(-1, 1) < 0) ? -distance : distance;
        }

        while (true)
        {
            if (Mathf.Approximately(transform.position.x, posIndex))
                break;

            var xPos = Mathf.MoveTowards(transform.position.x, posIndex, Time.deltaTime * speed);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

            yield return null;
        }

        // Animate and key event
        GetComponent<Animator>().SetBool("Attack_2", true);
    }

    void InstantiateAttackBox()
    {
        var boxObject = Instantiate(hitBox, pos);
    }

    public void EndBoxingAttack()
    {
        StartCoroutine(IEndBoxing());
    }

    IEnumerator IEndBoxing()
    {
        GetComponent<Animator>().SetBool("Attack_2", false);

        while (true)
        {
            if (Mathf.Approximately(transform.position.x, 0))
                break;

            var xPos = Mathf.MoveTowards(transform.position.x, 0, Time.deltaTime * 2);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

            yield return null;
        }

        Cut();
    }
}
