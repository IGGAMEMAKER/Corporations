using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNewCampaignOnlyIfInProductionMode : MonoBehaviour
{
    public GameObject ContinueGameButton;

    void Start()
    {
        #if UNITY_EDITOR
            ContinueGameButton.SetActive(true);
        #else
            ContinueGameButton.SetActive(false);
        #endif
    }
}
