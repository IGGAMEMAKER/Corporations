using Entitas;
using UnityEngine;

public class BusinessScreenView : View
{
    public GameObject Companies;
    GameEntity SelectedEntity;

    // Start is called before the first frame update
    void Awake()
    {
        if (myProductEntity == null)
            Debug.Log("no companies controlled");
    }

    void Render()
    {
        //SelectedEntity = GameContext.GetEntities(GameMatcher.Selected)[0];

        //Companies.transform.GetChild(0).GetComponent<CompanyPreviewView>().SetEntity(SelectedEntity);
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
