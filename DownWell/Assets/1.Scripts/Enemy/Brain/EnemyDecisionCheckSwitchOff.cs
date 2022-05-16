using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatDown
{
    public class EnemyDecisionCheckSwitchOff : CatDown.EnemyDecision
    {
        Image lightPanel;

        protected override void EnterExamine()
        {
            lightPanel = GameObject.Find("Light").GetComponent<Image>();
        }
        protected override void Examine()
        {
            if (lightPanel.enabled)
                Decide();
        }

    }
}
