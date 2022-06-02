using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLight : MonoBehaviour
{
    public bool activeOnce;

    private void Start()
    {
        activeOnce = false;
        if (StageManager.instance.stages[StageManager.instance.CurrentStageIndex].Name == "Stage3")
        {
            GetComponent<Animator>().SetBool("On", false);
            activeOnce = true;
        }
    }
    void Update()
    {
        //∫“¿ª π‡«˚¿ª ∂ß
        if (GetComponent<Animator>().GetBool("On")&& activeOnce)
        {
            activeOnce = false;
            if(IsInvoking("CaveLightOff"))
                CancelInvoke("CaveLightOff");
            Invoke("CaveLightOff", 3f);
        }
    }

    void CaveLightOff()
    {
        GetComponent<Animator>().SetBool("On", false);
        activeOnce = true;
    }
}
