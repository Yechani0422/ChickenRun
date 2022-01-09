using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FarmerCtrl : MonoBehaviour
{
    public enum FarmerState { idle, trace, attack, tired ,walk};
    public FarmerState farmerState = FarmerState.idle;

    private FarmerAnimCtrl animCtrl;
    private Transform farmerStartTr;
    private Transform farmerTr;
    private Transform chickenTr;
    private UnityEngine.AI.NavMeshAgent nvAgent;

    private float farmerRunGage;
    private bool isFarmerWin=false;

    public float traceDist = 5.0f;
    public float attackDist = 1.1f;
    private bool isDie = false;
    public bool isTrace = true;

    private float timer = 0.0f;
    private float waitTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        farmerTr = GetComponent<Transform>();
        farmerStartTr = GameObject.FindWithTag("STARTPOS").GetComponent<Transform>();
        chickenTr = GameObject.FindWithTag("CHICKEN").GetComponent<Transform>();
        animCtrl = GameObject.Find("Main Camera").GetComponent<FarmerAnimCtrl>();
        nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        nvAgent.destination = chickenTr.position;

        StartCoroutine(this.CheckFarmerState());
        StartCoroutine(this.FarmerAction());
        farmerRunGage = 500.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFarmerWin==true)
        {
            timer += Time.deltaTime;
            if(timer>waitTime)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void OnChickenDie()
    {
        StopAllCoroutines();
        nvAgent.isStopped = true;
        animCtrl.SetInt("animation,3");
        isFarmerWin = true;
    }

    void OnFarmerLose()
    {
        StopAllCoroutines();
        nvAgent.isStopped = true;
        animCtrl.SetInt("animation,7");
    }



    IEnumerator CheckFarmerState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(chickenTr.position, farmerTr.position);

            float dist2 = Vector3.Distance(farmerStartTr.position, farmerTr.position);

            if(dist2<=1.0f)
            {
                isTrace = true;
                farmerRunGage = 500.0f;
            }

            if(dist<=attackDist && farmerRunGage > 0)
            {
                farmerState = FarmerState.attack;
            }
            else if(dist<=traceDist && farmerRunGage>0 && isTrace==true)
            {
                farmerState = FarmerState.trace;
            }
            else if(farmerRunGage<=0)
            {
                farmerState = FarmerState.tired;
            }
            else if(isTrace==false)
            {
                farmerState = FarmerState.walk;
            }
            else
            {
                farmerState = FarmerState.idle;
            }
        }
    }

    IEnumerator FarmerAction()
    {
        while(!isDie)
        {
            switch(farmerState)
            {
                case FarmerState.idle:
                    nvAgent.isStopped = true;
                    animCtrl.SetInt("animation,1");
                    break;
                case FarmerState.trace:
                    nvAgent.destination = chickenTr.position;
                    nvAgent.speed = 3.0f;
                    nvAgent.stoppingDistance = 1.1f;
                    nvAgent.isStopped = false;
                    animCtrl.SetInt("animation,15");
                    farmerRunGage -= 1.0f;
                    yield return new WaitForSeconds(0.01f);
                    break;
                case FarmerState.attack:
                    nvAgent.isStopped = true;
                    animCtrl.SetInt("animation,19");
                    break;
                case FarmerState.tired:
                    nvAgent.isStopped = true;
                    animCtrl.SetInt("animation,22");
                    yield return new WaitForSeconds(3.0f);
                    farmerRunGage = 500.0f;
                    break;
                case FarmerState.walk:
                    nvAgent.destination = farmerStartTr.position;
                    nvAgent.speed = 1.5f;
                    nvAgent.stoppingDistance = 0.0f;
                    nvAgent.isStopped = false;
                    animCtrl.SetInt("animation,18");
                    break;
            }
            yield return null;
        }
    }
}

