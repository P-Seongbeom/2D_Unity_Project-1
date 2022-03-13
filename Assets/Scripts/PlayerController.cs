using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip JumpingSound;
    public AudioClip LandingSound;
    public AudioClip DyingSound;
    public float JumpForce = 300;

    private int _jumpCount = 0;
    private bool _isDead = false;

    private Rigidbody2D _playerRigidbody;
    private Animator _animator;
    private AudioSource _playerAudio;

    public ItemData ItemData;
    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_isDead)
        {
            return;
        }

        Jump();

    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && _jumpCount < 2)
        {
            ++_jumpCount;

            _playerRigidbody.velocity = Vector2.zero;
            _playerRigidbody.AddForce(new Vector2(0, JumpForce));

            _playerAudio.clip = JumpingSound;
            _playerAudio.Play();

            _animator.SetBool("isJump", true);
        }
        else if(Input.GetMouseButtonUp(0) && _playerRigidbody.velocity.y > 0)
        {
            _playerRigidbody.velocity *= 0.5f;
        }
    }

    private void Die()
    {
        _animator.SetTrigger("isDead");

        _playerAudio.clip = DyingSound;
        _playerAudio.Play();

        _playerRigidbody.velocity = Vector2.zero;
        _isDead = true;

        GameManager.Instance.PlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("¾ÆÀÌÅÛ");
            //Debug.Log(collision.gameObject.transform.parent);

            string typeName = collision.gameObject.transform.parent.name;
            //ItemPool.Instance.GetScore((ItemType)typeName);
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), typeName);
            Debug.Log(type);
            ItemPool.Instance.GetScore(type);
            //collision.gameObject.transform.parent.name
            //collision.gameObject.transform.GetChild(0).name
            //GameManager.Instance.AddItemScore(ItemData.Score);
        }

        if (collision.tag == "Dead" && false == _isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f && _animator.GetBool("isJump") == true)
        {
            if(collision.collider.tag == "Ground")
            {
                _animator.SetBool("isJump", false);
                _jumpCount = 0;

                _playerAudio.clip = LandingSound;
                _playerAudio.Play();
            }

        }

        if(collision.collider.tag == "Obstacle" && false == _isDead)
        {
            Die();
        }
    }
}
