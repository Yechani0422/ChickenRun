using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    public Transform targetTr;
    public float height = 40.0f;
    public float dampTrace = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
           targetTr.position-(Vector3.up*-height), Time.deltaTime*dampTrace);
    }
}
