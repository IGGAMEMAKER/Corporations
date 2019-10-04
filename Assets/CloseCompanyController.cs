using Assets.Utils;
using UnityEngine.UI;

public class CloseCompanyController : PopupButtonController
{
    public override void Execute()
    {
        var popup = NotificationUtils.GetPopupMessage(GameContext) as PopupMessageCloseCompany;

        CompanyUtils.CloseCompany(GameContext, popup.companyId);

        Navigate(ScreenMode.GroupManagementScreen);

        //var daughters = CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id);

        //if (daughters.Length > 0)
        //{
        //    // pick another daughter company
        //    NavigateToCompany(CurrentScreen, daughters[0].company.Id);
        //}
        //else
        //{

        //    NavigateToProjectScreen(MyCompany.company.Id);
        //}
    }

    public override string GetButtonName()
    {
        return "YES";
    }
}

public abstract class PopupButtonController : ButtonController
{
    public abstract string GetButtonName();

    void OnEnable()
    {
        Initialize();

        SetButtonName(GetButtonName());
    }

    public virtual void SetButtonName(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }
}