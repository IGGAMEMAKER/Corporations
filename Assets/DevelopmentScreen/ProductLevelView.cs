using UnityEngine.UI;

// TODO REMOVE
public class ProductLevelView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var Text = GetComponent<Text>();

        //AnimateIfValueChanged(Text, MyProduct.ProductLevel + " LVL");
    }
}
