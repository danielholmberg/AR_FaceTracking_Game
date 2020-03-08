using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPooledObject
{
    public Rigidbody rb;
    public Renderer renderer;
    public MeshCollider collider;
    public float speed;
    public float spawnRadius = 0.1f;
    public float spawnDepth = 0.4f;
    public bool collided = false;
    public GameObject explosionPrefab;
    private GameManager gameManager;
    private Rigidbody playerRigidbody;
    public Material originalMaterial;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        PlayerFaceGameObject playerObject = FindObjectOfType<PlayerFaceGameObject>();
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        renderer = this.gameObject.GetComponent<Renderer>();
        collider = this.gameObject.GetComponent<MeshCollider>();
    }

    public void OnObjectSpawn()
    {
        Debug.Log("OnObjectSpawn BombPoint");
        collided = false;
        renderer.material = originalMaterial;
        collider.enabled = true;
        GameManager.incomingBomb = true;
        rb.transform.position = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), -spawnDepth);
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        rb.angularVelocity = rb.transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(collided) {
            gameManager.health--;
            GameManager.incomingBomb = false;
            gameManager.objectPooler.AddToPool("Bomb", gameObject.transform.parent.gameObject);
            GameObject a = Instantiate(explosionPrefab) as GameObject;
            a.transform.position = transform.position;
        } else if(transform.position.z > playerRigidbody.transform.position.z) {
            collider.enabled = false;
            renderer.material.color = Color.green;
            GameManager.incomingBomb = false;

            if(transform.position.z > playerRigidbody.transform.position.z + 0.5f) {
                StartCoroutine(DelayedRemove(1f));
            }
        }
    }

    public IEnumerator DelayedRemove(float delay) 
    {
        yield return new WaitForSeconds(delay);
        gameManager.objectPooler.AddToPool("Bomb", gameObject.transform.parent.gameObject);
    }
}
