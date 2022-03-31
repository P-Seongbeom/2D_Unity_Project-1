using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip JumpingSound;
    public AudioClip LandingSound;
    public AudioClip DyingSound;

    public bool GiantState = false;
    public bool FlyState = false;

    public float _typeFixTime = 5f;
    public float JumpForce = 300;

    private int _jumpCount = 0;
    private bool _onGround = true;
    private bool _isDead = false;
    [SerializeField]
    private float _scaleSpeed = 1f;
    [SerializeField]
    private Vector3 _maxScale = new Vector3(3f, 3f, 0);
    [SerializeField]
    private Vector3 _minScale = new Vector3(1f, 1f, 0);

    private Rigidbody2D _playerRigidbody;
    private Animator _animator;
    private AudioSource _playerAudio;
    private SpriteRenderer _playerSprite;
    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_isDead)
        {
            return;
        }

        if(false == FlyState)
        {
            Jump();
        }
        else if(FlyState)
        {
            Flying();
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && _jumpCount < 2)
        {
            _onGround = false;
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
        if (collision.tag == "Obstacle" && false == _isDead)
        {
            if (false == GiantState)
            {
                Die();
            }
            else if (GiantState)
            {
                _playerAudio.clip = LandingSound;
                _playerAudio.Play();
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.tag == "Dead" && false == _isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f && false == _onGround && _animator.GetBool("isJump") )
        {
            if(collision.collider.tag == "Ground")
            {
                _animator.SetBool("isJump", false);
                _jumpCount = 0;

                _onGround = true;
                _playerAudio.clip = LandingSound;
                _playerAudio.Play();
            }
        }
    }

    public void Giantize(float duration)
    {
        StartCoroutine(GiantizeHelper(duration));
    }

    public IEnumerator GiantizeHelper(float duration)
    {
        if(false == GiantState)
        {
            GiantState = true;

            StartCoroutine(IncreasePlayerSize());

            yield return new WaitForSeconds(duration);

            yield return StartCoroutine(DecreasePlayerSize());

            GiantState = false;
        }
    }

    public IEnumerator IncreasePlayerSize()
    {
        while(transform.localScale.x < _maxScale.x && transform.localScale.y < _maxScale.y)
        {
            transform.localScale = new Vector3(transform.localScale.x + _scaleSpeed * Time.deltaTime, transform.localScale.y + _scaleSpeed * Time.deltaTime, 0);

            yield return null;
        }

        transform.localScale = _maxScale;
    }

    public IEnumerator DecreasePlayerSize()
    {
        while (transform.localScale.x > _minScale.x && transform.localScale.y > _minScale.y)
        {
            transform.localScale = new Vector3(transform.localScale.x - _scaleSpeed * Time.deltaTime, transform.localScale.y - _scaleSpeed * Time.deltaTime, 0);

            yield return null;
        }

        transform.localScale = _minScale;
    }

    public void ChangeToFly(float duration)
    {
        StartCoroutine(FlyingHelper(duration));
    }

    public IEnumerator FlyingHelper(float duration)
    {
        FlyState = true;

        ChangeBodyColor(Color.yellow);

        yield return new WaitForSeconds(duration);

        ChangeBodyColor(Color.white);

        FlyState = false;
    }

    public void Flying()
    {
        _jumpCount = 0;

        if (Input.GetMouseButton(0))
        {
            _onGround = false;
            _playerRigidbody.velocity = Vector2.zero;
            Vector3 reposition = new Vector3(0f, 3f * Time.deltaTime, 0f);
            transform.position += reposition;
        }
    }

    public void ChangeBodyColor(Color color)
    {
        _playerSprite.material.color = color;
    }
}
