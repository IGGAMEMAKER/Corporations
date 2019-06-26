using UnityEngine.EventSystems;

public class SelectCompanyController : ButtonController, IPointerEnterHandler
{
    public int companyId;

    public override void Execute()
    {
        SetSelectedCompany(companyId);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // doing this, because otherwise it triggers onhover sound very often
        if (SelectedCompany.company.Id != companyId)
            Execute();
    }
}
