using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float speed = 8f;

    [SerializeField]
    private bool isGrounded = false;

    private Ray ray;
    private Ray normal = new Ray();
    private Ray projection = new Ray();

    [SerializeField]
    private float rotation = 0f;

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        
        ray = new Ray(transform.position, transform.up * -1f);

        if (Physics.Raycast(ray, out hitInfo, 2f))
        {
            normal = new Ray(hitInfo.point, hitInfo.normal);
            projection = new Ray(hitInfo.point, Vector3.ProjectOnPlane(transform.forward, hitInfo.normal));

            if (transform.up != normal.direction)
            {
                transform.rotation = Quaternion.LookRotation(projection.direction, normal.direction);
                transform.position = hitInfo.point + transform.up;
            }
        }

        var rot = Input.GetAxis("Mouse X") * 5f;
        var ver = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        rotation += rot;
        transform.rotation = Quaternion.AngleAxis(rot, transform.up) * transform.rotation;
        
        //transform.position += transform.right * hor * speed * Time.deltaTime;
        transform.position += transform.forward * ver * speed * Time.deltaTime;
        transform.position += transform.right * hor * speed * Time.deltaTime;

        //var x = Input.GetAxis("Mouse X") * 100f;
        //Debug.Log(x);
        //transform.rotation = Quaternion.AngleAxis(x, transform.up);
    }

    private void OnDrawGizmos()
    {
        // Projection
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(projection.origin, projection.origin + projection.direction);

        // Normal
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(normal.origin, normal.origin + normal.direction);

        // Face raycast
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction);

    }
}
