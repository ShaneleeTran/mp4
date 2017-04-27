using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCanScript : MonoBehaviour {

    GlobalBehavior globalBehavior = null;
    private GameObject Player;

    // Use this for initialization
    void Start () {
        globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
        Player = GameObject.Find("Hero");
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, Player.transform.position) < 20f)
        {
            Destroy(this.gameObject);
            globalBehavior.fuel++;
        }        
    }
}
