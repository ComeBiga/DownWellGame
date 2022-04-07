using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionFollowTarget : CatDown.EnemyAction
    {
        private Transform target;
        public float speed = 1f;

        private Vector3 direction;

        protected override void OnActionEnter()
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
                target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void OnActionUpdate()
        {
            Debug.Log("OnActionUpdate");

            MoveToTarget();

            Animation();
        }

        void Animation()
        {
            if (target == null) return;

            var dir = target.position.x - transform.position.x;

            GetComponent<SpriteRenderer>().flipX = (dir < 0) ? true : false;
        }

        void MoveToTarget()
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;

                direction = target.position - transform.position;
            }
            //transform.position += direction.normalized * speed * Time.deltaTime;

            GetComponent<Rigidbody2D>().velocity = Vector2.one * direction.normalized * speed;
        }
    }
}
