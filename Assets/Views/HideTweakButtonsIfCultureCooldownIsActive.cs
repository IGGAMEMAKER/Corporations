using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
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

        tweakButton.SetActive(!hasCooldown && !willExceedLimits);
    }

    private void OnEnable()
    {
        ViewRender();
    }

    //void OnTransformChildrenChanged()
    void OnValidate()
    {
        SetStuff();
    }

    void SetStuff()
    {
        switch (CorporatePolicy)
        {
            case CorporatePolicy.CompetitionOrSupport: SetTexts("Competition or Support", "Competition", "Support"); break;

            case CorporatePolicy.DecisionsManagerOrTeam: SetTexts("Structure", "Vertical", "Horizontal", "+Team speed", "+Max feature lvl"); break;
            //case CorporatePolicy.DecisionsManagerOrTeam: SetTexts("Who makes decisions", "Manager", "Team", "+Team speed", "+Max feature lvl"); break;
            case CorporatePolicy.DoOrDelegate: SetTexts("Control level", "DO", "Delegate", "+Max feature lvl", "+1 team"); break;
            case CorporatePolicy.PeopleOrProcesses: SetTexts("People or Processes", "People", "Processes", "Managers grow faster", "Organisation grows faster"); break;

            case CorporatePolicy.Sell: SetTexts("Make or sell", "Make", "Sell"); break;
            case CorporatePolicy.SalariesLowOrHigh: SetTexts("Salaries", "Low", "High"); break;
            case CorporatePolicy.Make: SetTexts("Make?", "Left", "Right"); break;

            default: SetTexts($"<b>{CorporatePolicy.ToString()}</b>", "Left", "Right"); break;
        }
    }

    void SetTexts(string Policy, string Left, string Right, string leftHint = "???", string rightHint = "???")
    {
        PolicyName.text = Policy;

        LeftName.text = Left + "\n" + Visuals.Positive($"<b>{leftHint}</b>");
        RightName.text = Right + "\n" + Visuals.Positive($"<b>{rightHint}</b>");
        //RightName.text = Right;
    }
}
