using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionShootOctoTurret : BossAction
{
    [SerializeField] private GameObject octoTurret;
    [SerializeField] private int numOfShot = 2;
    [SerializeField] private float shotInterval = .5f;
    [SerializeField] private float shotSpeed = 12f;


    public override void Take()
    {
        Debug.Log("OctoTurret");
        StartCoroutine(EShoot());

        Cut();
    }

    private IEnumerator EShoot()
    {
        var count = 0;
        var dir = 1;
        var animator = GetComponent<Animator>();

        while (true)
        {
            GetComponent<Animator>().SetTrigger("ShootOctoTurret");

            yield return new WaitUntil(() =>
            {
                var state = animator.GetCurrentAnimatorStateInfo(0);
                var isName = state.IsName("ShootOctoTurret");
                var normalizedTime = (state.normalizedTime - (int)state.normalizedTime);

                return (isName && normalizedTime > .9f);
            });

            var random = CatDown.Random.Get();

            ShootOctoTurret(dir);
            dir *= -1;

            if (++count >= numOfShot) break;

            yield return new WaitForSeconds(shotInterval);
        }
    }

    private void ShootOctoTurret(int dir)
    {
        var newOcto = Instantiate(octoTurret, transform.position, Quaternion.identity);

        newOcto.GetComponent<Rigidbody2D>().velocity = new Vector2(shotSpeed * dir, shotSpeed);
    }
}
