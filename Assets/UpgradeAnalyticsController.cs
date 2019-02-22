using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;

public class UpgradeAnalyticsController : MonoBehaviour
{
    Button Button;

    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(OnUpgradeAnalytics);
    }

    void OnUpgradeAnalytics()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ProductDevelopmentSystem : ComponentSystem
{
    struct Components
    {
        public Product2 Product;
    }

    protected override void OnUpdate()
    {
        foreach (var e in GetEntities<Components>())
        {
            //e.Product.BrandPower++;
        }
    }
}
