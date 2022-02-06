using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Brick BrickPrefab;
    [SerializeField] int LineCount = 6;
    [SerializeField] Rigidbody Ball;

    [SerializeField] Text ScoreText;
    [SerializeField] Text BestScoreText;
    [SerializeField] GameObject GameOverText;

    private bool m_Started = false;
    private bool m_GameOver = false;
    
    //Temporary
    public int m_Points;
    string currentPlayerName;

    //Persistent
    string highScoreName;
    int highScore;


    // Start is called before the first frame update
    void Start()
    {
        GameOverText.SetActive(false);
        currentPlayerName = DataManager.instance.playerName;
  
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        UpdateHighScoreText();

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayAgain();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
    }

    public void GameOver()
    {
        m_GameOver = true;
        //compare scores
        SubmitScore(m_Points);
        GameOverText.SetActive(true);

    }
    void SubmitScore(int pts)
    {
        DataManager.instance.CheckScore(pts);
    }

    void UpdateHighScoreText()
    {
        DataManager.instance.LoadHighScores();
 
        highScoreName = DataManager.instance.GetHighScoreName();
        highScore = DataManager.instance.GetHighScore();

        if(!m_GameOver && highScore < m_Points)
        {
            string highScoreText = "High score: " + currentPlayerName + " (" + m_Points + ")";
            BestScoreText.text = highScoreText;
            ScoreText.text = " ";
        }
        else if (!m_GameOver)
        {
            string highScoreText = "High score: " + highScoreName + " (" + highScore + ")";
            BestScoreText.text = highScoreText;
            ScoreText.text = currentPlayerName + " " + $"score : {m_Points}";
        }
    }
    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void ViewHighScores()
    {
        SceneManager.LoadScene(2);
    }
}
