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
