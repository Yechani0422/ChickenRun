using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldChick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fieldChick;
    private BabyChickMoveMent moveMentScript;
    void Start()
    {
        moveMentScript = GameObject.Find("ChickSpawn").GetComponent<BabyChickMoveMent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "CHICKEN")
        {
            moveMentScript.AddBodyPart();
            Destroy(fieldChick);
        }
    }
}
