using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class GameManag : MonoBehaviour
{
    [Header("Score Elements")] 
    public int score;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;


    [Header("Coins Elements")] 
    public int coins;
    public Text coinsText;
    


    [Header("GameOver")] 
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;
    public Text gameOverPanelCoinsText;


    
    [Header("Paused")] 
    public GameObject pausedPanel;
    // public Text pausedPanelScoreText;
    // public Text pausedPanelHighScoreText;
    public Button pauseButton; 

 

    [Header("Sounds")]
    //public AudioClip[] sliceSounds;
    public AudioClip backgrounsSound;

    private AudioSource audioSource;



    [Header("Shop")]
    public ShopManag shopManag;
    public GameObject shopPanel;
    public Button closeShopButton; 

    [Header("Play")]
    public GameObject playPanel;


    private bool isPaused = false;

    private void Awake(){
        Debug.Log("Awake called in " + gameObject.name);
                
        //pauseButton.interactable = true;
        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);
        shopPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        GetHighScore();
        GetCoins();
        playPanel.SetActive(false);

        
    }
    
    private void GetHighScore(){
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best: " + highScore;
    }

    private void GetCoins(){
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = "Coins: " + coins;
    }

    public void IncreaseScore(int points){
        score += points;
        scoreText.text = score.ToString();
        if(score >highScore){
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString();  
        }
    }

    public void IncreaseCoins(int points){
        coins += points;
        coinsText.text = "Coins: " + coins.ToString();
    }

    public void OnBombHit(){
        Time.timeScale=0;
        audioSource.Stop();
        gameOverPanel.SetActive(true);
        playPanel.SetActive(false);

        audioSource.clip = backgrounsSound;
        audioSource.loop = true;
        audioSource.Play();

        //pauseButton.interactable = false;
        gameOverPanelScoreText.text = "Score: " + score.ToString();

        highScore = PlayerPrefs.GetInt("HighScore");
        gameOverPanelHighScoreText.text = "High score: " + highScore.ToString();

        PlayerPrefs.SetInt("Coins", coins);
        gameOverPanelCoinsText.text = coins.ToString();
    }

    public void RestartGame(){
        score = 0;
        scoreText.text = score.ToString();
        pauseButton.interactable = true;

        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);
        playPanel.SetActive(true);

        foreach ( GameObject gameObject in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(gameObject);
        }
        audioSource.Stop();

        Time.timeScale = 1;

    }
    public void PlaySliceSound(AudioClip sliceSound)
    {
        audioSource.PlayOneShot(sliceSound);
    }

    public void PauseGame(){
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0; 
            Debug.Log("Game Paused");
            pausedPanel.SetActive(true);
            playPanel.SetActive(false);
            audioSource.clip = backgrounsSound;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Time.timeScale = 1; 
            pausedPanel.SetActive(false);
            playPanel.SetActive(true);
            audioSource.Stop();
        }
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        pausedPanel.SetActive(false);
        playPanel.SetActive(true);
        audioSource.Stop();
    }

    public void OnShopButtonClicked()
    {
        shopManag.HideUIElements(); 
        shopManag.ShowPanel(shopPanel); 
        playPanel.SetActive(false);
    }

    public void OnCloseShopButtonClicked()
    {
        shopManag.ShowUIElements();
        shopPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        
    }

}