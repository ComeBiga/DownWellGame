using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAct : MonoBehaviour
{
    public abstract bool Act(Rigidbody2D rigidbody);
}

namespace EnemyEditor
{
    public struct ActProperty
    {
        public string name;
        public float value;
    }
}
