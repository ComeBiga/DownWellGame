using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Wall
{
    public void Destroy()
    {
        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);
    }
}
