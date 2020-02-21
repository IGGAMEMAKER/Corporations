using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Frost;

public class SelectFirstTabWhenProjectScreenStarts : MonoBehaviour
{
    public TopPanelManager TopPanelManager;

    void OnEnable()
    {
        TopPanelManager.PanelAnim(0);
    }
}
