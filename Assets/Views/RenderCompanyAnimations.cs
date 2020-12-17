using UnityEngine;

public class RenderCompanyAnimations : MonoBehaviour
{
    GameEntity company;

    public RenderFlagshipAudienceGrowth audienceGrowth;

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        audienceGrowth.SetEntity();
    }
}
