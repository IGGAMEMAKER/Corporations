﻿using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Michsky.UI.Frost
{
    public class TooltipContent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("CONTENT")]
        public string title;
        [TextArea] public string description;

        [Header("RESOURCES")]
        public GameObject mouseTooltipObject;
        public TextMeshProUGUI mouseTitle;
        public TextMeshProUGUI mouseDescription;

        private Animator mouseAnimator;
		private BlurManager mouseBlur;

        void Start()
        {
            // hack to avoid specifying references manually
            if (mouseTooltipObject == null)
            {
                mouseTooltipObject = FindObjectOfType<MichTooltip>().gameObject;

                var components = mouseTooltipObject.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
                mouseTitle = components[0];
                mouseDescription = components[1];
            }

            mouseAnimator = mouseTooltipObject.GetComponent<Animator>();
			mouseBlur = mouseTooltipObject.GetComponent<BlurManager>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseTitle.text = title;
            mouseDescription.text = description;
            mouseAnimator.Play("In");
			mouseBlur.BlurInAnim();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseAnimator.Play("Out");
			mouseBlur.BlurOutAnim();
        }
    }
}