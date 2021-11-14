using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DB", menuName = "Database/brushDB")]
public class BrushDatabase : ScriptableObject
{
    public List<LevelObject> wallBrushes;
    public List<LevelObject> enemyBrushes;
}
