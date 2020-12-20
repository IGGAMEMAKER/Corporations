using System.Runtime.Remoting.Messaging;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class HideTweakButtonsIfCultureCooldownIsActive : View
{
    public GameObject TweakLeft;
    public GameObject TweakRight;

    public Text PolicyName;
    public Text LeftName;
    public Text RightName;

    public Text PolicyValue;

    public CorporatePolicy CorporatePolicy;

    public Image LeftChoiceImage;
    public Image RightChoiceImage;

    public Hint LeftHint;
    public Hint RightHint;

    [Header("Images")]
    public Sprite ManagerOrTeamLeft;
    public Sprite ManagerOrTeamRight;
    
    public Sprite MindsetFamilyOrMercenaryLeft;
    public Sprite MindsetFamilyOrMercenaryRight;

    public Sprite OpennessOrSecretivenessLeft;
    public Sprite OpennessOrSecretivenessRight;

    public Sprite SalariesLeft;
    public Sprite SalariesRight;

    public Sprite PeopleOrProcessLeft;
    public Sprite PeopleOrProcessRight;

    public Sprite SkillsOrCommunicationsLeft;
    public Sprite SkillsOrCommunicationsRight;

    public override void ViewRender()
    {
        base.ViewRender();

        bool hasCooldown = Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany);

        var culture = Companies.GetOwnCulture(MyCompany);

        var value = Companies.GetPolicyValue(MyCompany, CorporatePolicy);

        TweakLeft.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, false);
        TweakRight.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, true);

        PolicyValue.text = value.ToString();

        RenderTweak(TweakLeft, hasCooldown, value, false);
        RenderTweak(TweakRight, hasCooldown, value, true);        
    }

    private void RenderTweak(GameObject tweakButton, bool hasCooldown, int value, bool Increment)
    {
        bool willExceedLimits = (Increment && value == C.CORPORATE_CULTURE_LEVEL_MAX) || (!Increment && value == C.CORPORATE_CULTURE_LEVEL_MIN);

        Draw(tweakButton, !hasCooldown && !willExceedLimits && false);
    }

    private void OnEnable()
    {
        ViewRender();
    }

    //void OnTransformChildrenChanged()
    private void OnValidate()
    {
        SetStuff();
    }

    void SetStuff()
    {
        var defaultLeft = ManagerOrTeamLeft;
        var defaultRight = ManagerOrTeamRight;
        
        switch (CorporatePolicy)
        {
            // mindset?
            case CorporatePolicy.CompetitionOrSupport: 
                SetTexts("Competition or Collaboration", 
                // SetTexts("Openness or Secretiveness", 
                    "Competition", 
                    "Collaboration", 
                    OpennessOrSecretivenessRight,
                    OpennessOrSecretivenessLeft);
                break;

            case CorporatePolicy.DecisionsManagerOrTeam: 
                SetTexts("Company Structure",
                    "Vertical",
                    "Horizontal",
                    ManagerOrTeamLeft,
                    ManagerOrTeamRight,
                    "Less control",
                    "+Max feature lvl\n+Team speed");
                break;
            //case CorporatePolicy.DecisionsManagerOrTeam: SetTexts("Who makes decisions", "Manager", "Team", "+Team speed", "+Max feature lvl"); break;
            
            case CorporatePolicy.DoOrDelegate: 
                SetTexts("Control level",
                    "DO",
                    "Delegate",
                    defaultLeft,
                    defaultRight,
                    "+Max feature lvl",
                    "+1 team");
                break;
            
            case CorporatePolicy.PeopleOrProcesses: 
                SetTexts("People or Process",
                    "People",
                    "Process",
                    PeopleOrProcessLeft,
                    PeopleOrProcessRight,
                    "Managers grow faster",
                    "Organisation grows faster");
                break;

            case CorporatePolicy.SalariesLowOrHigh: 
                SetTexts("Salaries", 
                    "Low", 
                    "High", 
                    SalariesLeft, 
                    SalariesRight,
                    "We spend less money on salaries",
                    "Bigger loyalty"); 
                break;
            
            case CorporatePolicy.Make: 
                SetTexts("Make?", 
                    "Left", 
                    "Right", 
                    defaultLeft, 
                    defaultRight);
                break;

            case CorporatePolicy.HardSkillsOrSoftSkills: 
                SetTexts("Skills or Communication",
                    "Skill", 
                    "Team work", 
                    SkillsOrCommunicationsLeft, 
                    SkillsOrCommunicationsRight, 
                    "More PERSONAL traits", 
                    "More TEAM traits");
                break;

            default: SetTexts($"<b>{CorporatePolicy.ToString()}</b>", "Left", "Right", defaultLeft, defaultRight); break;
        }
    }

    void SetTexts(string Policy, string Left, string Right, Sprite leftSprite, Sprite rightSprite, string leftHint = "???", string rightHint = "???")
    {
        PolicyName.text = Policy;

        LeftName.text = Left; // + "\n" + Visuals.Positive($"<b>{leftHint}</b>");
        LeftHint.SetHint(Visuals.Positive($"<b>{leftHint}</b>"));
        LeftChoiceImage.sprite = leftSprite;

        RightName.text = Right; // + "\n" + Visuals.Positive($"<b>{rightHint}</b>");
        RightHint.SetHint(Visuals.Positive($"<b>{rightHint}</b>"));
        RightChoiceImage.sprite = rightSprite;
    }
}
