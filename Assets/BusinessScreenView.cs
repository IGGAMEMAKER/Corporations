using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class BusinessScreenView : View
{
    public GameObject Companies;
    public GameObject CompnayPrefab;
    public Text CompanyNameLabel;

    void Awake()
    {
        if (myProductEntity == null)
            Debug.Log("no companies controlled");
    }

    void Update()
    {
        Render();
    }

    void RenderSelectedCompanyName()
    {
        CompanyNameLabel.text = SelectedCompany.company.Name;
    }

    void Render()
    {
        RenderSelectedCompanyName();

        int index = 0;

        var companies = GameContext.GetEntities(GameMatcher.Company);

        foreach (var e in companies)
        {
            Transform c;

            if (index < Companies.transform.childCount - 2)
                c = Companies.transform.GetChild(index);
            else
            {
                c = Instantiate(CompnayPrefab, Companies.transform, false).transform;
                c.SetSiblingIndex(index);
            }

            c.gameObject.GetComponent<CompanyPreviewView>().SetEntity(e);
            index++;
        }
    }
}
