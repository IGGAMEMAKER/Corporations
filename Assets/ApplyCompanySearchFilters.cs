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

    public Text ValuationLabel;


    bool IsProductFilter => ProductFilter.IsChosen;
    bool IsGroupFilter => GroupFilter.IsChosen;

    bool IsQuarterlyFilter => QuarterlyFilter.IsChosen;

    public void ShowProductCompanies()
    {
        ProductFilter.Toggle(true);
        GroupFilter.Toggle(false);

        RenderLists();
    }

    public void ShowQuarterlyGrowth()
    {
        QuarterlyFilter.Toggle(true);
        YearlyFilter.Toggle(false);

        GrowthFilterQuarterly.Quarterly = true;

        RenderLists();
    }

    public void ShowYearlyGrowth()
    {
        QuarterlyFilter.Toggle(false);
        YearlyFilter.Toggle(true);

        GrowthFilterQuarterly.Quarterly = false;

        RenderLists();
    }

    public void ShowGroupCompanies()
    {
        ProductFilter.Toggle(false);
        GroupFilter.Toggle(true);

        RenderLists();
    }

    void RenderLists()
    {
        bool productOnly = IsProductFilter;

        ProductCompanySearchListView.enabled = productOnly;
        GroupSearchListView.enabled = !productOnly;

        if (productOnly)
            ProductCompanySearchListView.Render();
        else
            GroupSearchListView.Render();

        ValuationLabel.text = $"Valuation\ngrowth\n" + (IsQuarterlyFilter ? "quarter" : "year");


        UpdateLabels();
    }

    void UpdateLabels()
    {
        if (IsProductFilter)
        {
            if (IsQuarterlyFilter)
                SetLabels("Market\nStage", "Audience\ngrowth\nquarterly");
            else
                SetLabels("Market\nStage", "Audience\ngrowth\nyearly");

            return;
        }

        if (IsGroupFilter)
        {
            SetLabels("Sphere of influence\nIndusries", "Sphere of influence\nNiches");
        }
    }

    void SetLabels(string t1, string t2)
    {
        Label1.text = t1;
        Label2.text = t2;
    }
}
