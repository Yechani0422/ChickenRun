using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFollowCam : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 5.0f;
    public float height = 3.0f;
    public float dampTrace = 20.0f;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        tr.position = Vector3.Lerp(tr.position,
            targetTr.position - (Vector3.back * dist) + (Vector3.up * height),
            Time.deltaTime * dampTrace);

        tr.LookAt(targetTr.position);

    }
}
