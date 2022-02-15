using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionHorizontalMove : CatDown.EnemyActionCollisionMove
    {
        [Header("Move Value")]
        public bool moveAsCollision = false;

        public float speed = 1f;
        public enum Direction { LEFT, RIGHT }
        public Direction startDirection = Direction.LEFT;
        protected int dir = -1;

        public float changeTime = 3f;
        protected float timer = 0;

        protected override void OnActionEnter()
        {
            collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
            collision.CalculateRaySpacing();

            SetStartDirection();

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        protected void SetStartDirection()
        {
            switch (startDirection)
            {
                case Direction.LEFT:
                    dir = -1;
                    break;
                case Direction.RIGHT:
                    dir = 1;
                    break;
            }
        }

        protected override void OnActionUpdate()
        {
            if (moveAsCollision) MoveAsCollision();
            else MoveAsTime();

            Animation();
        }

        void Animation()
        {
            GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
        }

        protected void MoveAsCollision()
        {
            collision.UpdateRaycastOrigins();

            if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
            else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
            else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
            else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, GetComponent<Rigidbody2D>().velocity.y);
        }

        protected void MoveAsTime()
        {
            timer += Time.deltaTime;

            if (timer >= changeTime)
            {
                dir *= -1;

                timer = 0;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
