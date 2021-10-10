using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUtility
{
    public static float activeRangeOffset;

    public static bool CheckTargetRange(Transform origin)
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * (9 / 16);

        float h_tarTothis = Mathf.Abs(GameManager.instance.playerPrefab.transform.position.y - origin.position.y);

        if (h_tarTothis < height / 2 + activeRangeOffset)
            return true;


        return false;
    }
}
