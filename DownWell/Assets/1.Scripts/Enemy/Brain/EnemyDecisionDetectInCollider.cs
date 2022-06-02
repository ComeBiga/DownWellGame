using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class EnemyDecisionDetectInCollider : CatDown.EnemyDecision
    {
        public Collider2D sensorCollider;
        public LayerMask targetLayer;
        public ContactFilter2D targetFilter = new ContactFilter2D();

        protected override void Examine()
        {
            if(Detect())
            {
                //Debug.Log("Detected");
                Decide(this.ToString());
            }
        }

        bool Detect()
        {
            Collider2D[] target = new Collider2D[1];
            var count = sensorCollider.OverlapCollider(targetFilter, target);

            if (count > 0)
            {
                //Debug.Log(target[0].tag);
                return true;
            }

            return false;
        }
    }
}
