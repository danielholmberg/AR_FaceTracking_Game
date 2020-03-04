using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CountDownController countDownController;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public float respawnBulletTime = 1f;
    public float respawnBombTime = 4f;
    public bool shouldSpawnBullets = false;
    public bool shouldSpawnBombs = false;
    public bool incomingBomb = false;

    public float restartDelay = 1f;
    bool gameHasEnded = false;
    public int health = 5;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start() 
    {   
        StartGame();    
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
        shouldSpawnBullets = true;
        shouldSpawnBombs = true;
        StartCoroutine(countDownController.CountDownToStart());
    }
    public void EndGame() 
    {
        if(!gameHasEnded) 
        {
            gameHasEnded = true;
            shouldSpawnBullets = false;
            Debug.Log("Game Over!");

            // Show GameOverMenu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartGame()
    {
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Helper functions
    private void SpawnBullet() 
    {
        GameObject a = Instantiate(bulletPrefab) as GameObject;
    }

    public IEnumerator LaunchBulletWave() 
    {
        while(shouldSpawnBullets) {
            yield return new WaitForSeconds(respawnBulletTime);
            if(!incomingBomb) {
                SpawnBullet();
            }
        }
    }

    private void SpawnBomb() 
    {
        GameObject a = Instantiate(bombPrefab) as GameObject;
        incomingBomb = true;
    }

    public IEnumerator LaunchBombWave() 
    {
        while(shouldSpawnBombs) {
            yield return new WaitForSeconds(respawnBombTime);
            SpawnBomb();
        }
    }
}
