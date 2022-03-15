using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public static ItemEffect Instance;

    public PlayerController Player;

    public bool GetGoldBar = false;
    public bool GiantState = false;
    public bool FlyState = false;

    [SerializeField]
    private float _goldenEggTime = 5f;
    [SerializeField]
    private float _giantTime = 5f;
    [SerializeField]
    private float _flyTime = 5f;


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

    public IEnumerator SpawnGoldenEgg()
    {
        GetGoldBar = true;

        yield return new WaitForSeconds(_goldenEggTime);

        GetGoldBar = false;
    }

    public IEnumerator ChangToGiant()
    {
        GiantState = true;

        yield return StartCoroutine(Player.IncreasePlayerSize());

        yield return new WaitForSeconds(_giantTime);

        yield return StartCoroutine(Player.DecreasePlayerSize());

        GiantState = false;
    }

    public IEnumerator Flying()
    {
        FlyState = true;

        yield return new WaitForSeconds(_flyTime);

        FlyState = false;

    }
}
