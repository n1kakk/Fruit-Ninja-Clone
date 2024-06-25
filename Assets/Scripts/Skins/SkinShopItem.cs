﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopItem : MonoBehaviour
{

    [Header("Sounds")]
    public AudioClip no;
    public AudioClip ok;
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
    skin = skinManager.skins[skinIndex];

    GetComponent<Image>().sprite = skin.sprite;

    if (skinManager.IsUnlocked(skinIndex))
    {
      buyButton.gameObject.SetActive(false);
    }
    else
    {
      buyButton.gameObject.SetActive(true);
      costText.text = skin.cost.ToString();
    }
  }

  public void OnSkinPressed()
  {
    if (skinManager.IsUnlocked(skinIndex))
    {      
      skinManager.SelectSkin(skinIndex);
        audioSource.clip = ok;
        audioSource.Play();
    }
    else{
        audioSource.clip = no;
        audioSource.Play();
    }
  }

  public void OnBuyButtonPressed()
  {
    int coins = PlayerPrefs.GetInt("Coins", 0);

    // Unlock the skin
    if (coins >= skin.cost && !skinManager.IsUnlocked(skinIndex))
    {
      PlayerPrefs.SetInt("Coins", coins - skin.cost);
      skinManager.Unlock(skinIndex);
      buyButton.gameObject.SetActive(false);
      skinManager.SelectSkin(skinIndex);
    }
    else
    {
      Debug.Log("Not enough coins :(");
    }
  }
}