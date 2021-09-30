using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Wall
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
