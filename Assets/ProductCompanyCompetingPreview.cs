using Assets.Classes;
using UnityEngine.UI;

public class ProductCompanyCompetingPreview : View, IProductListener
{
    GameEntity Company;

    public Text Name;
    public Text Clients;
    public Text Level;

    public void SetEntity(GameEntity entity)
    {
        Company = entity;
        Company.AddProductListener(this);

        Render();
    }

    void Render()
    {
        ProductComponent p = Company.product;

        Name.text = p.Name;
        Level.text = p.ProductLevel + "";
        Clients.text = "0";
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, IndustryType industry, int productLevel, int explorationLevel, TeamResource resources)
    {
        Render();
    }
}
