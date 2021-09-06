using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
