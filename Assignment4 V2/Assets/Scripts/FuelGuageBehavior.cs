using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGuageBehavior : MonoBehaviour {
    public Slider fuelBar;
    private GlobalBehavior globalBehavior = null;

    // Use this for initialization
    void Start () {
        globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
        fuelBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        fuelBar.value = globalBehavior.fuel;
	}
}
