using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class InputPC : Input
    {
        public override float GetAxisHorizontal()
        {
            return UnityEngine.Input.GetAxis("Horizontal");
        }

        public override bool GetJumpButton()
        {
            return UnityEngine.Input.GetButton("Jump");
        }

        public override bool GetJumpButtonDown()
        {
            return UnityEngine.Input.GetButtonDown("Jump");
        }

        public override bool GetJumpButtonUp()
        {
            return UnityEngine.Input.GetButtonUp("Jump");
        }
    }
}