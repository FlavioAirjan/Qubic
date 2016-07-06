using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultPanelScript : MonoBehaviour {


    public GameObject ResultPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ContinueGame()
    {
        ResultPanel.SetActive(false);
        
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
