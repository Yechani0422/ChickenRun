using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChickCount_UI : MonoBehaviour
{
    ChickenCtrl count;
    // Start is called before the first frame update
    void Start()
    {
        count = GameObject.FindWithTag("CHICKEN").GetComponent<ChickenCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "x"+count.babyCount;
    }
}
