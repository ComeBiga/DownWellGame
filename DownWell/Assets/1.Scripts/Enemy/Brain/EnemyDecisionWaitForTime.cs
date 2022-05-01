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
            WaitForTime(time);
        }

        protected override void Examine()
        {

        }

        private void WaitForTime(float time)
        {
            Invoke("Decide", time);
        }
    }
}
