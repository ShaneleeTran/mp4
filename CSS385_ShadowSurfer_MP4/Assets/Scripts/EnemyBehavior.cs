using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	private const float kReferenceSpeed = 20f;
	public float mSpeed = kReferenceSpeed;
    public float mTowardsCenter = 0.5f;
    private GameObject Player;
    private float runDistance = 30f;
    private float aspectR, maxX, minX, maxY, minY;
    private float turnSpeed = 1f;
    private int health = 3;
    private float timer;
    private bool stunned = false;
     
		// what is the change of enemy flying towards the world center after colliding with world bound
		// 0: no control
		// 1: always towards the world center, no randomness
		
	// Use this for initialization
	void Start ()
    {
        Player = GameObject.Find("Hero");
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        maxX = width / 2 - 10;
        minX = -width / 2 + 10;
        maxY = height / 2 - 10;
        minY = -height / 2 + 10;

        gameObject.GetComponent<Renderer>().material.color = Color.red;

        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        NewDirection();
	}

	// Update is called once per frame
	void Update ()
    {
        GlobalBehavior globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
        GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);

        if (Vector3.Distance(transform.position, Player.transform.position) < runDistance)
        {
            RunState();
        }
        else if (stunned)
        {
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                stunned = false;
                timer = 0f;
            }                    
                StunnedState();
        }              
        else if(!globalBehavior.pause)
            NormalState();
        		
		if (status != GlobalBehavior.WorldBoundStatus.Inside)
        {
		    // Debug.Log("collided position: " + this.transform.position);
			NewDirection();
		}
	}

	private void NewDirection() {
		GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();

		// we want to move towards the center of the world
		Vector2 v = globalBehavior.WorldCenter - new Vector2(transform.position.x, transform.position.y);  
				// this is vector that will take us back to world center
		v.Normalize();
		Vector2 vn = new Vector2(v.y, -v.x); // this is a direciotn that is perpendicular to V

		float useV = 1.0f - Mathf.Clamp(mTowardsCenter, 0.01f, 1.0f);
		float tanSpread = Mathf.Tan( useV * Mathf.PI / 2.0f );

		float randomX = Random.Range(0f, 1f);
		float yRange = tanSpread * randomX;
		float randomY = Random.Range (-yRange, yRange);

		Vector2 newDir = randomX * v + randomY * vn;
		newDir.Normalize();
		transform.up = newDir;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Only care if hitting an Egg (vs. hitting another Enemy!
		if (other.gameObject.name == "Egg(Clone)") {
            if(health <= 1)
                Destroy(this.gameObject);
            Destroy(other.gameObject);
            health--;
            stunned = true;
        }
	}

    private void NormalState()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
    }

    private void RunState()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;

        Vector2 playerFront = (Vector2)Player.transform.forward;
        Vector2 perpFront = new Vector2(playerFront.y, -playerFront.x);
        perpFront.Normalize();

        float theta = (float)((180.0 / Mathf.PI) * Mathf.Acos((float)Vector2.Dot(transform.forward, perpFront)));

        if(theta > 0.001f)
        {
            Vector3 sign = Vector3.Cross(transform.forward, perpFront);
            transform.Rotate(Vector3.forward, Mathf.Sign(sign.z) * theta * turnSpeed * Time.smoothDeltaTime);
        }

        transform.position += (40f * Time.smoothDeltaTime) * transform.up;
    }

    private void StunnedState()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        transform.Rotate(Vector3.forward, 9f * Time.smoothDeltaTime);
    }
	
}
