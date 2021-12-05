using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 0;

    public float lifeDistance = 5f;

    private void Update()
    {
        CollisionCheck();

        if (GetComponent<ProjectileMovement>().moveDistance >= lifeDistance)
            Destroy();
    }

    void CollisionCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x);

        foreach (var collider in colliders)
        {
            if (collider.tag == "Block")
            {
                GetComponent<Effector>().Generate("Hit");
                Destroy();  // 탄환 제거

                // 점막 제거
                if (collider.transform.GetComponent<BeSplashed>().currentRidCount > 0)
                {
                    collider.transform.GetComponent<BeSplashed>().Rid();
                    break;
                }

                // 블럭제거
                collider.transform.GetComponent<Block>().Destroy();

                break;
            }

            if (collider.tag == "Wall")
            {
                GetComponent<Effector>().Generate("Hit");
                Destroy();  // 탄환 제거

                // 점막 제거
                if (collider.transform.GetComponent<BeSplashed>().currentRidCount > 0)
                {
                    collider.transform.GetComponent<BeSplashed>().Rid();
                    break;
                }
            }

            if(collider.tag == "Enemy")
            {
                GetComponent<Effector>().Generate("Hit");
                Destroy();
                collider.GetComponent<Enemy>().Damaged(damage);
            }

            if(collider.tag == "Object")
            {
                collider.GetComponent<IUseObject>().Use();
            }
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
