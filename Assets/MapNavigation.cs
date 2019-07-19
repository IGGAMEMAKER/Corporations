using UnityEngine;

public class MapNavigation : MonoBehaviour
    //, IScrollHandler
{
    public float Limit = 550f;
    public float Sensitivity = 750f;

    float X;
    float Y;

    public float Zoom = 1f;

    void Update()
    {
        CheckMouseMovement();

        CheckZoom();

        X = Mathf.Clamp(X, -Limit, Limit);
        Y = Mathf.Clamp(Y, -Limit, Limit);

        transform.position = new Vector3(X, Y);
    }

    void CheckZoom()
    {
        var scroll = Input.mouseScrollDelta.y;

        if (scroll == 0)
            return;

        Zoom = Mathf.Clamp(Zoom + scroll / 50, 0.5f, 5f);

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float modifier = 0.25f;

        X += -(mouseX - Screen.width / 2) * modifier;
        Y += -(mouseY - Screen.height / 2) * modifier;

        RedrawMap();
    }

    MarketMapRenderer map;
    void RedrawMap()
    {
        if (map == null)
            map = GetComponent<MarketMapRenderer>();

        map.ViewRender();
    }

    void CheckMouseMovement()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float mul = Time.deltaTime * Sensitivity;

        float minOffset = 15f;

        // left 
        if (mouseX < minOffset)
            X += mul;

        // right
        if (mouseX > Screen.width - minOffset)
            X -= mul;

        // top
        if (mouseY < minOffset)
            Y += mul;

        // bottom
        if (mouseY > Screen.height - minOffset)
            Y -= mul;
    }
}
