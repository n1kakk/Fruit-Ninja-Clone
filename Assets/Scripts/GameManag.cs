
using UnityEngine;
using UnityEngine.UI;
public class GameManag : MonoBehaviour
{
    [Header("Score Elements")] 
    public int score;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;
    

    [Header("GameOver")] 
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;


    private void Awake(){
        gameOverPanel.SetActive(false);
        GetHighScore();
    }

    private void GetHighScore(){
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best: " + highScore;
    }

    public void IncreaseScore(int points){
        score += points;
        scoreText.text = score.ToString();
        if(score >highScore){
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString();  
        }
    }
    public void OnBombHit(){
        Time.timeScale=0;
        gameOverPanel.SetActive(true);
        gameOverPanelScoreText.text = "Score: " + score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore");
        gameOverPanelHighScoreText.text = "High score: " + highScore.ToString();
    }

    public void RestartGame(){
        score = 0;
        scoreText.text = score.ToString();

        gameOverPanel.SetActive(false);

        foreach ( GameObject gameObject in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;
    }

}