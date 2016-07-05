using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class IA : MonoBehaviour {

    public int MAX = int.MaxValue;
    public int MIN = int.MinValue;

    public int bestPlayIterator;

    public GameObject GameManager;
    public GameObject MasterCube;
    public bool hasDataToPlay;
    

    public int depth = 3;
    

    public List<SimulatedCube> bestCubesToPlay;

    public int boardValue = 0;

	// Use this for initialization
	void Start () {

        hasDataToPlay = false;

        bestPlayIterator = 0;
        bestCubesToPlay = new List<SimulatedCube>();

        for (int i = 0; i < depth; i++)
        {
            bestCubesToPlay.Add(new SimulatedCube());
        }
        
        GameManager = GameObject.Find("GameManager");
        MasterCube = GameObject.Find("MasterCube");

    }
	
	
    public int evaluate()
    {
        
        int sum = 0;
        int n = 0;
        foreach (SimulatedMoviments move in MasterCube.GetComponent<CubeScript>().SimulatedMoves)
        {
            move.updateValuePath();

            n = move.value;

            if (n != 0)
            {
                // ((10^|n|)*(n/|n|));
                sum += (int)(Math.Pow(10, Math.Abs(n)) * ((n / Math.Abs(n))));

            }
            else
            {
                sum = 0;
            }
        }

        return sum;
    }



    public int MinMax(SimulatedCube [][][] masterCube, int max, int min, int currentDepth, bool turn)
    {
        
        int v = 0;
        int v_linha = 0;
        
        if (currentDepth == depth)
        {
           
            boardValue = evaluate();

            return boardValue;
            
        }

        //Se for max.
        if (turn)
        {
            
            v = min;

            for (int i = 0; i < masterCube.Length; i++)
            {
                for (int j = 0; j < masterCube.Length; j++)
                {
                    for (int k = 0; k < masterCube.Length; k++)
                    {
                       
                        //Se não tiver marcado.
                        if (masterCube[i][j][k].value == 0)
                        {
                            masterCube[i][j][k].value = 1;

                            v_linha = MinMax(masterCube, max, v, currentDepth + 1, !turn);

                            if (v_linha > v)
                            {

                                v = v_linha;
                                bestCubesToPlay[currentDepth].value = 1;
                                bestCubesToPlay[currentDepth].pos[0] = i;
                                bestCubesToPlay[currentDepth].pos[1] = j;
                                bestCubesToPlay[currentDepth].pos[2] = k;
                                
                            }
                            masterCube[i][j][k].value = 0;

                            if (v > max)
                            {

                                return max;
                            }

                        }

                    }
                }
            }

            return v;
        }

        //Se for min
        if (!turn)
        {
            v = max;
            for (int i = 0; i < masterCube.Length; i++)
            {
                for (int j = 0; j < masterCube.Length; j++)
                {
                    for (int k = 0; k < masterCube.Length; k++)
                    {
                        masterCube[i][j][k].value = -1;

                        v_linha = MinMax(masterCube, v, min, currentDepth + 1, !turn);

                        if (v_linha < v)
                        {
                            v = v_linha;
                            bestCubesToPlay[currentDepth].value = -1;
                            bestCubesToPlay[currentDepth].pos[0] = i;
                            bestCubesToPlay[currentDepth].pos[1] = j;
                            bestCubesToPlay[currentDepth].pos[2] = k;
                        }
                        masterCube[i][j][k].value = 0;
                        if (v < min)
                        {
                            return min;
                        }

                    }
                }
            }
            return v;

        }

        Debug.Log("Erro");

        return 0;

        
    }

    

    public void Play()
    {
        /*
        if (!hasDataToPlay) { 
            MinMax(MasterCube.GetComponent<CubeScript>().masterCube, MAX, MIN, 0, true);
            hasDataToPlay = true;
            bestPlayIterator = 0;

        }
        
        if (hasDataToPlay)
        {
            MasterCube.GetComponent<CubeScript>().cubes[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].GetComponent<Renderer>().material.color = Color.yellow;
            MasterCube.GetComponent<CubeScript>().masterCube[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].setValue(1);
            bestPlayIterator +=2;

            if (bestPlayIterator > bestCubesToPlay.Count - 1)
            {
                hasDataToPlay = false;
            }

        }
        */
        MinMax(MasterCube.GetComponent<CubeScript>().masterCube, MAX, MIN, 0, true);
        MasterCube.GetComponent<CubeScript>().cubes[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].GetComponent<Renderer>().material.color = Color.yellow;
        MasterCube.GetComponent<CubeScript>().masterCube[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].setValue(1);
        /*
        foreach (SimulatedCube cube in bestCubesToPlay)
        {
           
            Debug.Log(cube.pos[0].ToString() + cube.pos[1].ToString() + cube.pos[2].ToString());
            
            if (cube.value == 1)
            {
                MasterCube.GetComponent<CubeScript>().cubes[cube.pos[0]][cube.pos[1]][cube.pos[2]].GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                MasterCube.GetComponent<CubeScript>().cubes[cube.pos[0]][cube.pos[1]][cube.pos[2]].GetComponent<Renderer>().material.color = Color.red;
            }

            
        }    
        */
        GameManager.GetComponent<GameManagerScript>().turn *= -1;

            Debug.Log("Término Jogada Computador");

    }
}
