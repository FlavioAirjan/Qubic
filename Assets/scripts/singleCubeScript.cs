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
        identifyCubeType(gameObject.name, masterCubeSize-1);
        paintCube();
  

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

        
        /*
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
        */

        externCb = positionFrequence[0] + positionFrequence[masterCubeSize]>=1 ? true : false;
        cornerCb = positionFrequence[0] + positionFrequence[masterCubeSize] == 3 ? true : false;
        borderCb = positionFrequence[0] + positionFrequence[masterCubeSize] == 2 || cornerCb ? true : false;
        internCb = !externCb ? true : false;

        
        
        int count_interns = 0;


        int sum = 0;
        for (int i = 1; i < positionFrequence.Length-1; i++)
        {
            if (positionFrequence[i] > 0)
            {
                count_interns += positionFrequence[i];

             sum += (int)(Math.Pow(i,(float) 2))* positionFrequence[i];
            }
            

            if (i == positionFrequence.Length - 2)
            {

                if (count_interns == 2 && (positionFrequence[0] + positionFrequence[masterCubeSize] == 1))
                {
                    if (sum % 2 == 0)
                    {
                        Debug.Log("Sum:" + sum.ToString());
                        insideDiagCb = true;
                    }
                    else
                    {
                        insideDiagCb = false;
                    }
                }
                else if (count_interns == 3)
                {
                    if (sum % 2 == 1)
                    {
                        insideDiagCb = true;
                    }
                    else if ((masterCubeSize + 1) % 2 == 1)
                    {
                        if (positionFrequence[(masterCubeSize + 1) / 2] == 3)
                        {

                            insideDiagCb = true;
                        }
                    }
                    else
                    {
                        insideDiagCb = false;
                    }
                }
                else if (cornerCb)
                {
                    insideDiagCb = true;
                }
            }    
        }

        if ((masterCubeSize + 1) % 2 == 1)
        {
            Debug.Log("entrou");
            centerCb = insideDiagCb && (positionFrequence[(masterCubeSize + 1)/2] == 2) && (positionFrequence[0] + positionFrequence[masterCubeSize]  == 1) || (positionFrequence[(masterCubeSize + 1) / 2] == 3) ? true : false;
            
        }

        

        outsideDiagCb = (externCb && !cornerCb && !insideDiagCb && !centerCb) || (internCb && !insideDiagCb) ? true : false;

        /*
        {

            insideDiagCb = true;
        }
        */

        cornerCube = cornerCb ? true : false;
        borderCube = borderCb ? true : false;
        insideDiagCube = insideDiagCb ? true : false;
        outsideDiagCube = outsideDiagCb ? true : false;
        externCube = externCb ? true : false;
        internCube = internCb ? true : false;
        centerCube = centerCb ? true : false;

    }


    void paintCube()
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
