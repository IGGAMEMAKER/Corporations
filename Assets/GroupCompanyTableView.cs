using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupCompanyTableView : View
{
    [SerializeField] Text CompanyTypeName;
    [SerializeField] Text NicheFocus;
    [SerializeField] Text IndustryFocus;

    GameEntity entity;

    public void SetEntity(GameEntity company)
    {
        entity = company;

        Render();
    }

    public void Render()
    {
        if (entity == null)
            return;

        CompanyTypeName.text = $"({EnumUtils.GetFormattedCompanyType(entity.company.CompanyType)}";

        NicheFocus.text = System.String.Join(",", entity.companyFocus.Niches);
        IndustryFocus.text = System.String.Join(",", entity.companyFocus.Industries);
    }
}
