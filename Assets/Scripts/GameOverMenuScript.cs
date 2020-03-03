using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuScript : MonoBehaviour
{
    public void RestartGame() 
    {
        FindObjectOfType<GameManager>().RestartGame();
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
