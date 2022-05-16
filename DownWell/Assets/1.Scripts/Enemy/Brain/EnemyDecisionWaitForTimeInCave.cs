using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatDown
{

    public class EnemyDecisionWaitForTimeInCave : CatDown.EnemyDecision
    {
        public float time = 3.0f;

        private float current = 0;

        Image lightPanel;

        protected override void EnterExamine()
        {
            lightPanel = GameObject.Find("Light").GetComponent<Image>();

            //if (GetComponent<Enemy>().info.name == "JellyPoo")
            //    Debug.Log("WaitForTime");
            WaitForTime(time);
        }

        protected override void Examine()
        {
            if (!lightPanel.enabled)
            {
                CancelInvoke();
                Decide(); 
            }
        }

        private void WaitForTime(float time)
        {
            Invoke("Decide", time);
        }
    }
}
