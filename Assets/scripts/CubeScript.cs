using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

    public float spaceBetweenCubes;
    public int cubeSize;
    public int[][][] masterCube;

	// Use this for initialization
	void Start () {
        masterCube = createCube(masterCube, cubeSize);
        drawCube(masterCube);
       
    }

    public int[][][] createCube(int [][][] masterCube, int cubeSize)
    {
        masterCube = new int[cubeSize][][];

        for (int i = 0; i < masterCube.Length; i++)
        {
            masterCube[i] = new int[cubeSize][];
            for (int j = 0; j < masterCube[i].Length; j++)
            {
                masterCube[i][j] = new int[cubeSize];

                for (int k = 0; k < masterCube[i][j].Length; k++)
                { 
                    masterCube[i][j][k] = new int();
                }
            }
        }

        return masterCube;
    }

    public void drawCube(int [][][] masterCube)
    {
        for (int i = 0; i < masterCube.Length; i++)
        {
            for (int j = 0; j < masterCube[i].Length; j++)
            {

                for (int k = 0; k < masterCube[i][j].Length; k++)
                {
                    
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.parent = gameObject.transform;
                    cube.transform.position = new Vector3(i, j, k);
                    cube.GetComponent<Transform>().localScale = new Vector3(cube.GetComponent<Transform>().localScale.x - spaceBetweenCubes, cube.GetComponent<Transform>().localScale.y - spaceBetweenCubes, cube.GetComponent<Transform>().localScale.z - spaceBetweenCubes);
                    
                    

                    cube.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
                    cube.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.6f);
                    cube.GetComponent<Renderer>().material.SetFloat("_Mode", 3);

                    
                    cube.GetComponent<Renderer>().material.SetFloat("_Metallic", 0f);
                    cube.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.115f);
                    cube.name = i.ToString() + j.ToString() + k.ToString();
                    cube.AddComponent<singleCubeScript>();
                    /*
                    cube.GetComponent<Renderer>().material.EnableKeyword("_METALLICGLOSSMAP");
                    cube.GetComponent<Renderer>().material.SetFloat("_METALLICGLOSSMAP", 0.5f);
                    */


                }
            }
        }
       
    }

	// Update is called once per frame
	void Update () {

	
	}
}
