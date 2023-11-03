using UnityEngine;

public class ARModelViewController : MonoBehaviour
{
    private float minValue = 1f;
    private float maxValue = 4f;
    private float rotateSpeed = 180f;
    private Vector2 startingPosition;

    private void Awake()
    {
        minValue = transform.localScale.x * 0.4f;
        maxValue = minValue * 4;
    }

    private void Update()
    {
        UpdateScale();

        if (Input.touchCount == 1)
        {
            UpdateRotation();
        }
    }

    private void UpdateScale()
    {
        if (transform.localScale.x >= maxValue)
        {
            transform.localScale = new Vector3(maxValue, maxValue, maxValue);
        }
        if (transform.localScale.x <= minValue)
        {
            transform.localScale = new Vector3(minValue, minValue, minValue);
        }
    }

    private void UpdateRotation()
    {
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startingPosition = touch.position;
                break;
            case TouchPhase.Moved:
                if (startingPosition.x > touch.position.x)
                {
                    transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
                }
                else if (startingPosition.x < touch.position.x)
                {
                    transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
                }

                //if (startingPosition.y > touch.position.y)
                //{
                //    transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
                //}
                //else if (startingPosition.y < touch.position.y)
                //{
                //    transform.Rotate(Vector3.right, -rotateSpeed * Time.deltaTime);
                //}
                break;
            case TouchPhase.Ended:
                break;
        }
    }
}


