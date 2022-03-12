using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool GameOver;
    public Text ScoreText;
    public GameObject GameOverUI;

    private float _score = 0;
    private float _itemScore;
    public float AddTimeValue = 10;
    public float ValueRate;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        if (false == GameOver)
        {
            AddTimeScore();

            ScoreText.text = "Score : " + (int)_score;
        }
        else if(GameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    public void AddTimeScore()
    {
        if(false == GameOver)
        {
            AddTimeValue += AddTimeValue * ValueRate * Time.deltaTime;

            _score += AddTimeValue * Time.deltaTime;
        }
    }

    public void AddItemScore(float addValue)
    {
        if(false == GameOver)
        {
            _itemScore = addValue;
            _score += _itemScore;
        }
    }

    public void PlayerDead()
    {
        GameOver = true;
        GameOverUI.SetActive(true);
    }
}
