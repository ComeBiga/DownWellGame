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

            //SetStartDirection();

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

            Move();
            //Debug.Log("Start" + GetComponent<Rigidbody2D>().velocity);

            //if (!moveAsCollision) MoveAsTime();
            timer = 0;
            //Debug.Log("StartTimer:"+ timer);
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
            //Debug.Log(GetComponent<Rigidbody2D>().velocity);

            //Debug.Log("HM Updating");

            Animation();
        }

        void Animation()
        {
            if (GetComponent<Rigidbody2D>().velocity.x < -0.01f) GetComponent<SpriteRenderer>().flipX = true;
            else if(GetComponent<Rigidbody2D>().velocity.x > 0.01f) GetComponent<SpriteRenderer>().flipX = false;
            //GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
        }

        private void Move()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, GetComponent<Rigidbody2D>().velocity.y);
        }

        protected void MoveAsCollision()
        {
            collision.UpdateRaycastOrigins();

            if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
            else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
            else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
            else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

            //Move();
            //GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, GetComponent<Rigidbody2D>().velocity.y);
        }

        protected void MoveAsTime()
        {
            timer += Time.deltaTime;

            if (timer >= changeTime)
            {
                ChangeDirection();

                timer = 0;
            }

            //Move();

            //Invoke("ChangeDirection", changeTime);
        }

        private void ChangeDirection()
        {
            dir *= -1;
            Move();
            //Debug.Log("ChangeDirection");

            //Invoke("ChangeDirection", changeTime);
        }
    }
}
