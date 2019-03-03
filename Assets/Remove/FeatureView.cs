using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView : MonoBehaviour {
    public Text Loyalty;
    public Text Name;

    public void Render(int index)
    {
        bool isWorkInProgress = false;
        int featureId = index;
        int projectId = 0;

        //if (feature.IsImplemented)
        //    RenderNormalMode(feature);
        //else
        //    RenderExplorationMode(feature);

        //if (feature.NeedsExploration)
        //    RenderExplorationMode(feature);
        //else
        //    RenderNormalMode(feature);

    }
}
