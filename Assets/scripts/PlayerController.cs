using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject CenterMasterCube;
    public GameObject MasterCube;
    private GameObject GameManager;
    private GameObject Computer;
    private Vector2 touchOrigin = -Vector2.one;
    private bool isToutch;
    public bool canPlay;
    private TouchPhase lastTouch;
    // Use this for initialization
    void Start () {
        CenterMasterCube = GameObject.Find("CenterMasterCube");
        isToutch = true;
     GameManager = GameObject.Find("GameManager");
        Computer = GameObject.Find("Computador");
        MasterCube = GameObject.Find("MasterCube");
        if (MasterCube == null)
            {
                 MasterCube = GameObject.Find("CenterMasterCube/MasterCube");
            }
        lastTouch = TouchPhase.Stationary;
        canPlay = true;
    }
        
        IEnumerator wait(float time){
        yield return new WaitForSeconds(time);
        isToutch=!isToutch;
    }

    // Update is called once per frame
    void Update () {

        if (canPlay)
        {

            float horizontal = 0;     //Used to store the horizontal move direction.
            float vertical = 0;       //Used to store the vertical move direction.

            //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 1)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    if (deltaMagnitudeDiff > 0)
                    {
                        reScaleMasterCube(0.1f);
                    }
                    else if (deltaMagnitudeDiff < 0)
                    {
                        reScaleMasterCube(-0.1f);
                    }
                }


                //rotation and click
                //Check if Input has registered more than zero touches
                if (Input.touchCount > 0)
                {
                    //Store the first touch detected.
                    Touch myTouch = Input.touches[0];

                    //Check if the phase of that touch equals Began
                    if (myTouch.phase == TouchPhase.Began)
                    {
                        //If so, set touchOrigin to the position of that touch
                        touchOrigin = myTouch.position;
                    }

                    if (isToutch)
                    {

                        //Set touchEnd to equal the position of this touch
                        Vector2 touchEnd = myTouch.position;

                        //Calculate the difference between the beginning and end of the touch on the x axis.
                        float x = touchEnd.x - touchOrigin.x;

                        //Calculate the difference between the beginning and end of the touch on the y axis.
                        float y = touchEnd.y - touchOrigin.y;

                        if (x > 0)
                        {
                            horizontal = 1;
                        }
                        else if (x < 0)
                        {
                            horizontal = -1;
                        }

                        if (y > 0)
                        {
                            vertical = 1;
                        }
                        else if (y < 0)
                        {
                            vertical = -1;
                        }

                        rotateMasterCube(vertical, horizontal);
                        //Se é click
                        Ray ray = Camera.main.ScreenPointToRay(touchEnd);


                        float distance = Vector2.Distance(touchOrigin, touchEnd);

                        if (distance<0.3f && myTouch.phase == TouchPhase.Ended && lastTouch!= TouchPhase.Moved)
                        {
                            Play(ray);
                            isToutch = false;
                            StartCoroutine(wait(0.2f));
                        }
                        lastTouch = myTouch.phase;

                        touchOrigin = touchEnd;

                    }


                }
            }
            else
            {


                if (Input.GetKeyDown("z"))
                {
                    reScaleMasterCube(0.1f);
                }
                else if (Input.GetKeyDown("x"))
                {
                    reScaleMasterCube(-0.1f);
                }

                //rotation
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
                rotateMasterCube(vertical, horizontal);
                //click
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Input.GetMouseButtonDown(0))
                {
                    Play(ray);
                }
            }
        }

       
        
    }

    void rotateMasterCube(float verticalRotation,float horizontalRotation)
    {
        
        CenterMasterCube.transform.transform.Rotate(verticalRotation, -1*horizontalRotation, 0);

    }

    void Play(Ray ray)
    {

      
            if ( GameManager.GetComponent<GameManagerScript>().turn == -1 && !MasterCube.GetComponent<CubeScript>().gameover)
            {

                
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


