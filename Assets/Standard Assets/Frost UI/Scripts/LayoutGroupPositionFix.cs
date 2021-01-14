using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Michsky.UI.Frost
{
    [ExecuteAlways]
    public class LayoutGroupPositionFix : MonoBehaviour
    {
        public LayoutGroup layoutGroup;

        [ExecuteAlways]
        void OnEnable()
        {
            if(layoutGroup == null)
            {
                layoutGroup = gameObject.GetComponent<LayoutGroup>();
            }

            // BECAUSE UNITY UI IS BUGGY AND NEEDS REFRESHING :P
            StartCoroutine(ExecuteAfterTime(0.01f));
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
            Destroy(this);
        }
    }
}