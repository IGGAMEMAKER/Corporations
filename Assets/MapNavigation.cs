using Assets.Utils;
using UnityEngine;

public class MapNavigation : View
{
    public float Limit = 550f;
    public float Sensitivity = 750f;

    static float X0 = -Screen.width / 2;
    static float Y0 = Screen.height / 2;

    public float X = X0;
    public float Y = Y0;

    public float Zoom = 1f;

    void Update()
    {
        CheckMouseMovement();

        CheckZoom();

        UpdateCanvasPosition();

        RedrawMap();
    }


    float amountOfNichesSqrt;
    private void OnEnable()
    {
        amountOfNichesSqrt = Mathf.Sqrt(NicheUtils.GetNiches(GameContext).Length);
    }

    void UpdateCanvasPosition()
    {
        var Xmax = Screen.width * Zoom;
        var Ymax = Screen.height * Zoom;

        X = Mathf.Clamp(X, X0, Xmax);
        Y = Mathf.Clamp(Y, Y0, Ymax);

        transform.localPosition = new Vector3(X, Y);
        transform.localScale = new Vector3(Zoom, Zoom, 1);
    }

    void CheckZoom()
    {
        var scroll = Input.mouseScrollDelta.y;

        if (scroll == 0)
            return;

        Zoom = Mathf.Clamp(Zoom + scroll / 20, 1f, 5f);

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float modifier = 0.25f;

        X += -(mouseX - Screen.width / 2) * modifier;
        Y += -(mouseY - Screen.height / 2) * modifier;
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
