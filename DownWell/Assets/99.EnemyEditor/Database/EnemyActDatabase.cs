using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new EnemyActDB", menuName = "Database/EnemyActDB")]
public class EnemyActDatabase : ScriptableObject
{
    public List<EnemyAct> enemyActs;
}
