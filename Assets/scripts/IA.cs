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

    public int maxValue;
    public int minValue;

    public int depth = 5;
    

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
       

        //Valores max e min que identificam se acabou o jogo com vitória ou derrota
        maxValue = (int) Math.Pow(10,MasterCube.GetComponent<CubeScript>().cubeSize);
        minValue = -1*(int) Math.Pow(10, MasterCube.GetComponent<CubeScript>().cubeSize-1);
        //Debug.Log("teste"+ maxValue + " "+minValue);

    }
	
	
    public int evaluate()
    {
        
        int sum = 0;
        int n = 0;
        int value;
        bool temMin = false;
        bool lose = false;
        foreach (SimulatedMoviments move in MasterCube.GetComponent<CubeScript>().SimulatedMoves)
        {
            move.updateValuePath();

            n = move.value;


            if (n != 0)
            {
                // ((10^|n|)*(n/|n|));
                value=(int)(Math.Pow(10, Math.Abs(n)) * ((n / Math.Abs(n))));
                //se ganhou o jogo
                if (value >= maxValue)
                {
                    return maxValue;
                }
                //se está prestes a perder
                if (value <= minValue)
                {
                    temMin = true;
                }
                //se perdeu o jogo
                if (value <= minValue*10)
                {
                    lose = true;
                }
                sum += value;
            }
            else
            {
                sum = 0;
            }
        }

        //retorna se perdeu
        if (lose==true)
        {
            return minValue*10;
        }
        //retorna que está prestes a perder
        else if (temMin)
        {
            return minValue ;
        }
        else
        {
            return sum;
        }
       
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
        /*boardValue = evaluate();
        if (boardValue==maxValue)
        {

        }*/

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
                            boardValue=evaluate();
                            //Se não tiver acabado
                            if (boardValue==maxValue) {
                                v_linha = maxValue;
                            }
                            else if (boardValue <= minValue*10)
                            {
                                v_linha = minValue;
                            }
                            else
                            {
                                v_linha = MinMax(masterCube, max, v, currentDepth + 1, !turn);
                            }

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
                        if (masterCube[i][j][k].value == 0)
                        {
                            masterCube[i][j][k].value = -1;

                            boardValue = evaluate();
                            //Se não tiver acabado
                            if (boardValue <= minValue*10)
                            {
                                v_linha = minValue*10;
                            }
                            else
                            {
                                v_linha = MinMax(masterCube, v, min, currentDepth + 1, !turn);
                            }

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
            }
            return v;

        }

        Debug.Log("Erro");

        return 0;

        
    }

    
    //Play retorna se o jogo terminou ou não, se não, ele chama minimax, faz a jogada e troca o turno.
    public bool Play()
    {

        int aux = evaluate();
        if (aux == maxValue)
        {
            Debug.Log("Que pena, você perdeu!");
            return true;
        }
        else if (aux == minValue * 10)
        {
            Debug.Log("Parabéns, você ganhou!");
            return true;
        }
        else
        {

            MinMax(MasterCube.GetComponent<CubeScript>().masterCube, MAX, MIN, 0, true);
            MasterCube.GetComponent<CubeScript>().cubes[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].GetComponent<Renderer>().material.color = Color.yellow;
            MasterCube.GetComponent<CubeScript>().masterCube[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].setValue(1);
            MasterCube.GetComponent<CubeScript>().cubes[bestCubesToPlay[bestPlayIterator].pos[0]][bestCubesToPlay[bestPlayIterator].pos[1]][bestCubesToPlay[bestPlayIterator].pos[2]].GetComponent<singleCubeScript>().value = 1;
            if (aux == maxValue)
            {
                Debug.Log("Que pena, você perdeu!");
                return true;
            }else if (aux == minValue * 10)
            {
                Debug.Log("Parabéns, você ganhou!");
                return true;
            }

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
            return false;
        }

    }
}
