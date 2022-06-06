using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public abstract class Input
    {
        protected float horizontal;

        protected float sens;
        protected float dead;

        protected GameObject controller;

        public virtual void Init(float sens, float dead)
        {
            horizontal = 0;
            this.sens = sens;
            this.dead = dead;
        }

        public void SetController(GameObject controller)
        {
            this.controller = controller;
        }

        public virtual void SetControllerSize(int ratio)
        {

        }

        public virtual void Update()
        {

        }

        // Horizontal
        public abstract float GetAxisHorizontal();

        // Jump
        public abstract bool GetJumpButtonDown();
        public abstract bool GetJumpButton();
        public abstract bool GetJumpButtonUp();
    }
}
