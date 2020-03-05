using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPooledObject
{
    public Rigidbody rb;
    public Renderer renderer;
    public Collider collider;
    public float speed;
    public bool collided = false;
    public GameObject explosionPrefab;
    private GameManager gameManager;
    private Rigidbody playerRigidbody;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        PlayerFaceGameObject playerObject = FindObjectOfType<PlayerFaceGameObject>();
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        renderer = this.gameObject.GetComponent<Renderer>();
        collider = this.gameObject.GetComponent<Collider>();
    }

    public void OnObjectSpawn()
    {
        Debug.Log("OnObjectSpawn BombPoint");
        rb.transform.position = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), -0.2f);
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        rb.angularVelocity = rb.transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(collided) {
            gameManager.health--;
            gameManager.incomingBomb = false;
            gameObject.SetActive(false);
            GameObject a = Instantiate(explosionPrefab) as GameObject;
            a.transform.position = transform.position;
        } else if(transform.position.z > playerRigidbody.transform.position.z) {
            collider.enabled = false;
            renderer.material.color = Color.green;
            gameManager.incomingBomb = false;

            if(transform.position.z > playerRigidbody.transform.position.z + 0.5f) {
                StartCoroutine(DelayedRemove(1f));
            }
        }
    }

    public IEnumerator DelayedRemove(float delay) 
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
