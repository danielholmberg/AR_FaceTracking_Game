using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FivePoint : MonoBehaviour, IPooledObject
{

    public Rigidbody rb;
    public Renderer renderer;
    public float speed = 5f;
    public float spawnRadius = 0.1f;
    public float spawnDepth = 0.4f;
    public bool collided = false;
    private GameManager gameManager;
    private Rigidbody playerRigidbody;
    public Material originalMaterial;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        PlayerFaceGameObject playerObject = FindObjectOfType<PlayerFaceGameObject>();
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        renderer = this.gameObject.GetComponent<Renderer>();
    }

    public void OnObjectSpawn()
    {
        Debug.Log("OnObjectSpawn FivePoint");
        collided = false;
        renderer.material = originalMaterial;
        rb.transform.position = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), -spawnDepth);
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        rb.angularVelocity = rb.transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {   
        if(gameManager.faceDetected) {
            if(!collided && (transform.position.z > playerRigidbody.transform.position.z + 0.2f)) {
                renderer.material.color = Color.red;
                StartCoroutine(DelayedRemove(1f));
            }
        }
    }

    public IEnumerator DelayedRemove(float delay) 
    {
        yield return new WaitForSeconds(delay);
        gameManager.objectPooler.AddToPool("FivePoint", gameObject.transform.parent.gameObject);
    }
}
