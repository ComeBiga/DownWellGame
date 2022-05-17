using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy", menuName = "LevelObject/Enemy")]
public class EnemyInfo : LevelObject
{
    [Header("Enemy")]
    public int hp = 10;
}
