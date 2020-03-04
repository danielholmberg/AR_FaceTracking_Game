using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public bool collided = false;
    public GameObject explosionPrefab;
    private GameManager gameManager;
    private PlayerFaceGameObject playerObject;

    // Start is called before the first frame update
    void Start()
    { 
        gameManager = FindObjectOfType<GameManager>();
        playerObject = FindObjectOfType<PlayerFaceGameObject>();

        rb = this.GetComponent<Rigidbody>();
        rb.transform.position = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        rb.angularVelocity = rb.transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(collided) {
            gameManager.health--;
            gameManager.incomingBomb = false;
            Destroy(this.gameObject);
            GameObject a = Instantiate(explosionPrefab) as GameObject;
            a.transform.position = transform.position;
        } else if(transform.position.z > playerObject.GetComponent<Rigidbody>().transform.position.z) {
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            gameManager.incomingBomb = false;

            if(transform.position.z > playerObject.GetComponent<Rigidbody>().transform.position.z + 0.5f) {
                Destroy(this.gameObject);
            }
        }
    }
}
