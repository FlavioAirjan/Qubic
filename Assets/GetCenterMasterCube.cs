using UnityEngine;
using System.Collections;

public class GetCenterMasterCube : MonoBehaviour {

    private GameObject masterCube;

    public bool showCenter;

	// Use this for initialization
	void Start () {

        
        masterCube = GameObject.Find("MasterCube");
        float cubeCenter = (float)masterCube.GetComponent<CubeScript>().cubeSize / 2 - masterCube.GetComponent<CubeScript>().spaceBetweenCubes;
        gameObject.transform.position = new Vector3(cubeCenter,cubeCenter,cubeCenter);
        masterCube.transform.parent = gameObject.transform;



    }
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Renderer>().enabled = showCenter ? true : false;
            }
}
