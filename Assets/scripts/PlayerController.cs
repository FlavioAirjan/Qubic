using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject CenterMasterCube;
    public GameObject MasterCube;
    private GameObject GameManager;
    private GameObject Computer;

    // Use this for initialization
    void Start () {
        CenterMasterCube = GameObject.Find("CenterMasterCube");
           
        GameManager = GameObject.Find("GameManager");
        Computer = GameObject.Find("Computador");
        MasterCube = GameObject.Find("MasterCube");
        if (MasterCube == null)
            {
                 MasterCube = GameObject.Find("CenterMasterCube/MasterCube");
            }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            reScaleMasterCube(0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            reScaleMasterCube(-0.1f);
        }
            rotateMasterCube();

        Play();
    }

    void rotateMasterCube()
    {
        float horizontalRotation = Input.GetAxis("Horizontal");
        float verticalRotation = Input.GetAxis("Vertical");

        
        CenterMasterCube.transform.transform.Rotate(verticalRotation, -1*horizontalRotation, 0);

    }

    void Play()
    {


        if (Input.GetMouseButtonDown(0) && GameManager.GetComponent<GameManagerScript>().turn == -1 && !MasterCube.GetComponent<CubeScript>().gameover)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Destroy(hit.transform.gameObject);
                //Debug.Log(hit.transform.gameObject.GetComponent<singleCubeScript>().value);

                if (hit.transform.gameObject.GetComponent<singleCubeScript>().value == 0)
                {

                    hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    hit.transform.gameObject.GetComponent<singleCubeScript>().value = -1; // O valor do cubo será 1.

                    int x = hit.transform.gameObject.GetComponent<singleCubeScript>().position[0];
                    int y = hit.transform.gameObject.GetComponent<singleCubeScript>().position[1];
                    int z = hit.transform.gameObject.GetComponent<singleCubeScript>().position[2];


                    MasterCube.GetComponent<CubeScript>().masterCube[x][y][z].setValue(-1);

                    //GameManager.GetComponent<GameManagerScript>().PaintedCubes.Add(this.gameObject);
                    GameManager.GetComponent<GameManagerScript>().turn *= -1;
                    GameManager.GetComponent<GameManagerScript>().setGameTurnText("Computer");

                    //MasterCube.GetComponent<CubeScript>().gameover = StartCoroutine(Computer.GetComponent<IA>().Play());
                    MasterCube.GetComponent<CubeScript>().gameover = Computer.GetComponent<IA>().Play();

                }
                //Se a IA retornar gameOver entao o cubo é restartado com os valores 0 e volta as cores iniciais.
                if (MasterCube.GetComponent<CubeScript>().gameover)
                {
                    MasterCube.GetComponent<CubeScript>().restartCube();
                    MasterCube.GetComponent<CubeScript>().reDraw();
                    MasterCube.GetComponent<CubeScript>().gameover = false;
                    MasterCube.GetComponent<CubeScript>().GameManager.GetComponent<GameManagerScript>().turn = -1;
                    GameManager.GetComponent<GameManagerScript>().setGameTurnText("You");
                    GameManager.GetComponent<GameManagerScript>().setGameResultText("In Progress");
                }
            }
        }

       
    }

    void reScaleMasterCube(float value)
    {
       
       
        for (int i = 0; i < MasterCube.GetComponent<CubeScript>().masterCube.Length; i++)
        {
            for (int j = 0; j < MasterCube.GetComponent<CubeScript>().masterCube[i].Length; j++)
            {

                for (int k = 0; k < MasterCube.GetComponent<CubeScript>().masterCube[i][j].Length; k++)
                {
                    if (value == 0.1f)
                    {
                        if (MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.x <= 0.9
                         || MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.y <= 0.9
                         || MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.z <= 0.9)
                        {
                            MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale = new Vector3(MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.x + value, MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.y + value, MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.z + value);
                        }
                    }
                    else if (value == -0.1f)
                    {
                       
                        if (MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.x >= 0.3
                         || MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.y >= 0.3
                         || MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.z >= 0.3)
                        {
                            MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale = new Vector3(MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.x + value, MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.y + value, MasterCube.GetComponent<CubeScript>().cubes[i][j][k].GetComponent<Transform>().localScale.z + value);
                        }
                    }
                    else
                    {
                        break;
                    }


                
                }
            }
        }
        }

    }


