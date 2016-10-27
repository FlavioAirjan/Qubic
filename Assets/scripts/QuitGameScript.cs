using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitGameScript : MonoBehaviour {

    public GameObject ResultText;
    public GameObject ResultPanel;
    public GameObject controler;



    public void QuitGame()
    {
        controler.GetComponent<PlayerController>().canPlay = false;
        ResultPanel.SetActive(true);
        ResultText.GetComponent<Text>().text = "Are you sure?";
    }
}
