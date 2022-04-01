using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionHorizontalMoveTowardTarget : CatDown.EnemyAction
    {
        [Header("Dash")]
        public float speed = 1f;
        public float dashTime = 1f;
        private int dashDir = 0;

        private Transform target;

        protected override void OnActionEnter()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

            target = GameObject.FindGameObjectWithTag("Player").transform;

            dashDir = (target.position.x - transform.position.x < 0) ? -1 : 1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashDir, GetComponent<Rigidbody2D>().velocity.y);
        }

        protected override void OnActionUpdate()
        {
            Animation();
        }

        void Animation()
        {
            GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
        }
    }
}
