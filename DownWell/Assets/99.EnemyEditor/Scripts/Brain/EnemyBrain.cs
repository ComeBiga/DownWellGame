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

        private void Start()
        {
            Init();

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
        }
    }
}
