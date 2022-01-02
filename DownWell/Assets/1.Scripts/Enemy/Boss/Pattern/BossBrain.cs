using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour
{
    BossPattern current;

    public BossNormalPattern normalPattern;
    public BossRagePattern ragePattern;

    private void Start()
    {
        current = normalPattern;
    }

    public void BecomeRage()
    {
        current = ragePattern;
    }
}
