using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public List<EnemyAct> enemyActs;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Act();
    }

    void Act()
    {
        StartCoroutine(ActUpdate());
    }

    IEnumerator ActUpdate()
    {
        int index = 0;
        bool actEnd = false;

        while (true)
        {
            if (actEnd)
            {
                
                index++;

                if (index >= enemyActs.Count) index = 0;
            }

            actEnd = enemyActs[index].Act(rigidbody);

            yield return null;
        }
    }
}
