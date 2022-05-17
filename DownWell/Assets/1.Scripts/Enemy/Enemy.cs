using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LevelObject info;
    public int health = 10;
    public bool invincible = false;

    [Header("DropItems")]
    [SerializeField] private ItemDrop itemDropper;
    public List<GameObject> dropItems;

    Collider2D[] colliders;
    ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new Collider2D[3];
        filter = new ContactFilter2D();
        filter.layerMask = 1 << 3;
        filter.useLayerMask = false;

        // ItemDrop Init
        itemDropper.SetItem(dropItems);
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

        foreach (var collider in colliders)
        {
            if (collider != null && collider.tag == "Player")
            {
                if (!collider.GetComponent<PlayerCombat>().IsInvincible)
                    collider.GetComponent<PlayerCombat>().Damaged(transform);

                return;
            }
        }
    }

    public virtual void Damaged(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    public virtual bool Stepped()
    {
        Die();
        return true;
    }

    public void Die()
    {
        if (invincible) return;

        // Item Drop
        itemDropper.Random(transform.position);

        // Score
        UICollector.Instance.score.Add(info.score);

        // Effect
        if (GetComponent<Effector>() != null) GetComponent<Effector>().Generate("Die");

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_1");
        
        // Destroy
        Destroy(this.gameObject);
    }
}
