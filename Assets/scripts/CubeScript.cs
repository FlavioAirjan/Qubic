using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CubeScript : MonoBehaviour {

    public float spaceBetweenCubes;
    public int cubeSize;

    private GameObject QubicInfo;


    public GameObject[][][] cubes;

    public GameObject GameManager;
    public GameObject Computer;

    public GameObject cubeMove;

    //List of all Moves (without repeat).
    public List<GameObject> Moves;


    //Vetor/Matriz de movimentos.
    public List<SimulatedMoviments> SimulatedMoves;

    //Vetor/Matriz que representa o cubo.
    public SimulatedCube[][][] masterCube;

    public bool gameover;


    // Use this for initialization


    void generateMoveList()
    {
        for (int i = 0; i < masterCube.Length; i++)
        {
            for (int j = 0; j < masterCube[i].Length; j++)
            {
                for (int k = 0; k < masterCube[i][j].Length; k++)
                {
                    //Pra cada movimento do cubo.
                    foreach (GameObject move in cubes[i][j][k].GetComponent<singleCubeScript>().MyMoves)
                    {
                        //Se for um movimento válido.
                        if (move.GetComponent<Moviments>().validMovement)
                        {
                            if (!move.GetComponent<Moviments>().insideList)
                            { 
                            //Adiciona o movimento à lista.
                            Moves.Add(move);
                            //Seta o movimento como falso. (pois não irá colocar o mesmo movimento para os outros cubos).
                            move.GetComponent<Moviments>().insideList = true;

                                //Para cada cubo que tem o mesmo movimento.
                                
                                foreach (GameObject cube in move.GetComponent<Moviments>().cubesOnThisMovement)
                                {

                                    foreach (GameObject move2 in cube.GetComponent<singleCubeScript>().MyMoves)
                                    {
                                        if (move2.name == move.name)
                                        {
                                            move2.GetComponent<Moviments>().insideList = true;

                                        }
                                    }

                                }
                           
                          }
                        }
                        
                    }
                    

                }
            }
        }

    }

    public void copyMoveListToSimulatedMoveList()
    {
       
        foreach (GameObject move in Moves)
        {
            SimulatedMoviments simulatedMove = new SimulatedMoviments(cubeSize);
            simulatedMove.countFreeCubes = move.GetComponent<Moviments>().countFreeCubes;

            for (int i = 0; i < cubeSize; i++)
            {
                // move.GetComponent<Moviments>().cubesOnThisMovement
                simulatedMove.cubesOnThisMovement[i] = masterCube[move.GetComponent<Moviments>().cubesOnThisMovement[i].GetComponent<singleCubeScript>().position[0]][move.GetComponent<Moviments>().cubesOnThisMovement[i].GetComponent<singleCubeScript>().position[1]][move.GetComponent<Moviments>().cubesOnThisMovement[i].GetComponent<singleCubeScript>().position[2]];
            }
            
            SimulatedMoves.Add(simulatedMove);



        }

        
        /*
        foreach (SimulatedMoviments move in SimulatedMoves)
        {
            Debug.Log("CountFreeCubes: " + move.countFreeCubes.ToString() + "MovimentValue: " + move.value.ToString());
            foreach(SimulatedCube cube in move.cubesOnThisMovement) {
                Debug.Log("Positions: " + cube.pos[0].ToString() + cube.pos[1].ToString() + cube.pos[2].ToString());
                    }

        }

*/        

    }

    void Start () {

        GameManager = GameObject.Find("GameManager");
        Computer = GameObject.Find("Computador");

        try
        {
            QubicInfo = GameObject.Find("QubicInfo");
            cubeSize = QubicInfo.GetComponent<QubicInfo>().cubeSize;
        }
        catch
        {
            Debug.Log("QubicInfoNotFound");
        }

        Moves = new List<GameObject>();
        SimulatedMoves = new List<SimulatedMoviments>();
        gameover = false;
        //Integer Matrix Cube.
        createCube();

        //Build the Cube (Object).
        drawCube();
        checkValidMoviments();
        generateMoveList();
       
        copyMoveListToSimulatedMoveList();

        /* foreach (GameObject i in Moves)
         {
             foreach (GameObject j in i.GetComponent<Moviments>().cubesOnThisMovement)
             {
                 Debug.Log(j.GetComponent<singleCubeScript>().position[0].ToString() + j.GetComponent<singleCubeScript>().position[1].ToString() + j.GetComponent<singleCubeScript>().position[2].ToString());
             }
             Debug.Log("\n\n");
         }*/

        
    }

    public void createCube()
    {
        masterCube = new SimulatedCube[cubeSize][][];
        cubes = new GameObject[cubeSize][][];
        for (int i = 0; i < masterCube.Length; i++)
        {
            masterCube[i] = new SimulatedCube[cubeSize][];
            cubes[i] = new GameObject[cubeSize][];

            for (int j = 0; j < masterCube[i].Length; j++)
            {
                masterCube[i][j] = new SimulatedCube[cubeSize];
                cubes[i][j] = new GameObject[cubeSize];

                for (int k = 0; k < masterCube[i][j].Length; k++)
                {
                    masterCube[i][j][k] = new SimulatedCube();
                    masterCube[i][j][k].setPosition(i, j, k);
                    cubes[i][j][k] = new GameObject();
                }
            }
        }
        
    }

    public void drawCube()
    {
        for (int i = 0; i < masterCube.Length; i++)
        {
            for (int j = 0; j < masterCube[i].Length; j++)
            {

                for (int k = 0; k < masterCube[i][j].Length; k++)
                {

                    cubes[i][j][k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[i][j][k].transform.parent = gameObject.transform;
                    cubes[i][j][k].transform.position = new Vector3(i, j, k);
                    cubes[i][j][k].GetComponent<Transform>().localScale = new Vector3(cubes[i][j][k].GetComponent<Transform>().localScale.x - spaceBetweenCubes, cubes[i][j][k].GetComponent<Transform>().localScale.y - spaceBetweenCubes, cubes[i][j][k].GetComponent<Transform>().localScale.z - spaceBetweenCubes);

                    cubes[i][j][k].GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
                    cubes[i][j][k].GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.6f);
                    cubes[i][j][k].GetComponent<Renderer>().material.SetFloat("_Mode", 3);

                    cubes[i][j][k].GetComponent<Renderer>().material.SetFloat("_Metallic", 0f);
                    cubes[i][j][k].GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.115f);
                    cubes[i][j][k].name = i.ToString() + j.ToString() + k.ToString();
                    cubes[i][j][k].AddComponent<singleCubeScript>();

                    //Inicializa os cubos.
                    cubes[i][j][k].GetComponentInChildren<singleCubeScript>().InitializeCube();
                    cubes[i][j][k].GetComponentInChildren<singleCubeScript>().setPosition(i,j,k);


                }
            }
        }

    }

    //Recolore os cubos
    public void reDraw()
    {
        for (int i = 0; i < masterCube.Length; i++)
        {
            for (int j = 0; j < masterCube[i].Length; j++)
            {

                for (int k = 0; k < masterCube[i][j].Length; k++)
                {


                    cubes[i][j][k].GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
                    cubes[i][j][k].GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.6f);
                    cubes[i][j][k].GetComponent<Renderer>().material.SetFloat("_Mode", 3);

                    cubes[i][j][k].GetComponent<Renderer>().material.SetFloat("_Metallic", 0f);
                    cubes[i][j][k].GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.115f);
                    cubes[i][j][k].name = i.ToString() + j.ToString() + k.ToString();


                }
            }
        }

    }


    public void checkValidMoviments()
    {

        int indexMoviment = 0;
        int count = 0;
        GameObject currentMove;
        int x1 = 0;
        int y1 = 0;
        int z1 = 0;

        //Variável que irá contar quantas vezes o movimento saiu do cubo (se sair mais que 2 vezes, significa que o movimento é inválido).
        int estourou = 0;
       

        for (int i = 0; i < masterCube.Length; i++)
        {
            for (int j = 0; j < masterCube[i].Length; j++)
            {
                for (int k = 0; k < masterCube[i][j].Length; k++)
                {
                    //Inicializa os cubos.
                    //cubes[i][j][k].GetComponentInChildren<singleCubeScript>().InitializeCube();

                    while (indexMoviment < cubes[i][j][k].gameObject.GetComponentInChildren<singleCubeScript>().MyMoves.Length)
                    {
                        x1 = i;
                        y1 = j;
                        z1 = k;

                        currentMove = cubes[i][j][k].gameObject.GetComponentInChildren<singleCubeScript>().MyMoves[indexMoviment].gameObject;
                        //Debug.Log("MovimentoCorrente: " + currentMove.GetComponent<Moviments>().x + "" + currentMove.GetComponent<Moviments>().y + "" + currentMove.GetComponent<Moviments>().z + "");
                        
                        while (count < cubeSize)
                        {
                            x1 += currentMove.gameObject.GetComponent<Moviments>().x;
                            //x1 = Math.Abs(x1) % cubeSize;
                            
                            y1 += currentMove.GetComponent<Moviments>().y;
                            //y1 = Math.Abs(y1) % cubeSize;
                            
                            z1 += currentMove.GetComponent<Moviments>().z;
                            //z1 = Math.Abs(z1) % cubeSize;

                            if (z1 >= cubeSize || z1 < 0 || x1 >= cubeSize || x1 < 0 || y1 >= cubeSize || y1 < 0)
                            {
                                estourou++;
                            }

                            if (x1 < 0)
                            {
                                x1 += cubeSize;

                            }
                            else
                            {
                                x1 = Math.Abs(x1) % cubeSize;
                            }

                            if (y1 < 0)
                            {
                                y1 += cubeSize;

                            }
                            else
                            {
                                y1 = Math.Abs(y1) % cubeSize;
                            }

                            if (z1 < 0)
                            {
                                z1 += cubeSize;
                               
                            }
                            else
                            { 
                            z1 = Math.Abs(z1) % cubeSize;
                            }

                            
                            

                            cubes[i][j][k].GetComponent<singleCubeScript>().MyMoves[indexMoviment].GetComponent<Moviments>().cubesOnThisMovement[count] = cubes[x1][y1][z1];
                          
                            count++;
                        }
                        

                        //if (x1 != i || y1 != j || z1 != k)
                        if(estourou > 1)
                        {
                            //Debug.Log("Invalid Moviment");
                            cubes[i][j][k].gameObject.GetComponentInChildren<singleCubeScript>().NumOfPossibleMoves--;
                            cubes[i][j][k].gameObject.GetComponentInChildren<singleCubeScript>().MyMoves[indexMoviment].GetComponent<Moviments>().validMovement = false;
                        }
                        else
                        {
                        //    Debug.Log(x1 + "" + y1 + "" + z1);
                        }
                        //Debug.Log(cubes[i][j][k].GetComponentInChildren<singleCubeScript>().MyMoves[indexMoviment].GetComponent<Moviments>().x + "" + cubes[i][j][k].GetComponentInChildren<singleCubeScript>().MyMoves[indexMoviment].GetComponent<Moviments>().y + "" + cubes[i][j][k].GetComponentInChildren<singleCubeScript>().MyMoves[indexMoviment].GetComponent<Moviments>().z);
                        indexMoviment++;
                        count = 0;
                        estourou = 0;
                    }
                    
                    indexMoviment = 0;

                    
                    cubes[i][j][k].GetComponentInChildren<singleCubeScript>().identifyCubeType(cubes[i][j][k].name, cubeSize-1);
                    //cubes[i][j][k].GetComponentInChildren<singleCubeScript>().paintCube();

                }
            }
        }

    }

    //Seta os valores do cubo para 0
    public void restartCube()
    {
       
        for (int i = 0; i < masterCube.Length; i++)
        {
            for (int j = 0; j < masterCube[i].Length; j++)
            {

                for (int k = 0; k < masterCube[i][j].Length; k++)
                {
                    masterCube[i][j][k].value = 0;
                    masterCube[i][j][k].setPosition(i, j, k);

                    cubes[i][j][k].GetComponent<singleCubeScript>().value = 0;
                }
            }
        }

    }


    // Update is called once per frame
    void Update () {
      

    }

    

}
