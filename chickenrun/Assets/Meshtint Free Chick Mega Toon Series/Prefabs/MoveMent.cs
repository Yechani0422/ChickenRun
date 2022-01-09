using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();

    public float mindistance = 0.25f;

    public int beginsize;

    public float speed = 1.0f;
    public float rotationspeed = 50.0f;

    public GameObject bodyprefab;

    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyParts;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<beginsize-1;i++)
        {
            AddBodyPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.Q))
        {
            AddBodyPart();
        }
    }

    public void Move()
    {
        float curspeed = speed;

        if (Input.GetKey(KeyCode.W))
            curspeed *= 2;

        BodyParts[0].Translate(BodyParts[0].forward * curspeed*Time.smoothDeltaTime,Space.World);

        if(Input.GetAxis("Horizontal") !=0)
        {
            BodyParts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        for (int i = 1; i < BodyParts.Count;i++)
        {
            curBodyPart = BodyParts[i];
            PrevBodyParts = BodyParts[i - 1];

            dis = Vector3.Distance(PrevBodyParts.position, curBodyPart.position);

            Vector3 newpos = PrevBodyParts.position;

            newpos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dis / mindistance * curspeed;

            if(T>0.5f)
            {
                T = 0.5f;
            }
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, 0.05f);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyParts.rotation,0.05f);
        }
    }

    public void AddBodyPart()
    {
        Transform newpart = (Instantiate(bodyprefab,BodyParts[BodyParts.Count-1].position, BodyParts[BodyParts.Count-1].rotation) as GameObject).transform;
        newpart.SetParent(transform);

        BodyParts.Add(newpart);
    }
}
