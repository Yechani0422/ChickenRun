using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
    public enum ChickState { idle, walk, run };
    public ChickState chickState = ChickState.idle;
    private Animator animator;
    
    private ChickenCtrl CC;

    private bool isDie = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        CC = GameObject.Find("Toon Chicken").GetComponent<ChickenCtrl>();

        StartCoroutine(this.CheckChickState());
        StartCoroutine(this.ChickAction());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator CheckChickState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);


            if (CC.isWalk == true)
            {
                chickState = ChickState.walk;
            }
            else if (CC.isRun == true)
            {
                chickState = ChickState.run;
            }
            else
            {
                chickState = ChickState.idle;
            }
        }
    }

    IEnumerator ChickAction()
    {
        while (!isDie)
        {
            switch (chickState)
            {
                case ChickState.idle:
                    animator.SetBool("IsWalk", false);
                    break;
                case ChickState.walk:
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsWalk", true);
                    break;
                case ChickState.run:
                    animator.SetBool("IsRun", true);
                    break;
            }
            yield return null;
        }
    }
}
