using UnityEditor;
using UnityEngine;

namespace SimpleUI
{
    [ExecuteAlways]
    [RequireComponent(typeof(SimpleUIEventHandler))]
    public class DisplayCompleteUrl : MonoBehaviour
    {
        public SimpleUIEventHandler SimpleUIEventHandler;

        void Start()
        {
            ShowCurrentPrefab();
        }

        void ShowCurrentPrefab()
        {
            if (SimpleUIEventHandler == null)
            {
                SimpleUIEventHandler = GetComponent<SimpleUIEventHandler>();
            }


            if (SimpleUIEventHandler != null)
            {
                var url = SimpleUI.GetInstance().GetCurrentUrl();

                SimpleUIEventHandler.PreviewUrlInEditor(url);
            }
        }
    }
}