using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void ExitGame(){
        Debug.Log("Exit button is pressed");
        Application.Quit(); 
    }
}