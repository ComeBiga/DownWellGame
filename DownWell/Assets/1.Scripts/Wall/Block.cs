using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Wall
{
    private void Awake()
    {
        
    }

    public virtual void Destroy()
    {
        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);
    }
}
