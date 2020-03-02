using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed = 50.0f;
    private Vector3 screenBounds;

    // Start is called before the first frame update
    void Start()
    { 
        rb = this.GetComponent<Rigidbody>();
        rb.transform.position = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > 3.0f) {
            Destroy(this.gameObject);
        }
    }
}
