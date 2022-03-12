using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform Player;

    [SerializeField]
    private float Distance = 3.5f;

    void Update()
    {
        Vector3 setPosition = new Vector3(Player.position.x + Distance, transform.position.y, transform.position.z);

        transform.position = setPosition;
    }
}
