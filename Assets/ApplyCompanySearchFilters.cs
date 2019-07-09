using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyCompanySearchFilters : MonoBehaviour
{
    public ProductCompanySearchListView ProductCompanySearchListView;
    public GroupSearchListView GroupSearchListView;
    public GrowthFilterQuarterly GrowthFilterQuarterly;

    public IsChosenComponent ProductFilter;
    public IsChosenComponent GroupFilter;
    
    public IsChosenComponent QuarterlyFilter;
    public IsChosenComponent YearlyFilter;



    public Text Label1;
    public Text Label2;

    public void ShowProductCompanies()
    {
        ProductCompanySearchListView.enabled = true;
        GroupSearchListView.enabled = false;

        ProductFilter.Toggle(true);
        GroupFilter.Toggle(false);

        ProductCompanySearchListView.Render();

        if (QuarterlyFilter.IsChosen)
            SetLabels("Market\nStage", "Audience\ngrowth\nquarterly");
        else
            SetLabels("Market\nStage", "Audience\ngrowth\nyearly");
    }

    public void ShowQuarterlyGrowth()
    {
        QuarterlyFilter.Toggle(true);
        YearlyFilter.Toggle(false);

        GrowthFilterQuarterly.enabled = true;
    }

    public void ShowYearlyGrowth()
    {
        QuarterlyFilter.Toggle(false);
        YearlyFilter.Toggle(true);

        GrowthFilterQuarterly.enabled = false;
    }

    void SetLabels(string t1, string t2)
    {
        Label1.text = t1;
        Label2.text = t2;
    }

    public void ShowGroupCompanies()
    {
        ProductCompanySearchListView.enabled = false;
        GroupSearchListView.enabled = true;

        ProductFilter.Toggle(false);
        GroupFilter.Toggle(true);

        GroupSearchListView.Render();

        SetLabels("Sphere of influence\nIndusries", "Sphere of influence\nNiches");
    }
}
