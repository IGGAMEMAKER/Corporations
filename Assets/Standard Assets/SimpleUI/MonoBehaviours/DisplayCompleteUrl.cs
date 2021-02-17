using UnityEditor;
using UnityEngine;

namespace SimpleUI
{
    [ExecuteAlways]
    [RequireComponent(typeof(SimpleUIEventHandler))]
    public class DisplayCompleteUrl : MonoBehaviour
    {
        public SimpleUIEventHandler SimpleUIEventHandler;

        SimpleUI _instance = null;
        SimpleUI SimpleUI
        {
            get
            {
                if (_instance == null)
                    _instance = SimpleUI.GetInstance();

                return _instance;
            }
        }

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
                var url = SimpleUI.GetCurrentUrl();

                SimpleUIEventHandler.PreviewUrlInEditor(url);
            }
        }
    }
}