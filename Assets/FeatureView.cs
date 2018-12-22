using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView : MonoBehaviour {
    public ComplexityView ComplexityView;
    public Text Loyalty;
    public Text Name;

    public void Render(Feature feature, int index, Dictionary<string, object> parameters)
    {
        bool isWorkInProgress = false;
        int featureId = index;
        int projectId = 0;

        Name.text = feature.name;
    }
}
