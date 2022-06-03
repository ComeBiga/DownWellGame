using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLight : MonoBehaviour
{
    public bool activeOnce;
    public string stageName="Stage3"; 

    private void Start()
    {
        activeOnce = false;
        if (StageManager.instance.stages[StageManager.instance.CurrentStageIndex].Name == stageName)
        {
            GetComponent<Animator>().SetBool("On", true);
            activeOnce = true;
        }
    }
    void Update()
    {
        if (StageManager.instance.stages[StageManager.instance.CurrentStageIndex].Name != stageName)
        {
            GetComponent<Animator>().SetBool("On", true);
            activeOnce =false;
            stageName = "";
        }

        //∫“¿ª π‡«˚¿ª ∂ß
        if (GetComponent<Animator>().GetBool("On") && activeOnce)
        {
            activeOnce = false;
            if (IsInvoking("CaveLightOff"))
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
