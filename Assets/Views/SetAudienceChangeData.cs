using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudienceChangeData : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<RenderFlagshipAudienceGrowth>().SetEntity();
    }
}
