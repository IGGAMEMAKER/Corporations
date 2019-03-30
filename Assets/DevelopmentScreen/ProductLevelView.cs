using UnityEngine.UI;

public class ProductLevelView : View
{
    Text Level;

    private void Start()
    {
        Level = GetComponent<Text>();
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        AnimateIfValueChanged(Level, myProduct.ProductLevel + "");
    }
}
