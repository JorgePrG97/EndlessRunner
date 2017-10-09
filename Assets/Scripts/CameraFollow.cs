using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 offset = new Vector3(8, 4, -1);

	// Use this for initialization
	void Awake ()
    {
        
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        this.transform.position = new Vector3(target.position.x + offset.x, offset.y, offset.z);		
	}
}
