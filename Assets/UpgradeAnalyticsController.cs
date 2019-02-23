using Assets;
using Assets.Classes;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAnalyticsController : ButtonController
{
    void OnUpgradeAnalytics()
    {
        Debug.Log("OnUpgradeAnalytics");

        if (DataManager.instance.world.projects[0].Analytics < 10)
        {
            DataManager.instance.world.projects[0].Analytics++;
        }
    }

    public override void Execute() => OnUpgradeAnalytics();
}


public class RenderFeatureLevelSystem : ComponentSystem
{
    struct Group
    {
        public FeatureRendererTag featureRendererTag;
        public Text text;
    }

    ComponentGroup m_Group;

    protected override void OnCreateManager()
    {
        // GetComponentGroup should always be cached from OnCreateManager, never from OnUpdate
        // - ComponentGroup allocates GC memory
        // - Relatively expensive to create
        // - Component type dependencies of systems need to be declared during OnCreateManager,
        //   in order to allow automatic ordering of systems
        m_Group = GetComponentGroup(typeof(FeatureRendererTag), typeof(Text));
    }

    protected override void OnUpdate()
    {

    }
}
