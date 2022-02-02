using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour
{
    [SerializeField] private float interval = 3.0f;
    [Range(0, 100)]
    public int rageModePercentage = 60;

    BossPattern current = new BossNormalPattern();

    public BossNormalPattern normalPattern;
    public BossRagePattern ragePattern;

    public void Use()
    {
        StartCoroutine(EUse());
    }

    IEnumerator EUse()
    {
        float timer = 0;

        BossAction.interval = this.interval;
        SetPattern(normalPattern);
        //BossAction.onCut += current.Act;
        //Act();

        while (true)
        {
            if (BossAction.ready)
            {
                Act();
                //timer += Time.deltaTime;

                //if (timer > interval)
                //{
                //    Debug.Log("Acting..");
                //    timer = 0;
                //    BossAction.ended = false;
                //    Act();
                //}
            }

            if (GetComponent<Boss>().UnderHealthRatio(rageModePercentage))
            {
                SetPattern(ragePattern);
                //BeRageMode();
                //current.Act();
            }

            yield return null;
        }
    }

    void Act()
    {
        current.Act();
    }

    public void SetPattern(BossPattern pattern)
    {
        current = pattern;
        //BossAction.onCut += current.Act;
    }
}
