using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatDown
{
    public class EnemyDecisionDetectInColliderAndLight : CatDown.EnemyDecision
    {
        public Collider2D sensorCollider;
        public LayerMask targetLayer;
        public ContactFilter2D targetFilter = new ContactFilter2D();
        Image lightPanel;

        protected override void EnterExamine()
        {
            lightPanel = GameObject.Find("Light").GetComponent<Image>();
        }
        protected override void Examine()
        {
            if (Detect()&& lightPanel.enabled)
            {
                //Debug.Log("Detected");
                Decide();
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
