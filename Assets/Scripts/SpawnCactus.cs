using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCactus : MonoBehaviour
{
    public GameObject GroundGroup;
    public Transform[] Grounds;
    public Transform[] Cactuses;

    public float ScrollSpeed;
    public float AddSpeedRate = 0f;

    public string OnTag = "Ground";

    void Start()
    {
        Cactuses = GetComponentsInChildren<Transform>();

        foreach(Transform child in Cactuses)
        {
            if (child.name != transform.name)
            {
                child.gameObject.SetActive(false);
            }
        }

        if(GroundGroup.tag == OnTag)
        {
            Grounds = GroundGroup.GetComponentsInChildren<Transform>();
        }
    }


    void Update()
    {
        //SetCactus();

        ScrollCactus();
    }

    void SetCactus()
    {
        int cactusIndex = Random.Range(1, Cactuses.Length - 1);
        //int groundIndex = Random.Range(1, Grounds.Length - 1);

        //if (Grounds[groundIndex].gameObject.activeSelf && false == Cactuses[cactusIndex].gameObject.activeSelf)
        //{
        //    Vector2 reposition = new Vector2(Grounds[groundIndex].position.x, 0);
        //    Cactuses[cactusIndex].position = reposition;
        //    Cactuses[cactusIndex].gameObject.SetActive(true);
        //}

        if (false == Cactuses[cactusIndex].gameObject.activeSelf)
        {
            //Vector2 reposition = new Vector2(Grounds[groundIndex].position.x, 0);
            //Cactuses[cactusIndex].position = reposition;
            Cactuses[cactusIndex].gameObject.SetActive(true);
        }
    }

    void ScrollCactus()
    {
        AddScrollSpeed(AddSpeedRate);

        foreach (Transform child in Cactuses)
        {
            if (child.name != transform.name)
            {
                child.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

                if (child.position.x < (-9f))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void AddScrollSpeed(float rate)
    {
        ScrollSpeed += ScrollSpeed * rate * Time.deltaTime;
    }
}
