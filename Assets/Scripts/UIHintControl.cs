using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class UIHintControl: MonoBehaviour
        //, IPointerClickHandler // 2
     //, IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
    {
        public UIHint hintChild;

        public void SetHintableChild(UIHint hintChild)
        {
            this.hintChild = hintChild;
        }

        public void SetHint(string hint) {
            hintChild.SetHintObject(hint);
        }

        void EnableHint()
        {
            hintChild.OnHover();
        }

        void DisableHint()
        {
            hintChild.OnExit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EnableHint();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DisableHint();
        }
    }
}
