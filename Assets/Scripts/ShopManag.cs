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


    void Start()
    {
        HideAllPanels();
    }

    void Update()
    {
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        selectedSkin.sprite = skinManager.GetSelectedSkin().sprite;
    }
    

    void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }

    public void HideUIElements()
    {
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(false);
        }
    }

    public void ShowUIElements()
    {
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(true);
        }
    }

    void OnCloseShopButtonClicked()
    {
        shopManag.ShowUIElements(); // Показываем скрытые UI элементы
        shopPanel.SetActive(false); // Скрываем панель магазина
    }



  public void LoadMenu() => SceneManager.LoadScene("MainMenuScene");
}