using System.Runtime.Remoting.Messaging;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class HideTweakButtonsIfCultureCooldownIsActive : View
{
    public GameObject TweakLeft;
    public GameObject TweakRight;
    public GameObject TweakCenter;

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

        var value = Companies.GetPolicyValue(MyCompany, CorporatePolicy);

        TweakLeft.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, -1);
        TweakRight.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, 1);
        TweakCenter.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, 0);

        PolicyValue.text = value.ToString();

        RenderTweak(TweakLeft, hasCooldown, value, false);
        RenderTweak(TweakRight, hasCooldown, value, true);
    }

    private void RenderTweak(GameObject tweakButton, bool hasCooldown, int value, bool Increment)
    {
        bool willExceedLimits = (Increment && value == C.CORPORATE_CULTURE_LEVEL_MAX) || (!Increment && value == C.CORPORATE_CULTURE_LEVEL_MIN);

        // Draw(tweakButton, !hasCooldown && !willExceedLimits && false);
    }

    private void OnEnable()
    {
        ViewRender();
    }

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
                    "Managers generate more management points!",
                    "+Max feature lvl\n+Team speed\nCheaper teams");
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
                    "Managers stay longer in company",
                    "We spend less <b>Manager points</b> on teams\nOrganization grows faster");
                break;

            case CorporatePolicy.SalariesLowOrHigh: 
                SetTexts("Salaries", 
                    "Low", 
                    "High", 
                    SalariesLeft, 
                    SalariesRight,
                    "Less money on salaries",
                    "Bigger employee loyalty"); 
                break;
            
            case CorporatePolicy.HardSkillsOrSoftSkills: 
                SetTexts("Skills or Communication",
                    "Skill", 
                    "Team work", 
                    SkillsOrCommunicationsLeft, 
                    SkillsOrCommunicationsRight, 
                    "More PERSONAL traits\nBigger employee rating", 
                    "More TEAM traits");
                break;

            default: SetTexts($"<b>{CorporatePolicy.ToString()}</b>", "Left", "Right", defaultLeft, defaultRight); break;
        }
    }

    void SetTexts(string policy, string left, string right, Sprite leftSprite, Sprite rightSprite, string leftHint = "???", string rightHint = "???")
    {
        PolicyName.text = policy;

        LeftName.text = left; // + "\n" + Visuals.Positive($"<b>{leftHint}</b>");
        LeftHint.SetHint(Visuals.Positive($"<b>{leftHint}</b>"));
        LeftChoiceImage.sprite = leftSprite;

        RightName.text = right; // + "\n" + Visuals.Positive($"<b>{rightHint}</b>");
        RightHint.SetHint(Visuals.Positive($"<b>{rightHint}</b>"));
        RightChoiceImage.sprite = rightSprite;
    }
}
