using Assets.Core;
using UnityEngine.UI;

public class ExpertiseView : View
{
    public LinkToNiche LinkToNiche;
    public ProgressBar ProgressBar;

    NicheType NicheType;

    public void SetEntity(NicheType nicheType)
    {
        NicheType = nicheType;

        var exp = SelectedHuman.humanSkills.Expertise[NicheType];

        ProgressBar.SetValue(exp, 100);

        LinkToNiche.SetNiche(NicheType);
        LinkToNiche.gameObject.GetComponent<Text>().text = Enums.GetFormattedNicheName(NicheType);
    }
}
