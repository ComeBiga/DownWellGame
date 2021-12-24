using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAction : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public List<EnemyActionState> actionStates;
    //public List<EnemyAct> enemyActs;

    private string currentStateName = "";
    private EnemyActionState currentState;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        //Act();
        currentState = actionStates[0];
    }

    private void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        currentState.UpdateActionState();

        if(currentState.CheckTransition(out currentStateName))
        {
            currentState = actionStates.Find(s => s.stateName == currentStateName);
            Debug.Log(currentState.stateName);
        }
    }

    //void Act()
    //{
    //    StartCoroutine(ActUpdate());
    //}

    //IEnumerator ActUpdate()
    //{
    //    int index = 0;
    //    bool actEnd = false;

    //    while (true)
    //    {
    //        if (actEnd)
    //        {
                
    //            index++;

    //            if (index >= enemyActs.Count) index = 0;
    //        }

    //        actEnd = enemyActs[index].Act(rigidbody);

    //        yield return null;
    //    }
    //}
}

[System.Serializable]
public class EnemyActionState
{
    public string stateName = "";
    public List<EnemyAct> acts;
    public List<EnemyTransition> transitions;

    //private EnemyAct currentAct;
    private int index = 0;

    public void UpdateActionState()
    {
        if(acts[index].UpdateAct())
        {
            index++;
            //Debug.Log(index);
            if (index >= acts.Count) index = 0;
        }
    }

    public bool CheckTransition(out string stateTo)
    {
        stateTo = "";

        foreach(var t in transitions)
        {
            // 만약 조건에 만족한다면
            if (t.Decide(out stateTo))
            {
                InitActs();
                index = 0;
                t.onTransition.Invoke();
                //Debug.Log("Decided");
                //Debug.Log(stateTo);
                return true;
            }
        }

        return false;
    }

    void InitActs()
    {
        foreach(var act in acts)
        {
            act.Init();
        }
    }
}

[System.Serializable]
public class EnemyTransition
{
    public EnemyDecision decision;
    public string trueState = "";
    public string falseState = "";
    public UnityEvent onTransition;

    public bool Decide(out string state)
    {
        state = (decision.Decide() == true) ? trueState : falseState;

        return (state != "") ? true : false;
    }
}
