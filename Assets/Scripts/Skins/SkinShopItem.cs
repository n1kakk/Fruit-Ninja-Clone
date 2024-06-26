﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopItem : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip no;
    public AudioClip ok;
    public AudioClip onBought;
    private AudioSource audioSource;


  [SerializeField] private SkinManager skinManager;
  [SerializeField] private int skinIndex;
  [SerializeField] private Button buyButton;
  [SerializeField] private Text costText;
  private Skin skin;

  void Awake(){
    audioSource = GetComponent<AudioSource>();
  }

  void Start()
  {
     // Get the skin from the SkinManager using the skinIndex
    skin = skinManager.skins[skinIndex];

    // Set the image sprite to the skin's sprite
    GetComponent<Image>().sprite = skin.sprite;


    // Check if the skin is already unlocked
    if (skinManager.IsUnlocked(skinIndex))
    {
      // If unlocked, hide the buy button
      buyButton.gameObject.SetActive(false);

    } else
    {
      // If not unlocked, show the buy button and display the cost
      buyButton.gameObject.SetActive(true);
      costText.text = skin.cost.ToString();
    }
  }

  public void OnSkinPressed()
  {
    // Check if the skin is unlocked
    if (skinManager.IsUnlocked(skinIndex))
    {    
      // If unlocked, select the skin and play the "ok" sound  
      skinManager.SelectSkin(skinIndex);
        audioSource.clip = ok;
        audioSource.Play();

    } else
    {
      // If not unlocked, play the "no" sound
        audioSource.clip = no;
        audioSource.Play();
    }
  }

  public void OnBuyButtonPressed()
  {
    int coins = PlayerPrefs.GetInt("Coins", 0);

    // Check if the player has enough coins and the skin is not already unlocked
    if (coins >= skin.cost && !skinManager.IsUnlocked(skinIndex))
    {
      PlayerPrefs.SetInt("Coins", coins - skin.cost);
      skinManager.Unlock(skinIndex);
      buyButton.gameObject.SetActive(false);
      skinManager.SelectSkin(skinIndex);
      audioSource.clip = onBought;
      audioSource.Play();
    }
    else
    {
      Debug.Log("Not enough coins :(");
    }
  }
}