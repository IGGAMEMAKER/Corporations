using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class FeatureListRenderer : ListRenderer
{
    public override int itemsPerLine
    {
        get { return 2; }
        set { }
    }

    public override Vector2 spacing
    {
        get { return new Vector2(300f, 175f); }
        set { }
    }

    public override void RenderObject(GameObject obj, object item, int index, Dictionary<string, object> parameters)
    {
        obj.GetComponent<FeatureView>().Render((Feature)item, index, parameters);
    }
}
