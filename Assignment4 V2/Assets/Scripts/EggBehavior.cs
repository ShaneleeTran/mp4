using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	
	private float mSpeed = 150f;

    void Start()
	{
    }

	// Update is called once per frame
	void Update () {
        GlobalBehavior globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
        GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);

        if (status != GlobalBehavior.WorldBoundStatus.Inside)
            Destroy(gameObject);

        transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
	}
	
	public void SetForwardDirection(Vector3 f)
	{
		transform.up = f;
	}
}
