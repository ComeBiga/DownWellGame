using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool poping = true;
    //private bool pickedUp = false;

    //public bool PickedUp { get { return pickedUp; } }
    
    [Header("ItemInfo")]
    public ItemInfo i_Info;

    private void EndPoping()
    {
        poping = false;
    }

    public void DestroyItem(float livingTime)
    {
        StartCoroutine(DestroyItem(this.gameObject, livingTime));
    }

    private IEnumerator DestroyItem(GameObject item, float livingTime)
    {
        yield return new WaitForSeconds(livingTime);
        Destroy(item);
    }

    public bool PickUp()
    {
        if (poping) return false;

        // Stop being dragged
        StopCoroutine("EDraggedIntoPlayer");

        // Item picking up event
        OnPickedUp();

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Coin");

        // Destroy
        Destroy(this.gameObject);

        return true;
    }

    protected virtual void OnPickedUp()
    {

    }

    public virtual void PutIn(PlayerItem playerItem)
    {

    }

    public void BeDraggedIntoPlayer(Transform target, float followSpeed)
    {
        StartCoroutine(EDraggedIntoPlayer(target, followSpeed));
    }

    private IEnumerator EDraggedIntoPlayer(Transform target, float followSpeed)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;

        while(true)
        {
            Vector3 dir = target.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = dir * followSpeed;

            yield return null;
        }
    }
}
