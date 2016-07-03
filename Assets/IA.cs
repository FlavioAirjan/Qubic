using UnityEngine;
using System.Collections;
using System;

public class IA : MonoBehaviour {

    public GameObject GameManager;
    public GameObject MasterCube;
 


	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager");
        MasterCube = GameObject.Find("MasterCube");
    }
	
	// Update is called once per frame
	void Update () {

        Play();
        //Debug.Log(MasterCube.GetComponent<CubeScript>().Moves[0].GetComponent<Moviments>().value);
    }

    public void setAllFakeValuesToZero()
    {
        for (int i = 0; i < MasterCube.GetComponent<CubeScript>().cubes.Length; i++)
        {
            for (int j = 0; j < MasterCube.GetComponent<CubeScript>().cubes[i].Length; j++)
            {
                for (int k = 0; k < MasterCube.GetComponent<CubeScript>().cubes[i][j].Length; k++)
                {
                    MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue = 0;
                }
            }
        }

    }

    public void setFakeValuesToOne()
    {
        for (int i = 0; i < MasterCube.GetComponent<CubeScript>().cubes.Length; i++)
        {
            for (int j = 0; j < MasterCube.GetComponent<CubeScript>().cubes[i].Length; j++)
            {
                for (int k = 0; k < MasterCube.GetComponent<CubeScript>().cubes[i][j].Length; k++)
                {
                    MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue = MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().value;
                    
                }
            }
        }

    }

    public int evaluate()
    {
        int sum = 0;
        int n = 0;
        foreach (GameObject move in MasterCube.GetComponent<CubeScript>().Moves)
        {
            n = move.GetComponent<Moviments>().fakeValue;
            if (n!= 0)
            {
                // ((10^|n|)*(n/|n|));
                sum += (int) (Math.Pow(10, Math.Abs(n))*((n/Math.Abs(n))));

            }
        }



        return sum;
    }

    public int MinMax(bool turn, int deep)
    {
       
       int point = 0;
        int bestPoint;
        if (turn == true)
        {
            bestPoint = int.MinValue;
        }
        else
        {
            bestPoint = int.MaxValue;
        }


        if (deep == 0)
        {
            //Deve retornar o valor do tabuleiro inteiro.
            foreach (GameObject move in MasterCube.GetComponent<CubeScript>().Moves) {
            move.GetComponent<Moviments>().updateValuePath();
            move.GetComponent<Moviments>().updateFakeValuePath();
            }
            int result = evaluate();
            return result;
        }

        for (int i = 0; i < MasterCube.GetComponent<CubeScript>().cubes.Length; i++)
        {
            for (int j = 0; j < MasterCube.GetComponent<CubeScript>().cubes[i].Length; j++)
            {
                for (int k = 0; k < MasterCube.GetComponent<CubeScript>().cubes[i][j].Length; k++)
                {
                    //Se não estiver marcado.
                    if(MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue == 0)
                    {
                        MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue = 1;
                       
                        

                        point = MinMax(!turn, deep-1);
                        if (turn)
                        {
                            if (point >= bestPoint)
                            {
                                bestPoint = point;
                            }

                        }
                        else
                        {
                            if (point <= bestPoint)
                            {
                                bestPoint = point;
                            }

                        }
                        
                        
                        MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue = 0;


                    }
                    
                }
            }
        }
        


        return bestPoint;
    }

    public void Play()
    {
        GameObject bestMatch;
        GameObject auxBestMatch;
        int auxBestPoint = 0;
        int bestPoint = int.MinValue;

        //Se tiver na vez do computador.
        if (GameManager.GetComponent<GameManagerScript>().turn == 1)
        {
           // Debug.Log("Computador joga");
            setFakeValuesToOne();

            bestMatch = MasterCube.GetComponent<CubeScript>().cubes[0][0][0];

            for (int i = 0; i < MasterCube.GetComponent<CubeScript>().cubes.Length; i++)
            {
                for (int j = 0; j < MasterCube.GetComponent<CubeScript>().cubes[i].Length; j++)
                {
                    for (int k = 0; k < MasterCube.GetComponent<CubeScript>().cubes[i][j].Length; k++)
                    {
                        //Se não está setado.
                        if (MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue == 0)
                        { 
                        
                            auxBestMatch = MasterCube.GetComponent<CubeScript>().cubes[i][j][k];
                            MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue = 1;
                            
                                //Starts with Max(true).
                                 auxBestPoint = MinMax(false, 1);

                            if (Math.Abs(auxBestPoint) >= bestPoint)
                            {

                                bestPoint = auxBestPoint;
                                bestMatch = auxBestMatch;

                            }
                            
                            
                            MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<singleCubeScript>().fakeValue = 0;
                        }

                    }
                }
            }

            bestMatch.GetComponent<singleCubeScript>().gameObject.GetComponent<Renderer>().material.color = Color.red;
            bestMatch.GetComponent<singleCubeScript>().value = 1;
            //
            //MasterCube.GetComponent<CubeScript>().Moves[0].GetComponent<Moviments>().cubesOnThisMovement[0].GetComponent<singleCubeScript>().value = 1;

            //MasterCube.GetComponent<CubeScript>().Moves[0].GetComponent<Moviments>().cubesOnThisMovement[0].GetComponent<singleCubeScript>().gameObject.GetComponent<Renderer>().material.color = Color.white;


            //Passa o turno.

            GameManager.GetComponent<GameManagerScript>().turn *= -1;

            setAllFakeValuesToZero();
            Debug.Log("Término Jogada Computador");
        }

    }
}
