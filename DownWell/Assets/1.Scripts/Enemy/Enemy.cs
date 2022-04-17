using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LevelObject info;
    public int health = 10;

    //public float speed = 1f;
    [Header("DropItems")]
    public List<GameObject> dropItems;
    private ItemDrop itemDropper;

    Collider2D[] colliders;
    ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<EnemyMovement>().speed = speed;

        colliders = new Collider2D[3];
        filter = new ContactFilter2D();
        filter.layerMask = 1 << 3;
        filter.useLayerMask = false;

        // ItemDrop Init
        itemDropper = new ItemDrop();
        itemDropper.Init(dropItems);

        //dropItems.Sort((A, B) => B.GetComponent<Item>().i_Info.chacePercent.CompareTo(A.GetComponent<Item>().i_Info.chacePercent));
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
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
                    collider.GetComponent<PlayerCombat>().Damaged(transform);

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
        // Item Drop
        itemDropper.Random(transform.position);

        // Score
        //Score.instance.getScore(this.gameObject);
        GameManager.instance.score.Add(info.score);

        // Effect
        if (GetComponent<Effector>() != null) GetComponent<Effector>().Generate("Die");

        // Sound
        //if (SoundManager.instance != null) SoundManager.instance.PlayEffSound("Shoot_1");  //사운드이펙트
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_1");
        
        // Destroy
        Destroy(this.gameObject);
    }
}
