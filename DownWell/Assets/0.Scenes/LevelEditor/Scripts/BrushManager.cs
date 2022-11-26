using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushManager : MonoBehaviour
{
    #region Singleton
    public static BrushManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public BrushDatabase brushDB;
    public StageDatabase stage;

    public LevelObject currentBrush;
    //public List<LevelObject> wallBrushes;
    //public List<LevelObject> enemyBrushes;
    public LevelObject eraserBrush;

    public PalletData enemyPallet;
    public List<LevelObject> EnemyPallet { get => enemyPallet.pallets; }
    public PalletData wallPallet;
    public List<LevelObject> WallPallet { get => wallPallet.pallets; }

    private void Start()
    {
        currentBrush = eraserBrush;
    }

    public void ChangeBrush(LevelObject brush)
    {
        currentBrush = brush;
    }

    public void ChangeToEraser()
    {
        currentBrush = eraserBrush;
    }
}
