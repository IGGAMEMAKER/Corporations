using UnityEngine;

public class SetAudienceChangeData : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<RenderFlagshipAudienceGrowth>().SetEntity();
    }
}
