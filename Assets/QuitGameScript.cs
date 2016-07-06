using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitGameScript : MonoBehaviour {

    public GameObject ResultText;
    public GameObject ResultPanel;



    public void QuitGame()
    {
        ResultPanel.SetActive(true);
        ResultText.GetComponent<Text>().text = "Are you sure?";
    }
}
