using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCtrl : MonoBehaviour
{
    public enum ChickenState { idle, walk ,run};
    public ChickenState chickenState = ChickenState.idle;

    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 1.0f;
    public float rotSpeed = 100.0f;

    public float m_fJumpForce = 1.0f;
    public bool jumpCheck = false;

    private Animator animator;

    private bool isDie = false;
    public bool isWalk = false;
    public bool isRun = false;

    public int babyCount=0;

    private BabyChickMoveMent deleteBabyTransform;

    GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = this.gameObject.GetComponent<Animator>();
        spawner = GameObject.Find("ChickSpawn");
        deleteBabyTransform = GameObject.Find("ChickSpawn").GetComponent<BabyChickMoveMent>();

        StartCoroutine(this.CheckChickenState());
        StartCoroutine(this.ChickenAction());
    }

    // Update is called once per frame
    void Update()
    {
        if(isDie==false)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            //캐릭터 이동
            if (h != 0 || v != 0)
            {
                Rotate(h, v);

                tr.Translate(Vector3.forward * Time.smoothDeltaTime * moveSpeed, Space.Self);

                if (isRun == false)
                {
                    isWalk = true;
                }
                //달리기           
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    moveSpeed = 2.0f;
                    isWalk = false;
                    isRun = true;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    moveSpeed = 1.0f;
                    isRun = false;
                }
            }
            else
            {
                isWalk = false;
                isRun = false;
            }

            //점프
            if (jumpCheck == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Rigidbody rigidibody = this.gameObject.GetComponent<Rigidbody>();
                    rigidibody.AddForce(Vector3.up * m_fJumpForce);
                    jumpCheck = true;
                }
            }
        }
    }

    

    void Rotate(float h,float v)
    {
        //캐릭터가 보는방향
        Vector3 dir = new Vector3(h, 0, v).normalized;

        Quaternion rot = Quaternion.identity; // Quaternion 값을 저장할 변수 선언 및 초기화

        rot.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0); // 역시 eulerAngles를 이용한 오일러 각도를 Quaternion으로 저장

        tr.transform.transform.rotation = rot;
    }
 

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag=="FLOOR")
        {
            jumpCheck = false;
        }

        if (coll.gameObject.tag == "CHICK")
        {
            babyCount += 1;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
       // if(babyCount>0)
        {
            if (coll.gameObject.tag == "FARMERATTACK")
            {
                Debug.Log("ouch!!!!!!!!");
                int i = spawner.transform.childCount;
                if (i > 0)
                {
                    Destroy(spawner.transform.GetChild(i - 1).gameObject);
                    deleteBabyTransform.BodyParts.RemoveAt(i);
                    babyCount -= 1;
                }
                Debug.Log(spawner.transform.childCount);
                if (spawner.transform.childCount == 0)
                {
                    ChickenDie();
                }
            }
        }
    }

    void ChickenDie()
    {
        Debug.Log("ChickenDie!!");

        isDie = true;

        GameObject farmer = GameObject.FindGameObjectWithTag("FARMER");

        farmer.SendMessage("OnChickenDie", SendMessageOptions.DontRequireReceiver);
    }


    IEnumerator  CheckChickenState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);


            if (isWalk == true)
            {
                chickenState = ChickenState.walk;
            }
            else if (isRun == true)
            {
                chickenState = ChickenState.run;
            }
            else
            {
                chickenState = ChickenState.idle;
            }

        }
    }

    IEnumerator ChickenAction()
    {
        while(!isDie)
        {
            switch(chickenState)
            {
                case ChickenState.idle:
                    animator.SetBool("IsWalk", false);
                    break;
                case ChickenState.walk:
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsWalk", true);
                    break;
                case ChickenState.run:
                    animator.SetBool("IsRun", true);
                    break;
            }
            yield return null;
        }
    }
}
