using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LevelObject info;
    public int health = 10;
    public float speed = 1f;
    [Header("DropItems")]
    public List<GameObject> dropItems;
    public List<GameObject> successItem;

    Collider2D[] colliders;

    ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyMovement>().speed = speed;

        colliders = new Collider2D[3];
        filter = new ContactFilter2D();
        filter.layerMask = 1 << 3;
        filter.useLayerMask = false;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        //GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
        
        //Debug.Log(filter.useLayerMask);
        //Debug.Log(LayerMask.LayerToName(filter.layerMask.value));
        var colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(filter, colliders);

        //if (colliders.Count > 0)
        //{
        //    Debug.Log(colliders.Count);
        //    if (!colliders[0].GetComponent<PlayerCombat>().IsInvincible)
        //        colliders[0].GetComponent<PlayerCombat>().Damaged(this);
        //}

        foreach (var collider in colliders)
        {
            if (collider != null && collider.tag == "Player")
            {
                //Debug.Log("TakeDamage");
                if (!collider.GetComponent<PlayerCombat>().IsInvincible)
                    collider.GetComponent<PlayerCombat>().Damaged(this);

                return;
            }
        }
    }

    public void Damaged(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        if (dropItems.Count > 0)
        {
            for (int i = 1; i < dropItems.Count; i++)
            {
                if (dropItems[i].GetComponent<ItemDrop>().setRandomItem())
                    successItem.Add(dropItems[i]);
            }
            if (successItem.Count>0)
            {
                int itemRand = UnityEngine.Random.Range(0, successItem.Count);
                successItem[itemRand].GetComponent<Item>().InstantiateItem(transform.position);
                successItem.Clear();
            }

            string seed = (Time.time + Random.value).ToString();
            System.Random rand = new System.Random(seed.GetHashCode());
            int rdCount = rand.Next(2, 5);
            //for (int i = 0; i < rdCount; i++)
            //    dropItems[0].GetComponent<Item>().InstantiateItem(transform.position);
            dropItems[0].GetComponent<Item>().InstantiateItem(transform.position, rdCount);
        }

        Score.instance.getScore(this.gameObject);
        if (GetComponent<Effector>() != null) GetComponent<Effector>().Generate("Die");
        Destroy(this.gameObject);
    }
}
