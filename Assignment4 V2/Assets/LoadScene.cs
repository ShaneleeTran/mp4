using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
    public Text TextMessage;
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.timeSinceLevelLoad > 4f)
	    {
	        SceneManager.LoadScene("Level2");
	        SceneManager.UnloadSceneAsync("LoadingScene");
	    }
	}
}
