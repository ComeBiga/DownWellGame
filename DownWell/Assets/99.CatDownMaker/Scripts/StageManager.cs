using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown.Maker
{
    [UnityEditor.InitializeOnLoad]
    public class StageManager : MonoBehaviour
    {
        #region Singleton
        public static StageManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }
        #endregion

        public StageDatabase database;
    }
}
