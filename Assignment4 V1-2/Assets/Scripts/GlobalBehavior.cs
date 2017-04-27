using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalBehavior : MonoBehaviour {

    #region World Bound support
    private Bounds mWorldBound;  // this is the world bound
    private Vector2 mWorldMin;  // Better support 2D interactions
    private Vector2 mWorldMax;
    private Vector2 mWorldCenter;
    private Camera mMainCamera;
    #endregion

    private int numOfEggs = 0;
    private int numOfEnemies = 0;
    private int initEnemies = 6;
    private int currentLevel = 1;
    private int fuelGoal = 4;
    public Text TextEggs, TextEnemies, TextFuel;
    public int fuel = 0;

    #region  support runtime enemy creation
    // to support time ...
    private float mPreEnemySpawnTime = -1f; // 
    private float kEnemySpawnInterval = 3.0f; // in seconds

    // spwaning enemy ...
    public GameObject mEnemyToSpawn = null;
    #endregion

    // Use this for initialization
    void Start() {

        #region world bound support
        mMainCamera = Camera.main;
        mWorldBound = new Bounds(Vector3.zero, Vector3.one);
        UpdateWorldWindowBound();
        #endregion
        initScene();
        setCountText();

        #region initialize enemy spawning
        initializeEnemies();
        
        #endregion
    }

    void initScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentLevel = 1;
            initEnemies = 6;
            kEnemySpawnInterval = 5.0f;
            fuelGoal = 4;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            currentLevel = 2;
            initEnemies = 12;
            kEnemySpawnInterval = 3.0f;
            fuelGoal = 10;
        }
        
    }

    void initializeEnemies()
    {
        if (null == mEnemyToSpawn)
            mEnemyToSpawn = Resources.Load("Prefabs/Enemy") as GameObject;

        for (int i = 0; i < initEnemies; i++)
        {
            GameObject e = (GameObject)Instantiate(mEnemyToSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnAnEnemy();      

        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        numOfEggs = GameObject.FindGameObjectsWithTag("Egg").Length;

        setCountText();

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel("Menu");

        levelUpdate(currentLevel);
    }

    void levelUpdate(int l)
    {
        switch (l)
        {
            case 1:
                updateLevelOne();
                break;
            case 2:
                updateLevelTwo();
                break;
        }
    }

    void updateLevelOne()
    {
        if (fuel >= 4)
        {
            SceneManager.LoadScene("Level2");
            SceneManager.UnloadSceneAsync("ShaneleeTran_mp3");
        }
    }

    void updateLevelTwo()
    {
        if (fuel >= 10)
        {
            // Load level 3
        }
    }

    void setCountText()
    {
        TextEggs.text = "Number of eggs: " + numOfEggs.ToString();
        TextEnemies.text = "Number of enemies: " + numOfEnemies.ToString();
        TextFuel.text = "FUEL: " + fuel.ToString() + " / " + fuelGoal;
    }
	
	#region Game Window World size bound support
	public enum WorldBoundStatus {
		CollideTop,
		CollideLeft,
		CollideRight,
		CollideBottom,
		Outside,
		Inside
	};

    /// <summary>
    /// This function must be called anytime the MainCamera is moved, or changed in size
    /// </summary>
    public void UpdateWorldWindowBound()
	{
		// get the main 
		if (null != mMainCamera) {
			float maxY = mMainCamera.orthographicSize;
			float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
			float sizeX = 2 * maxX;
			float sizeY = 2 * maxY;
			float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);
			
			// Make sure z-component is always zero
			Vector3 c = mMainCamera.transform.position;
			c.z = 0.0f;
			mWorldBound.center = c;
			mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

			mWorldCenter = new Vector2(c.x, c.y);
			mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
			mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
		}
	}
	
	public Vector2 WorldCenter { get { return mWorldCenter; } }
	public Vector2 WorldMin { get { return mWorldMin; }} 
	public Vector2 WorldMax { get { return mWorldMax; }}
	
	public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;
		
		if (mWorldBound.Intersects(objBound)) {
			if (objBound.max.x > mWorldBound.max.x)
				status = WorldBoundStatus.CollideRight;
			else if (objBound.min.x < mWorldBound.min.x)
				status = WorldBoundStatus.CollideLeft;
			else if (objBound.max.y > mWorldBound.max.y)
				status = WorldBoundStatus.CollideTop;
			else if (objBound.min.y < mWorldBound.min.y)
				status = WorldBoundStatus.CollideBottom;
			else if ( (objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z))
				status = WorldBoundStatus.Outside;
		} else 
			status = WorldBoundStatus.Outside;
		return status;
		
	}
	#endregion 

	#region enemy spawning support
	private void SpawnAnEnemy()
	{
		if ((Time.realtimeSinceStartup - mPreEnemySpawnTime) > kEnemySpawnInterval) {
			GameObject e = (GameObject) Instantiate(mEnemyToSpawn);
			mPreEnemySpawnTime = Time.realtimeSinceStartup;
			// Debug.Log("New enemy at: " + mPreEnemySpawnTime.ToString());
		}
	}
    #endregion
}

/*
    */