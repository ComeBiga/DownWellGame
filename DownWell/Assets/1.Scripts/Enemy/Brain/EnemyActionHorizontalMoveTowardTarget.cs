using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionHorizontalMoveTowardTarget : CatDown.EnemyAction
    {
        public bool animateInFrameTime = false;

        [Header("Dash")]
        public float speed = 1f;
        public float dashTime = 1f;
        private int dashDir = 0;

        private Transform target;

        protected override void OnActionEnter()
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            InitDir();
            
            GetComponent<Animator>().SetTrigger("Move");

            if (!animateInFrameTime)
                Move();
        }

        protected override void OnActionUpdate()
        {
            //Animation();
        }

        void Animation()
        {
            GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
        }

        private void InitDir()
        {
            target = PlayerManager.instance.playerObject.transform;

            dashDir = (target.position.x < transform.position.x) ? -1 : 1;
        }

        public void Move()
        {
            Debug.Log($"speed:{speed}, dir:{dashDir}");
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashDir, GetComponent<Rigidbody2D>().velocity.y);

            Animation();
        }
    }
}
