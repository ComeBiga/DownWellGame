using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public abstract class EnemyDecisionCheckCollision : CatDown.EnemyDecision
    {
        protected CollisionCheck collision = new CollisionCheck();

        [Header("Collision")]
        public float rayLength = 0.1f;
        public int horizontalRayCount = 4;
        public int verticalRayCount = 4;
        public LayerMask groundLayermask;
    }
}
