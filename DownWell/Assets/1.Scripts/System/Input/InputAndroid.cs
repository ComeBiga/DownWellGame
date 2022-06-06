using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class InputAndroid : Input
    {
        float screenCenter = Camera.main.pixelWidth / 2;
        float screenQuater = Camera.main.pixelWidth / 4;

        public override void Init(float sens, float dead)
        {
            base.Init(sens, dead);

            screenCenter = Camera.main.pixelWidth / 2;
            screenQuater = Camera.main.pixelWidth / 4;
        }

        public override void SetControllerSize(int ratio)
        {
            screenCenter *= ratio/100f;
            screenQuater *= ratio/100f;

            controller.GetComponent<UIController>().SetSize(ratio);
        }

        public override void Update()
        {
            base.Update();

            UpdateHorizontal();
        }

        public override float GetAxisHorizontal()
        {
            return horizontal;
        }


        public override bool GetJumpButtonDown()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                foreach (var touch in UnityEngine.Input.touches)
                {
                    if (touch.position.x > screenCenter && (touch.phase == TouchPhase.Began))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool GetJumpButton()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                foreach (var touch in UnityEngine.Input.touches)
                {
                    if (touch.position.x > screenCenter && (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool GetJumpButtonUp()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                foreach (var touch in UnityEngine.Input.touches)
                {
                    if (touch.position.x > screenCenter && (touch.phase == TouchPhase.Ended))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void UpdateHorizontal()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                var moveTouchCount = 0;
                foreach (var touch in UnityEngine.Input.touches)
                {
                    if (touch.position.x < screenCenter)
                    {
                        moveTouchCount++;

                        // 왼쪽 방향키
                        if (touch.position.x > 0 && touch.position.x <= screenQuater)
                        {
                            Debug.Log("left");
                            //horizontal = Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                            horizontal = (horizontal > 0) ? 0 : Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                            //horizontal = -1;

                            // ButtonUI
                            controller.GetComponent<UIController>().SetLeftButton(1);
                            controller.GetComponent<UIController>().SetRightButton(0);
                        }
                        // 오른쪽 방향키
                        else if (touch.position.x > screenQuater && touch.position.x < screenCenter)
                        {
                            Debug.Log("right");
                            //horizontal = Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                            horizontal = (horizontal < 0) ? 0 : Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                            //horizontal = 1;

                            // ButtonUI
                            controller.GetComponent<UIController>().SetLeftButton(0);
                            controller.GetComponent<UIController>().SetRightButton(1);
                        }
                    }
                }

                // 터치는 있지만, 방향키가 눌리지 않았을 때
                if (moveTouchCount <= 0)
                {
                    horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
                    //horizontal = 0;

                    // ButtonUI
                    controller.GetComponent<UIController>().SetLeftButton(0);
                    controller.GetComponent<UIController>().SetRightButton(0);
                }
            }
            else
            {
                horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
                //horizontal = 0;

                // ButtonUI
                controller.GetComponent<UIController>().SetLeftButton(0);
                controller.GetComponent<UIController>().SetRightButton(0);
            }
        }

        private void UpdateHorizontalAsButton()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                var moveTouchCount = 0;
                foreach (var touch in UnityEngine.Input.touches)
                {
                    if (touch.position.x < screenCenter)
                    {
                        moveTouchCount++;

                        // 왼쪽 방향키
                        // 오른쪽 방향키
                    }
                }

                // 터치는 있지만, 방향키가 눌리지 않았을 때
                if (moveTouchCount <= 0)
                {
                    horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
                    //horizontal = 0;
                }
            }
            else
            {
                horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
                //horizontal = 0;
            }
        }
    }
}