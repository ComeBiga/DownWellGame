using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLight : MonoBehaviour
{
    Animator caveLight;

    private void Start()
    {
        caveLight = PlayerManager.instance.playerObject.transform.GetChild(5).gameObject.GetComponent<Animator>();

        if (StageManager.instance.stages[StageManager.instance.CurrentStageIndex].Name == "Stage3")
            caveLight.SetBool("On", false);
    }
    void Update()
    {
    }
}
