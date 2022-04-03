using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class Random
    {
        public static System.Random Get()
        {
            string seed = (Time.time + UnityEngine.Random.value).ToString();
            System.Random rand = new System.Random(seed.GetHashCode());

            return rand;
        }
    }
}
