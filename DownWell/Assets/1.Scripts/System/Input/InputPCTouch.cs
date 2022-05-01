using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class InputPCTouch : Input
    {
        float screenCenter = Camera.main.pixelWidth / 2;
        float screenQuater = Camera.main.pixelWidth / 4;

        public override void Update()
        {
            base.Update();

            UpdateHorizontal();
        }

        public override float GetAxisHorizontal()
        {
            return horizontal;
        }

        public override bool GetJumpButton()
        {
            return UnityEngine.Input.GetMouseButton(0) && UnityEngine.Input.mousePosition.x > screenCenter;
        }

        public override bool GetJumpButtonDown()
        {
            return UnityEngine.Input.GetMouseButtonDown(0) && UnityEngine.Input.mousePosition.x > screenCenter;
        }

        public override bool GetJumpButtonUp()
        {
            return UnityEngine.Input.GetMouseButtonUp(0) && UnityEngine.Input.mousePosition.x > screenCenter;
        }

        private void UpdateHorizontal()
        {

            if (UnityEngine.Input.GetMouseButton(0) && UnityEngine.Input.mousePosition.x < screenCenter)
            {
                if (UnityEngine.Input.mousePosition.x > 0 && UnityEngine.Input.mousePosition.x <= screenQuater)
                {
                    //Debug.Log("left");
                    horizontal = (horizontal > 0) ? 0 : Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                }
                else if (UnityEngine.Input.mousePosition.x > screenQuater && UnityEngine.Input.mousePosition.x < screenCenter)
                {
                    //Debug.Log("right");
                    horizontal = (horizontal < 0) ? 0 : Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                }
            }
            else
            {
                horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
            }
        }
    }
}