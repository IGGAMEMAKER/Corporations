using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView : MonoBehaviour {
    public Text Loyalty;
    public Text Name;

    public void RenderExplorationMode(Feature feature)
    {
        Name.text = "Render Exploration mode";
    }

    public void RenderNormalMode(Feature feature)
    {
        Name.text = feature.name;
    }

    public void Render(Feature feature, int index)
    {
        bool isWorkInProgress = false;
        int featureId = index;
        int projectId = 0;

        Name.text = feature.name;

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
