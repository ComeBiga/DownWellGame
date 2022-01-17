using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new StageDB", menuName = "Database/StageDB")]
public class StageDatabase : ScriptableObject
{
    [Header("Wall")]
    public List<GameObject> mapObjects;
    public List<Sprite> wallSprites;

    [Header("Enemy")]
    public List<GameObject> enemyObjects;

    [Header("Boss")]
    public GameObject bossObject;
}
