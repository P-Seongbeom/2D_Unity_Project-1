using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem : Item
{
    public float _flyTime = 5f;

    private GameObject _player;
    void Start()
    {
        _player = GameObject.Find("Player");
    }
    protected override void Activate()
    {
        GameManager.Instance.AddItemScore(_itemData.Score);
        _player.GetComponent<PlayerController>().ChangeToFly(_flyTime);
    }
}
