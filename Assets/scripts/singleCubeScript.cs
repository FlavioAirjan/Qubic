using UnityEngine;
using System.Collections;
using System;

public class singleCubeScript : MonoBehaviour {

    public int masterCubeSize;

    public bool externCube;
    public bool internCube;
    public bool cornerCube;


	// Use this for initialization
	void Start () {

        masterCubeSize = gameObject.GetComponentInParent<CubeScript>().cubeSize;
        identifyCubeType(gameObject.name, masterCubeSize-1);
        paintCube();

       
	}

    

    public void identifyCubeType(string position, int masterCubeSize)
    {
        

        //Identify CornerCubes.
        bool cornerCb = true;
        bool externCb = false;
        bool internCb = false;

        for (int i = 0; i < 3; i++)
        {
            //Se ele tiver alguma posição que seja diferente de 0 e diferente do (máximo tamanho do cubo-1)
            if (position[i].ToString() != "0" && position[i].ToString() != masterCubeSize.ToString())
            {
                //Então ele não é um cubo de quina.
                cornerCb = false;
            }
            //Senão, se ele tiver alguma posição 0 ou (máximo tamanho do cubo-1).
            else
            {
                //Então ele é um cubo externo.
                externCb = true;
            }
           
        }



        if (cornerCb)
        {
            cornerCube = true;
            externCube = true;
            internCube = false;
        }
        else if (externCb)
        {
            cornerCube = false;
            externCube = true;
            internCube = false;
        }
        else
        {
            internCube = true;
            cornerCube = false;
            externCube = false;
        }
    }


    void paintCube()
    {
        if (cornerCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (externCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if(internCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

	// Update is called once per frame
	void Update () {


	
	}
}
