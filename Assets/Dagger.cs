using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {

    private Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        if(collision.gameObject.layer == 12)
        {
            collision.gameObject.SendMessage("Hitted");
        }
    }
}
