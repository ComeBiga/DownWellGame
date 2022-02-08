using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionBlast : BossAction
{
    public GameObject hitBox;
    public Transform pos;

    public override void Take()
    {
        StartCoroutine(IBoxingAttack());   
    }

    // Move position left, right to shoot blast
    IEnumerator IBoxingAttack()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        var posIndex = rand.Next(-1, 2) * 2;
        Debug.Log(posIndex);
        float dis = 0f;

        while (true)
        {
            if (Mathf.Approximately(transform.position.x, posIndex))
                break;

            var xPos = Mathf.MoveTowards(transform.position.x, posIndex, Time.deltaTime * 2);
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
