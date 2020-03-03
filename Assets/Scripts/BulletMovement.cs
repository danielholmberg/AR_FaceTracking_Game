using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public bool collided = false;
    private bool outOfPlay = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(!collided && (transform.position.z > playerObject.GetComponent<Rigidbody>().transform.position.z + 0.2f)) {
            if(!outOfPlay) {
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                gameManager.health--;
                outOfPlay = true;
            }

            if(transform.position.z > playerObject.GetComponent<Rigidbody>().transform.position.z + 0.5f) {
                Destroy(this.gameObject);
            }
        }
    }
}
