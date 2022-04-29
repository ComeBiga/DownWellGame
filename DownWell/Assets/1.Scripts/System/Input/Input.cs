using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public abstract class Input
    {
        protected float sens;
        protected float dead;

        public virtual void Init(float sens, float dead)
        {
            this.sens = sens;
            this.dead = dead;
        }

        // Horizontal
        public abstract float GetAxisHorizontal();

        // Jump
        public abstract bool GetJumpButtonDown();
        public abstract bool GetJumpButton();
        public abstract bool GetJumpButtonUp();
    }
}
