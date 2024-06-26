using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManag : MonoBehaviour
{
  [SerializeField] private Image selectedSkin;
  [SerializeField] private Text coinsText;
  [SerializeField] private SkinManager skinManager;


    public GameObject[] panels;
    public GameObject[] uiElementsToHide;

    
    [Header("Shop")]
    public ShopManag shopManag;
    public GameObject shopPanel;


    // Called when the script instance is being loaded
    void Start()
    {
        HideAllPanels();
    }


    void Update()
    {
        // Update the coin count text with the value stored in PlayerPrefs
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("Coins");

        // Update the selected skin image with the currently selected skin's sprite
        selectedSkin.sprite = skinManager.GetSelectedSkin().sprite;
    }
    

    // Method to hide all shop panels
    void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }


    // Method to show a specific panel
    public void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }


    // Method to hide specified UI elements
    public void HideUIElements()
    {
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(false);
        }
    }


    // Method to show specified UI elements
    public void ShowUIElements()
    {
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(true);
        }
    }


    // Method to handle closing the shop panel
    void OnCloseShopButtonClicked()
    {
        shopManag.ShowUIElements(); 
        shopPanel.SetActive(false); 
    }


    // Method to load the main menu scene
    public void LoadMenu() => SceneManager.LoadScene("MainMenuScene");
}