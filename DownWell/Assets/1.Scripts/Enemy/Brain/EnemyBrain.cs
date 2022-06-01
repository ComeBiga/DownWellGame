using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField]
        private List<CatDown.EnemyState> states;

        private CatDown.EnemyState current;
        public CatDown.EnemyState Current { get { return current; } }

        private int count = 0;

        private void Start()
        {
            Init();
            //if (GetComponent<Enemy>().info.name == "JellyPoo") Debug.Log($"Current state : {current.name} at Brain Start");
            current.Handle();
        }

        private void Init()
        {
            foreach (var s in states)
            {
                s.Init(this);
            }

            current = states[0];
        }

        public void ChangeState(string name)
        {
            current.Stop();
            current = states.Find(s => s.name == name);
            current.EnterState();
            if (GetComponent<Enemy>().info.name == "JellyPoo")
            {
                //Debug.Log($"transition count :{++count}");
                //Debug.Log($"Current state : {current.name}");
            }
            current.Handle();
        }

        public static void CurrentState()
        {
            //Debug.Log($"current state : )
        }

        #region Static Function

        public static bool CheckTargetRange(Transform player, Transform enemy, float _activeRange = 3f)
        {
            float height = Camera.main.orthographicSize * 2;
            float width = height * (9 / 16);

            float h_tarTothis = Mathf.Abs(player.position.y - enemy.position.y);

            if (height / 2 + _activeRange < enemy.position.y - player.position.y)
            {
                //Debug.Log("Destroy " + enemy.name);
                Destroy(enemy.gameObject);
            }

            if (h_tarTothis < height / 2 + _activeRange)
                return true;

            return false;
        }

        #endregion
    }
}
