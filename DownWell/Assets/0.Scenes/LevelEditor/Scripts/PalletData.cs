using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Pallet", menuName = "Pallet/Pallet")]
public class PalletData : ScriptableObject
{
    public List<LevelObject> pallets = new List<LevelObject>();
}
