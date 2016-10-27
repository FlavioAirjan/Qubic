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
        GameObject.Find("Player").GetComponent<PlayerController>().canPlay = true;
        ResultPanel.SetActive(false);
        
    }

    public void QuitGame()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().canPlay = true;
        SceneManager.LoadScene("StartMenu");
    }
}
