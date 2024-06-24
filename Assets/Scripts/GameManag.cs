
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


    
    [Header("Paused")] 
    public GameObject pausedPanel;
    public Text pausedPanelScoreText;
    public Text pausedPanelHighScoreText;
    public Button pauseButton; 


    [Header("Sounds")]
    public AudioClip[] sliceSounds;
    private AudioSource audioSource;


    private bool isPaused = false;

    private void Awake(){
        pauseButton.interactable = true;
        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Stop();
        gameOverPanel.SetActive(true);
        pauseButton.interactable = false;
        gameOverPanelScoreText.text = "Score: " + score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore");
        gameOverPanelHighScoreText.text = "High score: " + highScore.ToString();
    }

    public void RestartGame(){
        score = 0;
        scoreText.text = score.ToString();
        pauseButton.interactable = true;

        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);

        foreach ( GameObject gameObject in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;
    }


    public void PlayRandomSliceSound(){
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }

    public void PauseGame(){
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0; 
            Debug.Log("Game Paused");
            pausedPanel.SetActive(true);
            pausedPanelScoreText.text = "Score: " + score.ToString();
            highScore = PlayerPrefs.GetInt("HighScore");
            pausedPanelHighScoreText.text = "High score: " + highScore.ToString();
        }
        else
        {
            Time.timeScale = 1; 
            Debug.Log("Game Resumed");
            pausedPanel.SetActive(false);
        }
    }
    public void ResumeGame(){
        Time.timeScale = 1; 
        pausedPanel.SetActive(false);
    }

}