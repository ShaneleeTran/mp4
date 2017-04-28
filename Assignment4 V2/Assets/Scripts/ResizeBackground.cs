using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;
        gameObject.transform.localScale = Vector3.one * cameraHeight / 4.0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
