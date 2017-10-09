using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private Camera cam;

	// Use this for initialization
	void Awake () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < cam.OrtographicBounds().min.x)
        {
            Destroy(gameObject);
        }
	}
}
