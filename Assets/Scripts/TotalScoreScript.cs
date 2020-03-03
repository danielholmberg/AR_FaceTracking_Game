using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreScript : MonoBehaviour
{

    Text totalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        totalScoreText = GetComponent<Text>();
        totalScoreText.text = ScoreScript.scoreValue.ToString();
    }
}
