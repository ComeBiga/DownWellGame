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

    private void Update()
    {
        if(current != null) current.Act();
    }

    public void BecomeRage()
    {
        current = ragePattern;
    }
}
