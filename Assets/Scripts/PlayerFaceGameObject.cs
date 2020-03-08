using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceGameObject : MonoBehaviour
{
    bool isToClose;
    private GameManager gameManager;
    public float toCloseDistance = 0.3f;
    private Color originalColor;
    public float pointAlpha = 0.2f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        isToClose = (transform.position.z < toCloseDistance);
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        isToClose = (transform.position.z < toCloseDistance);
        if(isToClose) 
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        } else {
            gameObject.GetComponent<Renderer>().material.color = originalColor;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        AudioSource targetAudio = other.gameObject.GetComponent<AudioSource>();
        Renderer targetRenderer = other.gameObject.GetComponent<Renderer>();

        if(other.gameObject.CompareTag("OnePoint")) 
        {   
            OnePoint collidedObject = other.gameObject.GetComponent<OnePoint>();
            if(gameManager.faceDetected && isToClose) 
            {
                targetRenderer.material.color = Color.red;
            } 
            else
            {   
                if(!collidedObject.collided) 
                {
                    targetAudio.Play();
                    ScoreScript.scoreValue++;
                }
            }

            // Disable bullet collision 
            collidedObject.collided = true;
            
            Color tempColor = targetRenderer.material.color;
            tempColor.a = pointAlpha;
            targetRenderer.material.color = tempColor;

            StartCoroutine(collidedObject.DelayedRemove(1f));
        } 
        else if(other.gameObject.CompareTag("TwoPoint")) 
        {
            TwoPoint collidedObject = other.gameObject.GetComponent<TwoPoint>();
            if(gameManager.faceDetected && isToClose) 
            {
                targetRenderer.material.color = Color.red;
            } 
            else
            {   
                if(!collidedObject.collided) 
                {
                    targetAudio.Play();
                    ScoreScript.scoreValue+=2;
                }
            }

            // Disable bullet collision 
            collidedObject.GetComponent<TwoPoint>().collided = true;
            
            Color tempColor = targetRenderer.material.color;
            tempColor.a = pointAlpha;
            targetRenderer.material.color = tempColor;

            StartCoroutine(collidedObject.DelayedRemove(1f));
        }
        else if(other.gameObject.CompareTag("FivePoint")) 
        {
            FivePoint collidedObject = other.gameObject.GetComponent<FivePoint>();
            if(gameManager.faceDetected && isToClose) 
            {
                targetRenderer.material.color = Color.red;
            } 
            else
            {   
                if(!collidedObject.collided) 
                {
                    targetAudio.Play();
                    ScoreScript.scoreValue+=5;
                }
            }

            // Disable bullet collision 
            collidedObject.collided = true;

            Color tempColor = targetRenderer.material.color;
            tempColor.a = pointAlpha;
            targetRenderer.material.color = tempColor;

            StartCoroutine(collidedObject.DelayedRemove(1f));
        }
        else if(other.gameObject.CompareTag("Bomb")) 
        {
            other.gameObject.GetComponent<Bomb>().collided = true;
        }
    }
}
