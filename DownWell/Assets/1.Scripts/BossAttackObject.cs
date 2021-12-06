using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackObject : MonoBehaviour, IUseObject
{
    public int damage = 10;

    bool active = false;

    void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        if(colliders.Count > 0)
        {
            //Debug.Log("Count");
            foreach (var col in colliders)
            {
                //Debug.Log("Col");
                if (col.transform.tag == "Boss" && active)
                {
                    Debug.Log("Boss");
                    col.GetComponent<Boss>().Damaged(damage);

                    Destroy();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss" && active)
        {
            Debug.Log("Boss");
            collision.GetComponent<Boss>().Damaged(damage);

            Destroy();
        }
    }

    public void Use()
    {
        active = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void Destroy()
    {
        GetComponent<Effector>().Generate("Explode");

        Destroy(this.gameObject);
    }
}
