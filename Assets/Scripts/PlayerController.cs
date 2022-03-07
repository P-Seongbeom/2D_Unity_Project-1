using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip JumpingSound;
    public AudioClip LandingSound;
    public AudioClip DyingSound;
    public float JumpForce = 200;

    private int _jumpCount = 0;
    private bool _onGround = false;
    private bool _isDead = false;

    private Rigidbody2D _playerRigidbody;
    private Animator _animator;
    private AudioSource _playerAudio;
    
    void Start()
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
        }
        else if(Input.GetMouseButtonUp(0) && _playerRigidbody.velocity.y > 0)
        {
            _playerRigidbody.velocity *= 0.5f;
        }

        _animator.SetBool("onGround", _onGround);
    }

    private void Die()
    {
        _animator.SetTrigger("isDead");

        _playerAudio.clip = DyingSound;
        _playerAudio.Play();

        _playerRigidbody.velocity = Vector2.zero;
        _isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dead" && false == _isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            _onGround = true;
            _jumpCount = 0;

            _playerAudio.clip = LandingSound;
            _playerAudio.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _onGround = false;
    }
}
