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
    public float AddValue = 100;
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
            AddScore();
        }
        else if(GameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    public void AddScore()
    {
        if(!GameOver)
        {
            AddValue += AddValue * ValueRate * Time.deltaTime;

            _score += AddValue * Time.deltaTime;

            ScoreText.text = "Score : " + (int)_score;

            //Debug.Log($"{AddValue}");
        }
    }

    public void PlayerDead()
    {
        GameOver = true;
        GameOverUI.SetActive(true);
    }
}
