using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float minPopSpeed = 2f;
    public float maxPopSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateItem(Vector3 position)
    {
        GameObject newItem = Instantiate(this.gameObject, position, transform.rotation);
        Rigidbody2D rb2d = newItem.GetComponent<Rigidbody2D>();
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        Vector2 popSpeed = new Vector2(rand.Next(-(int)maxPopSpeed, (int)maxPopSpeed),
                                        rand.Next((int)minPopSpeed, (int)maxPopSpeed));
        rb2d.AddForce(popSpeed, ForceMode2D.Impulse);

        newItem.GetComponent<Item>().DestroyItem();
    }

    public void DestroyItem()
    {
        StartCoroutine(DestroyItem(this.gameObject));
    }

    IEnumerator DestroyItem(GameObject item)
    {
        yield return new WaitForSeconds(2f);
        Destroy(item);
    }
}
