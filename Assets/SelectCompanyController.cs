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
        Execute();
    }
}
