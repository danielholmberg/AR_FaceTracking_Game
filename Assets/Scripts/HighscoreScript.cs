using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{
    Text highscoreText;
    int highscoreValue;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText = GetComponent<Text>();
        highscoreValue = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = highscoreValue.ToString();
    }
}
