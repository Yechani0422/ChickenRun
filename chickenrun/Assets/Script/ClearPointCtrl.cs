using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPointCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "CHICKEN")
        {
            Debug.Log("ChickenWin!!");
            

            GameObject farmer = GameObject.FindGameObjectWithTag("FARMER");

            farmer.SendMessage("OnFarmerLose", SendMessageOptions.DontRequireReceiver);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
