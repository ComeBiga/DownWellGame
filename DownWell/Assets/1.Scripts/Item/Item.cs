using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float livingTime = 2f;

    public float maxHorizontalPopSpeed = 5f;
    public float minVerticalPopSpeed = 2f;
    public float maxVerticalPopSpeed = 10f;

    public float popingTime = .5f;
    bool poping = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!poping)
            CollisionCheck();
    }

    void CollisionCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (var collider in colliders)
        {
            if(collider.tag == "Player")
            {
                // coin up
                GameManager.instance.GainCoin();
                Destroy(this.gameObject);
                // coin get sound
                //SoundManager.instance.PlayEffSound("gun");
            }
        }
    }

    public void InstantiateItem(Vector3 position)
    {
        GameObject newItem = Instantiate(this.gameObject, position, transform.rotation);
        Rigidbody2D rb2d = newItem.GetComponent<Rigidbody2D>();
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        Vector2 popSpeed = new Vector2(rand.Next(-(int)maxHorizontalPopSpeed, (int)maxHorizontalPopSpeed),
                                        rand.Next((int)minVerticalPopSpeed, (int)maxVerticalPopSpeed));
        rb2d.AddForce(popSpeed, ForceMode2D.Impulse);

        newItem.GetComponent<Item>().Invoke("EndPoping", popingTime);
        newItem.GetComponent<Item>().DestroyItem();
    }

    private void EndPoping()
    {
        poping = false;
    }

    public void DestroyItem()
    {
        StartCoroutine(DestroyItem(this.gameObject));
    }

    IEnumerator DestroyItem(GameObject item)
    {
        yield return new WaitForSeconds(livingTime);
        Destroy(item);
    }
}
