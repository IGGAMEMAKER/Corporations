using UnityEngine;

public class ProductUpgradePerformanceFix : MonoBehaviour
{
    void Start()
    {
        #if UNITY_EDITOR
            //GetComponent<Animator>().enabled = false;
            //GetComponent<ToggleAnim>().enabled = false;
            //GetComponent<LazyUpdate>().DateChanges = false;
        #endif
    }
}
