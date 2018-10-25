using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    class UIHintControl: MonoBehaviour
        //, IPointerClickHandler // 2
     //, IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
    {
        public UIHintable hintChild;

        public void SetHintableChild(UIHintable hintChild)
        {
            this.hintChild = hintChild;
        }

        public void SetHint(string hint) {
            hintChild.SetHintObject(hint);
        }

        void Start()
        {

        }

        void Update()
        {

        }

        void OnMouseEnter()
        {
            EnableHint();
        }

        void OnMouseOver()
        {
            EnableHint();
        }

        void OnMouseExit()
        {
            DisableHint();
        }

        void EnableHint()
        {
            Debug.Log("OnMouseIn");
            hintChild.OnHover();
        }

        void DisableHint()
        {
            Debug.Log("OnMouseExit");
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
