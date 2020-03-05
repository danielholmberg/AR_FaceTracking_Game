using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPoint : MonoBehaviour, IPooledObject
{

    public Rigidbody rb;
    public Renderer renderer;
    public float speed = 10f;
    public bool collided = false;
    private GameManager gameManager;
    private Rigidbody playerRigidbody;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        PlayerFaceGameObject playerObject = FindObjectOfType<PlayerFaceGameObject>();
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        renderer = this.gameObject.GetComponent<Renderer>();
    }

    public void OnObjectSpawn()
    {
        Debug.Log("OnObjectSpawn TwoPoint");
        rb.transform.position = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), -0.2f);
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        rb.angularVelocity = rb.transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!collided && (transform.position.z > playerRigidbody.transform.position.z + 0.2f)) {
            renderer.material.color = Color.red;
            StartCoroutine(DelayedRemove(1f));
        }
    }

    public IEnumerator DelayedRemove(float delay) 
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
