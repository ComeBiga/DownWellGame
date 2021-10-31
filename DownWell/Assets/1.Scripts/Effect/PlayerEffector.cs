using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffector : Effector
{
    private void Start()
    {
        GetComponent<PlayerController>().OnGrounded += PlayGroundingFX;
    }

    void PlayGroundingFX()
    {
        Generate("Grounding");
    }
}
