using UnityEngine;
using System.Collections;

public class QubicInfo : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public int cubeSize;
    public int depth;

    

}
