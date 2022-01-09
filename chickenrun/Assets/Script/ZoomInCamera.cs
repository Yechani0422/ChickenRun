using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 3.0f;
    public float dampTrace = 20.0f;
    public float distanceMin = .5f;
    public float distanceMax = 15f;

    // Use this for initialization
    void Start()
    {
    }

    void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position,
            target.position - (Vector3.forward * distance) + (Vector3.up * height),
            Time.deltaTime * dampTrace);

            transform.LookAt(target.position);
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            height = Mathf.Clamp(height - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

           
        }
    }

}
