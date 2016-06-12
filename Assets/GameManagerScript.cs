using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

    //-1 = player.
    // 0 = GameStop.
    //1 = computer.
    public int turn;

    //Recebe os cubos pintados.
    public List<GameObject> PaintedCubes;

    void Awake()
    {
        turn = Random.value > 0.5 ? 1 : -1;
        PaintedCubes = new List<GameObject>();


    }

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (PaintedCubes.Count > 0)
        {

            Debug.Log(PaintedCubes.Count);
        }
        */
    }
}
