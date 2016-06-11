using UnityEngine;
using System.Collections;
using System;

public class singleCubeScript : MonoBehaviour {

    public GameObject masterCube;

    public int masterCubeSize;



    //Cubos da quina (participam de 3 faces externas).
    public bool cornerCube;

    //Cubo que compõe o centro (quando houver) das faces externas e o centro interno do cubo.
    public bool centerCube;

    //Cubos da borda (participam de 2 ou mais faces externas[inclui o cornerCube]).
    public bool borderCube;

    //Cubos que compõem as diagonais internas ou externas do cubo (inclui cornerCube e centerCube).
    public bool insideDiagCube;

    //Cubos que não compõem as diagonais internas ou externas do cubo (inclui borderCube).
    public bool outsideDiagCube;

    //Cubos pertencentes às faces externas.
    public bool externCube;

    //Cubos não pertencentes às faces externas (internos).
    public bool internCube;

    
    public int NumOfPossibleMoves;
   
    


    //Contém os movimentos possíveis.
    public GameObject[] MyMoves;

   


    // Use this for initialization
    void Start () {
        
    externCube = false;
    internCube = false;
    cornerCube = false;
    borderCube = false;
    insideDiagCube = false;
    outsideDiagCube = false;
    centerCube = false;

    //Deve ser atualizado.
    NumOfPossibleMoves = 13;

        MyMoves = new GameObject[NumOfPossibleMoves];

        for (int i = 0; i < NumOfPossibleMoves; i++)
        {
            MyMoves[i] = Instantiate(gameObject.GetComponentInParent<CubeScript>().cubeMove);
            MyMoves[i].transform.parent = gameObject.transform;
            MyMoves[i].name = "Move" + i.ToString();
            MyMoves[i].GetComponent<Moviments>().setMove(i);
            MyMoves[i].GetComponent<Moviments>().validMoviment = true;
        }

        masterCube = gameObject.transform.parent.gameObject;
        masterCubeSize = gameObject.GetComponentInParent<CubeScript>().cubeSize;
        //identifyCubeType(gameObject.name, masterCubeSize-1);
       
  

    }

    public void identifyCubeType(string position, int masterCubeSize)
    {
        

        //Identify CornerCubes.
        bool cornerCb = true;
        bool externCb = false;
        bool borderCb = false;
        bool internCb = false;
        bool insideDiagCb = false;
        bool outsideDiagCb = false;
        bool centerCb = false;


        //Inicializa o vetor que contará a quantidade dos numeros da posição de um cubo.
        //Ex: Se a posição for 323 e o cubo for um 4x4x4, o vetor ficará: [0,0,1,2,0].
        int[] positionFrequence = new int[masterCubeSize + 1];

        for (int i = 0; i < masterCubeSize + 1; i++)
        {
            positionFrequence[i] = 0;
        }
        //Faz a contagem do vetor.
        for(int i = 0; i < 3; i++)
        {
            positionFrequence[(int)char.GetNumericValue(position[i])]++;
        }

        /*Debugging*/
        string debuggingPosition = "Position: " + position;
        string debuggingPositionFrequency = "Frequency: ";
        for (int i = 0; i < positionFrequence.Length; i++)
        {
            debuggingPositionFrequency += (positionFrequence[i]);
        }
        Debug.Log(debuggingPosition + "     " + debuggingPositionFrequency);
        /*END-OF-Debugging*/
        

        externCb = positionFrequence[0] + positionFrequence[masterCubeSize]>=1 ? true : false;
        cornerCb = positionFrequence[0] + positionFrequence[masterCubeSize] == 3 ? true : false;
        borderCb = positionFrequence[0] + positionFrequence[masterCubeSize] == 2 || cornerCb ? true : false;
        internCb = !externCb ? true : false;

        //Se for um cubo ímpar (3x3x3 ou 5x5x5, ...).
        if ((masterCubeSize + 1) % 2 == 1)
        {
            //Calcula o centro das faces externas e o centro interno do cubo.
            centerCb = (positionFrequence[(masterCubeSize + 1) / 2] == 2) && (positionFrequence[0] + positionFrequence[masterCubeSize] == 1) || (positionFrequence[(masterCubeSize + 1) / 2] == 3) ? true : false;

        }

        int count = 0;
        int[] targetCbPositionFrequence = new int[masterCubeSize + 1];

       
        var cubeSize = masterCubeSize + 1;
        for (int i  = 0; i < MyMoves.Length; i++)
        {
            

            //Se o movimento for válido e não for nenhum movimento que só ande em 1 sentido.
            if (MyMoves[i].GetComponent<Moviments>().validMoviment && i != 0 && i != 1 && i != 4)
            {


                int x1 = (int)char.GetNumericValue(position[0]);
                int y1 = (int)char.GetNumericValue(position[1]);
                int z1 = (int)char.GetNumericValue(position[2]);


                count = 0;
                while (count < cubeSize)
                {
                    //Limpa o vetor de frequencia de posição do cubo alvo.
                    for (int j = 0; j < masterCubeSize + 1; j++)
                    {
                        targetCbPositionFrequence[j] = 0;
                    }


                    x1 += MyMoves[i].GetComponent<Moviments>().x;
                    //x1 = Math.Abs(x1) % cubeSize;

                    y1 += MyMoves[i].GetComponent<Moviments>().y;
                    //y1 = Math.Abs(y1) % cubeSize;

                    z1 += MyMoves[i].GetComponent<Moviments>().z;
                    //z1 = Math.Abs(z1) % cubeSize;
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

                   
                    targetCbPositionFrequence[x1]++;
                    targetCbPositionFrequence[y1]++;
                    targetCbPositionFrequence[z1]++;


                    if (targetCbPositionFrequence[0] + targetCbPositionFrequence[masterCubeSize] == 3)
                    {

                       
                        insideDiagCb = true;

                    }

                    count++;
                }

                }
            }



        outsideDiagCb = (externCb && !cornerCb && !insideDiagCb && !centerCb) || (internCb && !insideDiagCb) ? true : false;



        cornerCube = cornerCb ? true : false;
        borderCube = borderCb ? true : false;
        insideDiagCube = insideDiagCb ? true : false;
        outsideDiagCube = outsideDiagCb ? true : false;
        externCube = externCb ? true : false;
        internCube = internCb ? true : false;
        centerCube = centerCb ? true : false;

    }


    public void paintCube()
    {
        
        //Do mais abrangente ao menos abrangente.
        if (externCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            
        }

        if (internCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }

        if (insideDiagCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }

        if (outsideDiagCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (borderCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        
       

        if (cornerCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if (centerCube)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        

    }

    

    

  

	// Update is called once per frame
	void Update () {
        
    }
}
