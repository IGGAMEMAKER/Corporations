using UnityEngine;
using UnityEngine.UI;

public class TargetingButtonColorController : View
{
    Image Image;

    private void Start()
    {
        Image = GetComponent<Image>();
    }

    void Update()
    {
        if (myProductEntity.isTargeting)
        {
            Image.color = Color.blue;
        }
        else
        {
            Image.color = Color.white;
        }
    }
}
