using UnityEngine;

public class ShowImprovementList : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        Debug.LogError("Show Improvement List");
        return "ShowImprovementList error";
    }
}
