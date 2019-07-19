using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        Zoom = Mathf.Clamp(Zoom + scroll / 20, 0.5f, 5f);

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        X += -(mouseX - Screen.width / 2);
        Y += -(mouseY - Screen.height / 2);

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
            MoveRight(mul);

        // right
        if (mouseX > Screen.width - minOffset)
            MoveLeft(mul);

        // top
        if (mouseY < minOffset)
            MoveDown(mul);

        // bottom
        if (mouseY > Screen.height - minOffset)
            MoveUp(mul);
    }



    void MoveRight(float mul)
    {
        X += mul;
    }

    void MoveLeft(float mul)
    {
        X -= mul;
    }

    void MoveUp(float mul)
    {
        Y -= mul;
    }

    void MoveDown(float mul)
    {
        Y += mul;
    }
}
