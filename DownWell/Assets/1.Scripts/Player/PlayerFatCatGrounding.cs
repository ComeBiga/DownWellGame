using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFatCatGrounding : MonoBehaviour
{
    [Header("Quake")]
    [SerializeField] private Collider2D quakeCollider;
    [SerializeField] private ContactFilter2D filter;
    [SerializeField] private float magnitude = 2f;
    [SerializeField] private float duration = .2f;


    private PlayerPhysics physics;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        physics = GetComponent<PlayerPhysics>();
        physics.OnGrounded += QuakeGround;
    }

    private void QuakeGround()
    {
        var colliders = new List<Collider2D>();
        quakeCollider.OverlapCollider(filter, colliders);

        if(colliders.Count > 0)
        {
            foreach(var collider in colliders)
            {
                Debug.Log($"{collider.gameObject.name} quaked as FatCat!");
                collider.GetComponent<Enemy>().Die();
            }
        }

        Camera.main.GetComponent<CameraShake>().Shake(magnitude, duration);
    }
}
