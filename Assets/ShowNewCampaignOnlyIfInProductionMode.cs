using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNewCampaignOnlyIfInProductionMode : MonoBehaviour
{
    public GameObject ContinueGameButton;
    public GameObject NewGameButton;

    public ContinueGame ContinueGame1;
    public StartGameController StartGameController1;

    void Start()
    {
        #if UNITY_EDITOR
            ContinueGameButton.SetActive(true);
        #else
            ContinueGameButton.SetActive(false);
        #endif
    }

    public void StartNewGame()
    {
        HideButtons();
        StartGameController1.Execute();
    }

    public void ContinueGame()
    {
        HideButtons();
        ContinueGame1.Execute();
    }

    void HideButtons()
    {
        NewGameButton.SetActive(false);
        ContinueGameButton.SetActive(false);
    }
}
