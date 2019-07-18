using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNavigation : MonoBehaviour, IScrollHandler
{
    public float Limit = 550f;
    public float Sensitivity = 750f;

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

        if (mouseY > Screen.height - minOffset)
            MoveUp(mul);
    }



    void MoveRight(float mul)
    {
        if (transform.position.x < Limit)
            transform.position += Vector3.right * mul;
    }

    void MoveLeft(float mul)
    {
        if (transform.position.x > -Limit)
            transform.position += Vector3.left * mul;
    }

    void MoveUp(float mul)
    {
        if (transform.position.y > -Limit)
            transform.position += Vector3.down * mul;
    }

    void MoveDown(float mul)
    {
        if (transform.position.y < Limit)
            transform.position += Vector3.up * mul;
    }

    void IScrollHandler.OnScroll(PointerEventData eventData)
    {
        Debug.Log("Is scrolling");
    }
}
