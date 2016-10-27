using UnityEngine;
using System.Collections;

public class QubicInfo : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public int cubeSize;
    public int depth;

    

}
