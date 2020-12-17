using Michsky.UI.Frost;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NavigateToTab : MonoBehaviour
{
    public int panelIndex = 0;

    public TopPanelManager TopPanelManager;

    void Animate() {
        TopPanelManager.PanelAnim(panelIndex);
    }

    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(Animate);
    }

    void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(Animate);
    }
}
