using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2dPlatformer : MonoBehaviour {
    public Transform target; //what camera is following
    public float smoothing; //dampening effect
    Vector3 offset; //difference between camera and character
    float lowY; //how low can camera go


	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
        lowY = transform.position.y;
	}


    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetcamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetcamPos, smoothing * Time.deltaTime);
            if (transform.position.y < lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
    }
}

