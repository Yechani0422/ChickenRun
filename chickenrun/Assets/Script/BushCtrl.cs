using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCtrl : MonoBehaviour
{
    private FarmerCtrl isTrace;
    // Start is called before the first frame update
    void Start()
    {
        isTrace = GameObject.FindWithTag("FARMER").GetComponent<FarmerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "CHICKEN")
        {
            isTrace.isTrace = false;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.r,
                    this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.g,
                    this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.b, 0.3f);
            }
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "CHICKEN")
        {
            isTrace.isTrace = false;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.r,
                    this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.g,
                    this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.b, 0.3f);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "CHICKEN")
        {
            isTrace.isTrace = true;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.r,
                    this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.g,
                    this.transform.GetChild(i).GetComponent<MeshRenderer>().material.color.b, 1.0f);
            }
        }
    }
}
