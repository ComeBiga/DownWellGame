using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemDroppingSetting", menuName = "Item/ItemDroppingSetting")]
public class ItemDroppingSetting : ScriptableObject
{
    [Header("TimeSet")]
    public float popingTime = .5f;
    public float livingTime = 2f;

    [Header("PopSpeed")]
    public float maxHorizontalPopSpeed = 5f;
    public float minVerticalPopSpeed = 2f;
    public float maxVerticalPopSpeed = 10f;
}
