using UnityEngine.UI;

public class ProductLevelView : View
    , IAnyDateListener
{
    Text Level;

    void OnEnable()
    {
        // TODO Update
        LazyUpdate(this);

        Render();
    }

    void Render()
    {
        Level = GetComponent<Text>();
        AnimateIfValueChanged(Level, MyProduct.ProductLevel + " LVL");
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
