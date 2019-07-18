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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseMovement();

        CheckZoom();
    }

    void CheckZoom()
    {
        //var scrollAxis = "Mouse ScrollWheel";
        var scrollAxis = "Vertical";
        //var scrollAxis = "Mouse Y";

        //var scroll = Input.GetAxis(scrollAxis);
        var scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            Zoom = Mathf.Clamp(Zoom + scroll / 10, 0.5f, 5f);
            //transform.localScale = new Vector3(Zoom, Zoom, 1);

            RedrawMap();
        }
        //transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel"));
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

        transform.position = new Vector3(X, Y);
    }



    void MoveRight(float mul)
    {
        if (transform.position.x < Limit)
            X += mul;
    }

    void MoveLeft(float mul)
    {
        if (transform.position.x > -Limit)
            X -= mul;
    }

    void MoveUp(float mul)
    {
        if (transform.position.y > -Limit)
            Y -= mul;
    }

    void MoveDown(float mul)
    {
        if (transform.position.y < Limit)
            Y += mul;
    }

    //void IScrollHandler.OnScroll(PointerEventData eventData)
    //{
    //    Debug.Log("Is scrolling");
    //}
}
