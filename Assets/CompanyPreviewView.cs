using Assets.Classes;
using Assets.Utils;
using Entitas;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Visuals;

public class CompanyPreviewView : View, IEventListener, IProductListener, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public GameEntity _entity;
    public Text CompanyNameLabel;
    public Text CompanyTypeLabel;

    public ColoredValuePositiveOrNegative IncomeLabel;
    public Text ShareCostLabel;

    public LinkToProjectView LinkToProjectView;

    public static GameObject itemBeingDragged;

    public void SetEntity(GameEntity entity)
    {
        _entity = entity;

        Render(_entity);
    }

    void RenderCompanyType(GameEntity entity)
    {
        string text;

        switch (entity.company.CompanyType)
        {
            case CompanyType.Product: text = "Product Company"; break;
            case CompanyType.Holding: text = "Holding"; break;
            case CompanyType.Corporation: text = "Corporation"; break;
            case CompanyType.FinancialGroup: text = "Financial Group"; break;
            case CompanyType.Group: text = "Group of companies"; break;
            default: text = "WUT"; break;
        }

        CompanyTypeLabel.text = text;
    }

    void RenderCompanyName(GameEntity entity)
    {
        CompanyNameLabel.text = entity.company.Name;
    }

    void RenderIncome(GameEntity entity)
    {
        IncomeLabel.value = CompanyEconomyUtils.GetIncome(entity, GameContext);
    }

    void Render(GameEntity e)
    {
        RenderCompanyName(e);
        RenderCompanyType(e);
        RenderIncome(e);

        LinkToProjectView.CompanyId = e.company.Id;
    }

    public void OnProduct(GameEntity entity, int id, string name, Niche niche, Industry industry, int productLevel, int explorationLevel, TeamResource resources)
    {
        Debug.Log($"OnProduct.");

        Render(entity);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;

        Debug.Log("OnEndDrag");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering company " + _entity.company.Name);

        if (itemBeingDragged != null && itemBeingDragged != gameObject)
            gameObject.AddComponent<DroppableAnimation>();
    }

    void IEventListener.RegisterListeners(IEntity entity)
    {
        Debug.Log($"RegisterListeners CompanyPreviewView");

        _entity = (GameEntity)entity;
        _entity.AddProductListener(this);
    }
}