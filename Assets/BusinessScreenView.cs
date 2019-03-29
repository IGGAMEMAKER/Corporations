using Entitas;
using UnityEngine;

public class BusinessScreenView : View
{
    public GameObject Companies;

    // Start is called before the first frame update
    void Awake()
    {
        if (myProductEntity == null)
            Debug.Log("no companies controlled");
    }

    void Render()
    {
        int index = 0;

        var companies = GameContext.GetEntities(GameMatcher.Company);

        Debug.Log("Amount of companies " + companies.Length);

        foreach (var e in companies)
        {
            Companies.transform.GetChild(index).GetComponent<CompanyPreviewView>().SetEntity(e);
            index++;
        }

        //Companies.transform.GetChild(index).GetComponent<CompanyPreviewView>().SetEntity(myProductEntity);
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
