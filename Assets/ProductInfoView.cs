using UnityEngine;
using UnityEngine.UI;

public class ProductInfoView : View
{
    public Text Level;
    public ProgressBar ProgressBar;

    public GameObject UpgradeProductProgressBar;
    public GameObject ExploreProductProgressBar;

    void Render()
    {
        AnimateIfValueChanged(Level, myProduct.ProductLevel + "");

        //ProgressBar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
