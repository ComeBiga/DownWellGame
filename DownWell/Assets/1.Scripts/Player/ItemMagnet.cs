using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.transform.tag == "Item")
            {
                collision.transform.GetComponent<Item>().BeDraggedIntoPlayer(transform.parent);
            }
        }
    }
}
