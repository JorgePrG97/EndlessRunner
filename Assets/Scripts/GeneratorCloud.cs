using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCloud : MonoBehaviour
{
    private float speed=-10;
    private int distance;
    private SpriteRenderer sr;
    private Camera cam;

	// Use this for initialization
	void Awake() {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        distance = Random.Range(10, 50);
        sr.sortingOrder = -distance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x + speed/distance * Time.deltaTime, transform.position.y, transform.position.z);		
        if(cam.OrtographicBounds().min.x > sr.bounds.max.x)
        {
            transform.position = new Vector3(cam.OrtographicBounds().max.x + sr.bounds.size.x/2f ,transform.position.y + Random.Range(-5, 5), transform.position.z);
        }
	}
}
