using UnityEngine.UI;

public class ProductLevelView : View
    , IAnyDateListener
{
    Text Level;

    private void Start()
    {
        Level = GetComponent<Text>();

        ListenDateChanges(this);
    }

    void OnEnable()
    {
        // TODO Update

        Render();
    }

    void Render()
    {
        AnimateIfValueChanged(Level, MyProduct.ProductLevel + " LVL");
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
