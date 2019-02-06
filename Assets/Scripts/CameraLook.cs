using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float mouseSensitivity = 5f;

    private float rotation = 0;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var vertical = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotation -= vertical;

        rotation = Mathf.Clamp(rotation, -85f, 85f);
        transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.right);
    }
}