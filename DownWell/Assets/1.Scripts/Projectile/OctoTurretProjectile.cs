using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoTurretProjectile : Projectile
{
    private Camera mainCamera;
    private float width;
    private float height;

    public override void Init(float speed, Vector2 direction)
    {
        base.Init(speed, direction);

        mainCamera = Camera.main;
        height = mainCamera.orthographicSize * 2;
        width = height / 16 * 9;
    }


    public override void Update()
    {
        var position = transform.position;
        var cameraPos = mainCamera.transform.position;
        if (cameraPos.x - width / 2 > position.x || cameraPos.x + width / 2 < position.x || cameraPos.y - height / 2 > position.y || cameraPos.y + height / 2 < position.y)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().Damaged(transform);
            
            if(destroyOnHit) Destroy();
        }
    }
}
