using UnityEngine;
using System.Collections;

public class SimulatedCube {

    public int[] pos;

    public int value;

    public void setValue(int v)
    {
        value = v;
    }

    public void setPosition(int x, int y, int z)
    {
        pos[0] = x;
        pos[1] = y;
        pos[2] = z;
        //Debug.Log(pos[0].ToString() + pos[1].ToString() + pos[2].ToString());
    }

   


    
	// Use this for initialization
	public SimulatedCube () {

        pos = new int[3];
        value = 0;

       

	}
	
}
