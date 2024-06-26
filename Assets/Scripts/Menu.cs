using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    // Method to handle the exit game functionality
    public void ExitGame(){
        Debug.Log("Exit button is pressed");
       //Application.Quit(); 
       //SceneManager.LoadScene(0);
    }
}
