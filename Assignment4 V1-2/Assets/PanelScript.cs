using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour {
    public Button StartButton;
    public Button CreditButton;
    public Button ExitButton;

    // Use this for initialization
    void Start () {
        StartButton.onClick.AddListener(StartLevel);
        CreditButton.onClick.AddListener(StartCredit);
        ExitButton.onClick.AddListener(EndGame);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void StartLevel()
    {
        Debug.Log("asd");
        Application.LoadLevel("ShaneleeTran_mp3");
    }
    void StartCredit()
    {
        Application.LoadLevel("Credit");
    }
    void EndGame()
    {
        Application.Quit();
    }
}
