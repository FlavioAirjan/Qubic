using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

    //-1 = player.
    // 0 = GameStop.
    //1 = computer.
    public int turn;

    //Recebe os cubos pintados.
    void Awake()
    {
        //turn = Random.value > 0.5 ? 1 : -1;
        turn = -1;
       }

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
     
    }

    

}
