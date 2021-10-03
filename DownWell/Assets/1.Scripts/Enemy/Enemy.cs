using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LevelObject info;
    public int health = 10;
    Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new Collider2D[3];
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        foreach(var collider in colliders)
        {
            if(collider != null && collider.tag == "Player")
            {
                //Debug.Log("TakeDamage");
                collider.GetComponent<PlayerDamaged>().Damaged(this);
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
        Destroy(this.gameObject);
    }
}
