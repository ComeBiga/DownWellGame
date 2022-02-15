using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class EnemyActionJump : CatDown.EnemyAction
    {
        [SerializeField]
        private bool executeInAnimationFrame = false;
        public float horizontalSpeed = 0;
        public float verticalSpeed = 1f;

        protected override void OnActionEnter()
        {
            if (executeInAnimationFrame) return;

            Jump();
        }

        public void Jump()
        {
            var direction = (GetComponent<SpriteRenderer>().flipX == true) ? -1 : 1;

            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * horizontalSpeed, verticalSpeed);
        }
    }
}
