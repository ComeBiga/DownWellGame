using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEditorManager : MonoBehaviour
{
    public List<EnemyAct> enemyActs = new List<EnemyAct>();

    private void Start()
    {
        //CreateEnemyAct();

        //Act();
    }

    public void AddAct(string actName)
    {
        enemyActs.Add(Activator.CreateInstance(Type.GetType(actName)) as EnemyAct);
    }

    void CreateEnemyAct()
    {
        //var itemToCast = Activator.CreateInstance(Type.GetType("HorizontalMovement"));

        enemyActs.Add(Activator.CreateInstance(Type.GetType("HorizontalMovement")) as EnemyAct);
        enemyActs.Add(Activator.CreateInstance(Type.GetType("FollowTarget")) as EnemyAct);
    }

    void Act()
    {
        foreach(var enemyAct in enemyActs)
        {
            enemyAct.Act(null);
        }
    }
}
