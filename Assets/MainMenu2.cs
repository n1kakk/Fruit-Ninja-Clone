using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
    public void ExitGame(){
        //Application.Quit();
        Debug.Log("Button is pressed");
    }
}
