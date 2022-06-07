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

    [Header("Interval")]
    [SerializeField] private float beforeRage = 1f;
    [SerializeField] private float betweenRage = 1f;
    [SerializeField] private float afterRage = 1f;

    [Header("RageColor")]
    [SerializeField] private Color first;
    [SerializeField] private Color second;

    public void Use()
    {
        StartCoroutine(EUse());
    }

    IEnumerator EUse()
    {
        Debug.Log($"EUse() in {this}");
        float timer = 0;

        BossAction.ready = true;
        BossAction.interval = this.interval;
        SetPattern(normalPattern);
        //BossAction.onCut += current.Act;
        //Act();

        Debug.Log($"BossAction.ready : {BossAction.ready}");

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
                current.CancelAction();
                StartCoroutine(EChangeToRage());
                break;
                //current.CancelAction();
                //StartCoroutine(EChangeToRage());
                
                //SetPattern(ragePattern);
                //BeRageMode();
                //current.Act();
            }

            yield return null;
        }

        while(true)
        {
            if(BossAction.ready)
            {
                Act();
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

    private IEnumerator EChangeToRage()
    {
        var effector = GetComponent<Effector>();
        var sr = GetComponent<SpriteRenderer>();
        var ubsr = GetComponent<Boss>().upperBossObject.GetComponent<SpriteRenderer>();

        BossAction.ready = false;

        yield return new WaitForSeconds(beforeRage);

        effector.Generate("Breath_Rage");
        sr.material.color = first;
        ubsr.material.color = first;
        GetComponent<Boss>().bodyColor = first;
        


        yield return new WaitForSeconds(betweenRage);

        effector.Generate("Breath_Rage");
        sr.material.color = second;
        ubsr.material.color = second;
        GetComponent<Boss>().bodyColor = second;


        yield return new WaitForSeconds(afterRage);

        SetPattern(ragePattern);
        BossAction.ready = true;
    }
}
