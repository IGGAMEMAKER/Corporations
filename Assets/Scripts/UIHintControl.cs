using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class UIHintControl: MonoBehaviour
    {
        public UIHintable hintChild;

        public void SetHintableChild(UIHintable hintChild)
        {
            this.hintChild = hintChild;
        }

        public void SetHint(string hint) {
            hintChild.SetHintObject(hint);
        }

        public void SetHint(GameObject hint) {
            hintChild.SetHintObject(hint);
        }

        void OnMouseEnter()
        {
            hintChild.OnHover();
        }

        void OnMouseExit()
        {
            hintChild.OnExit();
        }
    }
}
