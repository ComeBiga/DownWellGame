using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new string name = "";
    public int num;

    private ContactFilter2D filter;


    private void Start()
    {
        filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.useTriggers = true;
        filter.layerMask = LayerMask.GetMask("Boss");
    }

    private void Update()
    {
        var colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(filter, colliders);

        foreach (var collider in colliders)
        {
            //Debug.Log("Collide");
            if (collider != null && collider.tag == "Boss")
            {
                //Debug.Log("Collide Boss");
                GetComponent<PlayerCombat>().Damaged(collider.transform);

                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "StageEnd")
            {
                // StageEnd
                //GameManager.instance.StageEnd();
                //BossStageManager.instance.StartBossStage();
                //Destroy(collision.gameObject);
            }

            if (collision.tag == "Boss")
            {
                //GetComponent<PlayerCombat>().Damaged(collision.transform);
            }

            //if (collision.tag == "UpperBoss")
            //{
            //    if (GetComponent<PlayerPhysics>().Grounded)
            //    {
            //        GetComponent<PlayerCombat>().Damaged(collision.transform, GetComponent<PlayerHealth>().CurrentHealth);
            //        //GetComponent<PlayerHealth>().Die();
            //    }
            //    else
            //    {
            //        GetComponent<PlayerCombat>().Damaged(collision.transform);
            //    }
            //}
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Boss")
            {
                //GetComponent<PlayerCombat>().Damaged(collision.transform);
            }
        }
    }
}
