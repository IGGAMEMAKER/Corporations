using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class AddTeamButton : ButtonController
{
    public TeamType TeamType;

    //[Space(20)]
    [Header("Specify team images here")]
    Sprite source;

    public Sprite Universal;
    public Sprite Development;
    public Sprite Marketing;
    public Sprite Servers;
    public Sprite Support;

    [Space(20)]
    public Image TeamTypeImage;
    public TextMeshProUGUI Title;

    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType);
    }

    //[ExecuteInEditMode]
    void OnValidate()
    {
        //Debug.Log("AddTeamButton");
        if (Title == null)
            return;
        Title.text = $"<b>{Teams.GetFormattedTeamType(TeamType)}"; // Create\n

        var hint = GetComponent<Hint>();

        switch (TeamType)
        {
            case TeamType.SupportTeam:
                source = Support;
                hint.SetHint("This team supports our clients");
                break;

            case TeamType.ServersideTeam:
                source = Servers;
                hint.SetHint("This team maintains big servers");
                break;

            case TeamType.DevelopmentTeam:
                source = Development;
                hint.SetHint("This team upgrades features faster");
                break;

            case TeamType.MarketingTeam:
                source = Marketing;
                hint.SetHint("This team makes more effecient marketing campaigns");
                break;

            default:
                source = Universal;
                hint.SetHint("This team can perform all types of tasks");
                break;
        }

        TeamTypeImage.sprite = source;

    }
}
