using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyDecisionWaitForTime : CatDown.EnemyDecision
    {
        public float time = 3.0f;

        private float current = 0;

        protected override void EnterExamine()
        {
            //if (GetComponent<Enemy>().info.name == "JellyPoo")
            //    Debug.Log("WaitForTime");
            Debug.Log("EnterDecision");
            current = 0;
            Debug.Log($"current : {current}");

            //WaitForTime(time);
        }

        protected override void Examine()
        {
            current += Time.deltaTime;

            if(current > time)
            {
                Debug.Log($"current : {current}");
                Decide(this.ToString());
            }
        }

        private void WaitForTime(float time)
        {
            Invoke("Decide", time);
        }
    }
}
