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
    public Button pauseButton; 

 

    [Header("Sounds")]
    public AudioClip backgrounsSound;

    private AudioSource audioSource;



    [Header("Shop")]
    public ShopManag shopManag;
    public GameObject shopPanel;
    public Button closeShopButton; 

    [Header("Play")]
    public GameObject playPanel;


    private bool isPaused = false;


    // Initialize the game state
    private void Awake(){
        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);
        shopPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        GetHighScore();
        GetCoins();
        playPanel.SetActive(false);       
    }
    

    // Retrieve the high score from PlayerPrefs and update the UI
    private void GetHighScore(){
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best: " + highScore;
    }


    // Retrieve the coin count from PlayerPrefs and update the UI
    private void GetCoins(){
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = "Coins: " + coins;
    }


    // Increase the score and update the UI
    public void IncreaseScore(int points){
        score += points;
        scoreText.text = score.ToString();
        if(score >highScore){
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString();  
        }
    }


    // Increase the coin count and update the UI
    public void IncreaseCoins(int points){
        coins += points;
        coinsText.text = "Coins: " + coins.ToString();
    }


    // Handle the event when the bomb is hit
    public void OnBombHit(){
        Time.timeScale=0;
        audioSource.Stop();
        gameOverPanel.SetActive(true);
        playPanel.SetActive(false);

        audioSource.clip = backgrounsSound;
        audioSource.loop = true;
        audioSource.Play();

        gameOverPanelScoreText.text = "Score: " + score.ToString();

        highScore = PlayerPrefs.GetInt("HighScore");
        gameOverPanelHighScoreText.text = "High score: " + highScore.ToString();

        PlayerPrefs.SetInt("Coins", coins);
        gameOverPanelCoinsText.text = coins.ToString();
    }


    // Restart the game and reset the game state
    public void RestartGame(){
        score = 0;
        scoreText.text = score.ToString();
        pauseButton.interactable = true;

        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);
        playPanel.SetActive(true);

        // Destroy all interactable objects
        foreach ( GameObject gameObject in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(gameObject);
        }
        audioSource.Stop();

        Time.timeScale = 1;

    }


    // Play the slice sound effect
    public void PlaySliceSound(AudioClip sliceSound)
    {
        audioSource.PlayOneShot(sliceSound);
    }
    

    // Toggle pause state
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


    // Resume the game from pause state
    public void ResumeGame(){
        Time.timeScale = 1;
        pausedPanel.SetActive(false);
        playPanel.SetActive(true);
        audioSource.Stop();
    }


    // Show the shop panel and hide other UI elements
    public void OnShopButtonClicked()
    {
        shopManag.HideUIElements(); 
        shopManag.ShowPanel(shopPanel); 
        playPanel.SetActive(false);
    }


    // Close the shop panel and show the game over panel
    public void OnCloseShopButtonClicked()
    {
        shopManag.ShowUIElements();
        shopPanel.SetActive(false);
        gameOverPanel.SetActive(true);        
    }

}