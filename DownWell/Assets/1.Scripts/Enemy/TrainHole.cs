using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHole : MonoBehaviour
{
    [SerializeField] private GameObject train;
    [SerializeField] private BoxCollider2D sensor;
    [SerializeField] private float passTime;

    private List<Collider2D> colliders;
    private ContactFilter2D playerFilter;

    private void Start()
    {
        colliders = new List<Collider2D>();

        playerFilter = new ContactFilter2D();
        playerFilter.useLayerMask = true;
        playerFilter.layerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        SensorCharacter();
    }

    public void PassThrough()
    {
        StartCoroutine(EPassThrough());
    }
    
    private IEnumerator EPassThrough()
    {
        train.SetActive(true);

        yield return new WaitForSeconds(passTime);

        train.SetActive(false);
    }

    private void SensorCharacter()
    {
        var count = sensor.OverlapCollider(playerFilter, colliders);

        if(count > 0)
        {
            foreach(var c in colliders)
            {
                sensor.gameObject.SetActive(false);

                PassThrough();
                break;
            }
        }
    }
}
