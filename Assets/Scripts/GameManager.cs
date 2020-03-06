using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CountDownController countDownController;
    public float restartDelay = 1f;
    bool gameHasEnded = false;
    public ObjectPooler objectPooler;
    BackgroundAudio backgroundAudio;

    // Points
    public GameObject onePointPrefab;
    public GameObject twoPointPrefab;
    public GameObject fivePointPrefab;
    public float respawnOnePointTime = 2f;
    public float respawnTwoPointTime = 5f;
    public float respawnFivePointTime = 20f;
    public bool shouldSpawnOnePoints = false;
    public bool shouldSpawnTwoPoints = false;
    public bool shouldSpawnFivePoints = false;

    // Bomb
    public GameObject bombPrefab;
    public float respawnBombTime = 10f;
    public bool shouldSpawnBombs = false;
    public static bool incomingBomb = false;

    // Health
    public int health = 5;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Persistent Data
    private string highscoreKey = "Highscore";

    // Coroutines
    private IEnumerator launchOnePointWaveCoroutine;
    private IEnumerator launchTwoPointWaveCoroutine;
    private IEnumerator launchFivePointWaveCoroutine;
    private IEnumerator launchBombWaveCoroutine;

    private void Start() 
    {   
        objectPooler = ObjectPooler.Instance;
        backgroundAudio = BackgroundAudio.Instance;
        StartCoroutine(countDownController.CountDownToStart());
        launchOnePointWaveCoroutine = LaunchOnePointWave();
        launchTwoPointWaveCoroutine = LaunchTwoPointWave();
        launchFivePointWaveCoroutine = LaunchFivePointWave();
        launchBombWaveCoroutine = LaunchBombWave();
    }

    private void Update() 
    {  
        for(int i = 0; i < hearts.Length; i++) {
            if(i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
        }

        if(health == 0) 
        {
            EndGame();
        }    
    }

    public void StartGame()
    {
        gameHasEnded = false;
        shouldSpawnOnePoints = true;
        shouldSpawnTwoPoints = true;
        shouldSpawnFivePoints = true;
        shouldSpawnBombs = true;
        StartCoroutine(launchOnePointWaveCoroutine);
        StartCoroutine(launchTwoPointWaveCoroutine);
        StartCoroutine(launchFivePointWaveCoroutine);
        StartCoroutine(launchBombWaveCoroutine);
    }
    public void EndGame() 
    {
        if(!gameHasEnded) 
        {
            gameHasEnded = true;

            backgroundAudio.PlayEndAudio();

            // Disable object respawn
            shouldSpawnOnePoints = false;
            shouldSpawnTwoPoints = false;
            shouldSpawnFivePoints = false;
            shouldSpawnBombs = false;
            StopCoroutine(launchOnePointWaveCoroutine);
            StopCoroutine(launchTwoPointWaveCoroutine);
            StopCoroutine(launchFivePointWaveCoroutine);
            StopCoroutine(launchBombWaveCoroutine);
            Debug.Log("Game Over!");

            // Set Highscore
            if(PlayerPrefs.HasKey(highscoreKey)) {
                if(PlayerPrefs.GetInt(highscoreKey) < ScoreScript.scoreValue) {
                    PlayerPrefs.SetInt(highscoreKey, ScoreScript.scoreValue);
                }
            } else {
                PlayerPrefs.SetInt(highscoreKey, ScoreScript.scoreValue);
            }
             
            // Show GameOverMenu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartGame()
    {
        ScoreScript.scoreValue = 0;
        backgroundAudio.PlayBackgroundAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // ---- Helper functions ----

    // One Point
    private void SpawnOnePoint() 
    {
        objectPooler.SpawnFromPool("OnePoint");
    }

    private IEnumerator LaunchOnePointWave() 
    {
        while(shouldSpawnOnePoints) {
            yield return new WaitForSeconds(respawnOnePointTime);
            if(!incomingBomb) {
                SpawnOnePoint();
            }
        }
    }

    // Two Points
    private void SpawnTwoPoint() 
    {
        objectPooler.SpawnFromPool("TwoPoint");
    }

    private IEnumerator LaunchTwoPointWave() 
    {
        while(shouldSpawnTwoPoints) {
            yield return new WaitForSeconds(respawnTwoPointTime);
            if(!incomingBomb) {
                SpawnTwoPoint();
            }
        }
    }

    // Five Points
    private void SpawnFivePoint() 
    {
        objectPooler.SpawnFromPool("FivePoint");
    }

    private IEnumerator LaunchFivePointWave() 
    {
        while(shouldSpawnFivePoints) {
            yield return new WaitForSeconds(respawnFivePointTime);
            if(!incomingBomb) {
                SpawnFivePoint();
            }
        }
    }

    private void SpawnBomb() 
    {
        objectPooler.SpawnFromPool("Bomb");
    }

    private IEnumerator LaunchBombWave() 
    {
        while(shouldSpawnBombs) {
            yield return new WaitForSeconds(respawnBombTime);
            if(!incomingBomb) {
                SpawnBomb();
            }
        }
    }
}
