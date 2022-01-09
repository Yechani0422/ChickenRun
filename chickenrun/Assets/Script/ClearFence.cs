using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFence : MonoBehaviour
{

    ChickenCtrl Chicken;
    // Start is called before the first frame update
    void Start()
    {
        Chicken = GameObject.FindWithTag("CHICKEN").GetComponent<ChickenCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Chicken.babyCount>=5)
        {
            foreach(Collider coll in gameObject.GetComponentsInChildren<BoxCollider>())
            {
                coll.enabled = false;
            }
        }
    }
}
