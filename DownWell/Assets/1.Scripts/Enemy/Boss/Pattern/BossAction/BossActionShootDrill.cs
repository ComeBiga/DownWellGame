using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossActionShootDrill : BossAction
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject warning;


    float[] x_pos = { -5, 5 };
    Vector3[] pos = new Vector3[3];
    GameObject[] shotProjectile = new GameObject[3];
    GameObject[] warn = new GameObject[3];
    public override void Take()
    {
        setDir();
    }

    void setDir()
    {
        var player = PlayerManager.instance.playerObject;
        if (player.GetComponent<PlayerHealth>().CurrentHealth <= 0) return;

        var target = player.transform;

        System.Random random = new System.Random();
        x_pos = x_pos.OrderBy(x => random.Next()).ToArray();

        pos[0] = new Vector3(x_pos[0], target.position.y + 2, target.position.z);
        pos[1] = new Vector3(x_pos[0], target.position.y - 2, target.position.z);
        pos[2] = new Vector3(x_pos[1], target.position.y, target.position.z);

        warningAttack();
    }

    void warningAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            if (pos[i].x > 0)
                warn[i] = Instantiate(warning, new Vector3(pos[i].x - 1f, pos[i].y), Quaternion.identity);
            else if (pos[i].x < 0)
                warn[i] = Instantiate(warning, new Vector3(pos[i].x + 1f, pos[i].y), Quaternion.identity);
        }
        StartCoroutine(ShootDrill());
    }

    IEnumerator ShootDrill()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            Destroy(warn[i]);
        }

        for (int i = 0; i < 3; i++)
            shotProjectile[i] = Instantiate(projectile, pos[i], Quaternion.identity, transform);
       
        Shoot();
        for(int i=0;i<3;i++)
            shotProjectile[i].GetComponent<BossProjectile>().MoveToTargetByTransform();
        
        Cut();    
    }

    void Shoot()
    {
        for (int i = 0; i < 3; i ++)
        { 
            if (shotProjectile[i].transform.position.x==5)
            {
                shotProjectile[i].GetComponent<SpriteRenderer>().flipX = true;
                shotProjectile[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * projectile.GetComponent<BossProjectile>().speed;
            }
            else if (shotProjectile[i].transform.position.x == -5)
            {
                shotProjectile[i].GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * projectile.GetComponent<BossProjectile>().speed;
            } 
        }
    }

}
