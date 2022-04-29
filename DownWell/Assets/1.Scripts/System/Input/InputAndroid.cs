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

        public override float GetAxisHorizontal()
        {
            float horizontal = 0f;

            if (UnityEngine.Input.touchCount > 0)
            {
                var moveTouchCount = 0;
                foreach (var touch in UnityEngine.Input.touches)
                {
                    if (touch.position.x < screenCenter)
                    {
                        moveTouchCount++;

                        // ���� ����Ű
                        if (touch.position.x > 0 && touch.position.x <= screenQuater)
                        {
                            Debug.Log("left");
                            //horizontal = Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                            horizontal = (horizontal > 0) ? 0 : Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                            //horizontal = -1;
                        }
                        // ������ ����Ű
                        else if (touch.position.x > screenQuater && touch.position.x < screenCenter)
                        {
                            Debug.Log("right");
                            //horizontal = Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                            horizontal = (horizontal < 0) ? 0 : Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                            //horizontal = 1;
                        }
                    }
                }

                // ��ġ�� ������, ����Ű�� ������ �ʾ��� ��
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
    }
}