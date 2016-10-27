using UnityEngine;
using System.Collections;

public class QubicInfos : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public int cubeSize;
    public int depth;

}
