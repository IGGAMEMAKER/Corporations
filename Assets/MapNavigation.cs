using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float mul = Time.deltaTime * 750f;

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
        transform.position += Vector3.right * mul;
    }

    void MoveLeft(float mul)
    {
        transform.position += Vector3.left * mul;
    }

    void MoveUp(float mul)
    {
        transform.position += Vector3.up * mul;
    }

    void MoveDown(float mul)
    {
        transform.position += Vector3.down * mul;
    }
}
