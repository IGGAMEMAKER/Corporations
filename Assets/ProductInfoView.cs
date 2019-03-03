using UnityEngine.UI;

public class ProductInfoView : View
{
    public Text Level;

    void Render()
    {
        AnimateIfValueChanged(Level, myProduct.ProductLevel + "");
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
