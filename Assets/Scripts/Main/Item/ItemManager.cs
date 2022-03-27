using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public PlayerController Player;

    public bool TypeFix = false;
    public bool GiantState = false;
    public bool FlyState = false;

    public float _typeFixTime = 5f;
    public float _giantTime = 5f;
    public float _flyTime = 5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
