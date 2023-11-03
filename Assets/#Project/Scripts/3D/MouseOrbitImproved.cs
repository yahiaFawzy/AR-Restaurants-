using UnityEngine;
using System.Collections;

public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;

    [Header("Camera Sitting For Rotation")]
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    [Header("Camera Sitting For Zoom")]
    public float zoomSpeed = 1f;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    private Rigidbody rigidbody;
    private Vector3 mouseWorldPosStart;

    float x = 0.0f;
    float y = 0.0f;

    bool orbitable;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
        Rotate();
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            orbitable = true;
        else if (Input.GetMouseButtonUp(0))
            orbitable = false;

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        else if (orbitable)
        {
            if (target)
            {
                Rotate();
            }
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        Debug.Log("Restrants -> x input" + x);
        Debug.Log("Restrants -> y input" + y);

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Debug.Log("Restrants -> rotation " + rotation);

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit))
        {
            distance -= hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    void Zoom(float increment)
    {
        this.GetComponent<Camera>().orthographicSize = Mathf.Clamp(this.GetComponent<Camera>().orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}