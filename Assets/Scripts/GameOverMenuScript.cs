using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public void RestartGame() 
    {

        BackgroundAudio.Instance.PlayBackgroundAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
