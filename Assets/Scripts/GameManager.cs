using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CountDownController countDownController;

    public GameObject bulletPrefab;
    public float respawnTime = 1f;
    public bool shouldSpawnBullets = false;

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

    private void ShowGameOverMenu()
    {
        
    }

    // Helper functions
    private void SpawnBullet() 
    {
        GameObject a = Instantiate(bulletPrefab) as GameObject;
    }

    public IEnumerator LaunchBulletWave() 
    {
        while(shouldSpawnBullets) {
            yield return new WaitForSeconds(respawnTime);
            SpawnBullet();
        }
    }
}
