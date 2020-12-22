using Assets.Core;

//[RequireComponent(typeof(LazyUpdate))]
public class TweakCorporatePolicy : ButtonController
{
    public CorporatePolicy CorporatePolicy;
    public int Change;

    public override void Execute()
    {
        var value = Companies.GetPolicyValue(MyCompany, CorporatePolicy);
        
        if (Change > 0)
            Companies.IncrementCorporatePolicy(Q, MyCompany, CorporatePolicy);
        
        if (Change < 0)
            Companies.DecrementCorporatePolicy(Q, MyCompany, CorporatePolicy);
        
        if (Change == 0)
            Companies.SetCorporatePolicy(Q, MyCompany, CorporatePolicy);

        CopyCorporateCulture(MyCompany, Flagship);
        DescribeChange(value != Companies.GetPolicyValue(MyCompany, CorporatePolicy));
    }

    public static void CopyCorporateCulture(GameEntity from, GameEntity to)
    {
        foreach (var policy in from.corporateCulture.Culture)
        {
            to.corporateCulture.Culture[policy.Key] = policy.Value;
        }
    }

    public void SetSettings(CorporatePolicy policy, int change)
    {
        CorporatePolicy = policy;
        Change = change;
        
        
    }

    void DescribeChange(bool valueChanged)
    {
        if (!valueChanged)
            return;

        if (CorporatePolicy == CorporatePolicy.DoOrDelegate)
        {
            DescribeDelegationChanges();
        }

        PlaySound(Assets.Sound.GoalCompleted);
    }

    void DescribeDelegationChanges()
    {
        var newValue = Companies.GetPolicyValue(MyCompany, CorporatePolicy);

        var text = "";

        if (Change > 0)
        {
            switch (newValue)
            {
                case 1: text = "You can add TEAMS now!"; break;
                case 2: text = "You can promote teams to BIG TEAMS!"; break;
                case 3: text = "You can promote big teams to DEPARTMENTS!"; break;
                case 4: text = "You can change corporate culture now!"; break;
                case 5: text = "You can buy companies now!"; break;
                case 6: text = "You can have up to 4 companies now!"; break;
                case 7: text = "You can own groups"; break;
                case 8: text = "You can form a CORPORATION!"; break;
                case 9: text = "You can run an IPO!"; break;
            }

            NotificationUtils.AddSimplePopup(Q, Visuals.Positive(text), "Corporate culture changed");
        }
    }
}
