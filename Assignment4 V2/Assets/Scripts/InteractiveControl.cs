using UnityEngine;	
using System.Collections;

public class InteractiveControl : MonoBehaviour
{

    public GameObject mProjectile = null;

    private const float eggSpawnInterval = 0.1f;
    private float timer = 0.0f;
    private float maxX, minX, maxY, minY;

    #region user control references
    private float kHeroSpeed = 40f;
    private float kHeroRotateSpeed = 90f; // 90-degrees in 1 seconds
    #endregion

    // Use this for initialization
    void Start()
    {

        // initialize projectile spawning
        if (null == mProjectile)
            mProjectile = Resources.Load("Prefabs/Egg") as GameObject;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        maxX = width / 2;
        minX = -width / 2;
        maxY = height / 2;
        minY = -height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        #region user movement control
        Vector3 v3 = transform.position;
        v3.x = Mathf.Clamp(transform.position.x, minX, maxX);
        v3.y = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") * (kHeroRotateSpeed * Time.smoothDeltaTime));
        transform.position = v3;
        transform.position += Input.GetAxis("Vertical") * transform.up * (kHeroSpeed * Time.smoothDeltaTime);
        #endregion

        #region shooting eggs
        if (Input.GetAxis("Fire1") > 0f)
        { // this is Left-Control
            timer += Time.deltaTime;
            float seconds = timer % 60;
            if (timer >= eggSpawnInterval)
            {
                GameObject e = Instantiate(mProjectile) as GameObject;
                EggBehavior egg = e.GetComponent<EggBehavior>(); // Shows how to get the script from GameObject
                if (null != egg)
                {
                    e.transform.position = transform.position;
                    egg.SetForwardDirection(transform.up);
                }
                timer = 0.0f;
            }
        }
        #endregion
    }
}
