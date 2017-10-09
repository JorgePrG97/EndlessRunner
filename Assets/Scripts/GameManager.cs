using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
            }
            return instance;
        }
    }

    private int distanceTraveled, coinsPicked, enemiesKilled;
    public int EnemiesKilled
    {
        get
        {
            return enemiesKilled;
        }
        set
        {
            enemiesKilled = value;
        }
    }
    public int CoinsPicked
    {
        get
        {
            return coinsPicked;
        }
        set
        {
            coinsPicked = value;
        }
    }
    public Text lifeText;
    public Text scoreText;
    public int score;
    public int Score
    {
        get
        {
            score = distanceTraveled + coinsPicked * 5 + enemiesKilled * 50;
            return score;
        }
        set
        {
            score = value;
        }
    }
    //Otra manera de hacer la relacion
    /*private void Awake()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }*/

    private void Update()
    {
        if(Player.Instance.transform.position.x > distanceTraveled)
        {
            distanceTraveled = (int) Player.Instance.transform.position.x;
        }
    }

    private void LateUpdate()
    {
        scoreText.text = "Score: " + Score;
        lifeText.text = "Life: " + Player.Instance.currentHealth;
    }

    public void Pause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            panel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
