using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class CreateCompanyController : View
{
    public GameObject StartupButton;

    void OnEnable()
    {
        var niche = Markets.GetNiche(Q, SelectedNiche);
        var startCapital = Markets.GetStartCapital(niche, Q);

        if (MyGroupEntity != null)
        {
            var groupName = Visuals.Colorize(MyGroupEntity.company.Name, Colors.COLOR_COMPANY_WHERE_I_AM_CEO);

            SetActions(true, $"Create a startup and attach it to group \n\n{groupName}\n\n "
            //+ $"This will cost {Format.Money(startCapital)}"
            );
        }
        else
        {
            SetActions(false, "You have one startup already!\n" +
                "Promote it to group of companies or sell it if you want to start new project");
        }
    }

    void SetActions(bool interactible, string hint)
    {
        StartupButton.GetComponent<Button>().interactable = interactible;
        StartupButton.GetComponent<Hint>().SetHint(hint);
    }
}
