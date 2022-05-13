using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossActionShootDrill : BossAction
{
    [SerializeField] protected GameObject projectile;
   
    float[] x_pos = { -5, 5 };
    GameObject[] shotProjectile = new GameObject[3];
    public override void Take()
    {
        ShootDrill();
        Cut();
    }

    void ShootDrill()
    {
        var player = PlayerManager.instance.playerObject;
        if (player.GetComponent<PlayerHealth>().CurrentHealth <= 0) return;

        var target = player.transform;

        System.Random random = new System.Random();
        x_pos = x_pos.OrderBy(x => random.Next()).ToArray();

        var pos0 = new Vector3(x_pos[0], target.position.y + 2, target.position.z);
        var pos1 = new Vector3(x_pos[0], target.position.y - 2, target.position.z);
        var pos2 = new Vector3(x_pos[1], target.position.y, target.position.z);

        shotProjectile[0] = Instantiate(projectile, pos0, Quaternion.identity, transform);
        shotProjectile[1] = Instantiate(projectile, pos1, Quaternion.identity, transform);
        shotProjectile[2] = Instantiate(projectile, pos2, Quaternion.identity, transform);

        ShootDir();
        for(int i=0;i<3;i++)
            shotProjectile[i].GetComponent<BossProjectile>().MoveToTargetByTransform();
    }

    void ShootDir()
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
