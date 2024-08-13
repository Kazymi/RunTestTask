using UnityEngine;

public class InputHelper
{
    private Vector3 mouseLastPosition;
    private Vector3 mouseDirection;

    public float Horizontal => mouseDirection.x;
    public float Vertical => mouseDirection.y;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseLastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDirection = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            mouseDirection = (mousePosition - mouseLastPosition).normalized;
            mouseLastPosition = mousePosition;
        }
    }
}