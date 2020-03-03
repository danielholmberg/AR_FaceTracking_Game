using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceGameObject : MonoBehaviour
{
    bool isToClose;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        isToClose = (transform.position.z < 0.4f);
    }

    void Update()
    {
        isToClose = (transform.position.z < 0.4f);
        if(transform.position.z < 0.4f) 
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        } else {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Bullet")) 
        {
            if(isToClose) 
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.red;
                gameManager.health--;
            } 
            else
            {   
                if(!other.gameObject.GetComponent<BulletMovement>().collided) 
                {
                    other.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    other.gameObject.GetComponent<AudioSource>().Play();
                    ScoreScript.scoreValue++;
                }
            } 

            // Disable bullet collision 
            other.gameObject.GetComponent<BulletMovement>().collided = true;

            Destroy(other.gameObject, 1.0f);
        }
    }
}
