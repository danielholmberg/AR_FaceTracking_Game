using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class deployBullets : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float respawnTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bulletWave());
    }

    private void spawnBullet() 
    {
        GameObject a = Instantiate(bulletPrefab) as GameObject;
        Vibration.Vibrate(100);
    }

    IEnumerator bulletWave() 
    {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            spawnBullet();
        }
    }
}
