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
        var scrollAxis = "Mouse ScrollWheel";
        //var scrollAxis = "Mouse Y";

        var scroll = Input.GetAxis(scrollAxis);

        if (scroll != 0)
            transform.localScale = new Vector3(1, 1, scroll);
        //transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel"));
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
