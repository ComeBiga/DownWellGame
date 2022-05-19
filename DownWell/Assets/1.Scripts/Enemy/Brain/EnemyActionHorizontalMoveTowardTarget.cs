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
        private float _speed = 0f;

        private CollisionCheck wallCollision;
        private EnemyPhysics physics;

        private void Start()
        {
            physics = new EnemyPhysics(transform,
                GetComponent<Rigidbody2D>(),
                GetComponent<BoxCollider2D>()
                );

            physics.InitCollision(4, 4, LayerMask.GetMask("Ground"));
        }

        protected override void OnActionEnter()
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            //wallCollision = new CollisionCheck();
            //wallCollision.Init(GetComponent<BoxCollider2D>(),
            //    .1f,
            //    4,
            //    4,
            //    LayerMask.GetMask("Ground"));
            //wallCollision.CalculateRaySpacing();

            //_speed = 0f;
            physics.SetVelocity(0f);
            InitDir();
            
            GetComponent<Animator>().SetTrigger("Move");

            if (!animateInFrameTime)
                Move();
        }

        protected override void OnActionUpdate()
        {
            //Animation();
            //wallCollision.UpdateRaycastOrigins();

            //MoveHorizontal();

            physics.Update();
        }

        void Animation()
        {
            //GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
            GetComponent<SpriteRenderer>().flipX = (dashDir < -0.01) ? true : false;
        }

        private void InitDir()
        {
            target = PlayerManager.instance.playerObject.transform;

            dashDir = (target.position.x < transform.position.x) ? -1 : 1;

            Animation();
        }

        public void Move()
        {
            //Debug.Log($"speed:{speed}, dir:{dashDir}");
            //GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashDir, GetComponent<Rigidbody2D>().velocity.y);
            //_speed = speed;

            physics.SetVelocity(speed * dashDir);
        }

        //private void MoveHorizontal()
        //{
        //    if(!CheckHorizontalCollision(dashDir))
        //        transform.position += Vector3.right * _speed * dashDir * Time.deltaTime;
        //}

        //private bool CheckHorizontalCollision(float hInput)
        //{
        //    if (hInput > 0)
        //        return wallCollision.CheckCollision(CollisionDirection.RIGHT);
        //    else if (hInput < 0)
        //        return wallCollision.CheckCollision(CollisionDirection.LEFT);
        //    else
        //        return false;
        //}
    }
}
