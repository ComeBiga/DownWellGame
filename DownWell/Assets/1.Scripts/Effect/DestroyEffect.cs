using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public GameObject fx;

    public void Destroy()
    {
        if (fx == null)
            Destroy(this.gameObject);
        else
            Destroy(fx);
    }
}
