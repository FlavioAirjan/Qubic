using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    //-1 = player.
    // 0 = GameStop.
    //1 = computer.
    public int turn;
    public bool win;
    public GameObject TurnText;
    public GameObject GameText;
    public GameObject ResultText;
    public GameObject MasterCube;
    public GameObject ResultPanel;
    //Recebe os cubos pintados.
    void Awake()
    {
        TurnText = GameObject.Find("Canvas/StatePanel/Turn");
        GameText = GameObject.Find("Canvas/StatePanel/Game");
        GameText.GetComponent<Text>().text = "Game: In Progress";

        //turn = Random.value > 0.5 ? 1 : -1;

        turn = 1;
        changeTurn();

    }

    // Use this for initialization
    void Start () {
        
        MasterCube = GameObject.Find("MasterCube");
        if (MasterCube == null)
        {
            MasterCube = GameObject.Find("CenterMasterCube/MasterCube");
        }
       
    }

    public void setGameResultText(string s)
    {
        GameText.GetComponent<Text>().text = "Game: " + s;
    }

    public void setGameTurnText(string s)
    {
        TurnText.GetComponent<Text>().text = "Turn: " + s;
    }

    public void setResultText(string s)
    {
        ResultText.GetComponent<Text>().text = s;
    }

    public void changeTurn()
    {
        turn *= -1;
        if (turn > 0.5)
        {
            setGameTurnText("Computer");
        }
        else
        {
            setGameTurnText("You");
        }

    }



	// Update is called once per frame
	void Update () {

       



    }

    

}
