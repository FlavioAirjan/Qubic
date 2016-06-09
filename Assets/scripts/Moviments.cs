using UnityEngine;

public class Moviments : MonoBehaviour {

    public int x;
    public int y;
    public int z;
    public bool validMoviment;

    public Moviments()
    {
        x = 0;
        y = 0;
        z = 0;
    }
    

    public void setMove(int numMove)
    {

        /*
         Convenção:
         ------------
         \/ : 1
         > : 1
         + : 1

        /\ : -1
        < : -1
        * : -1
        -------------
         */
        switch (numMove)
        {
            case 0:
                //+
                x = 0;
                y = 0;
                z = 1;
                break;
            case 1:
                // >
                x = 0;
                y = 1;
                z = 0;
                break;
            case 2:
                // + >
                x = 0;
                y = 1;
                z = 1;
                break;
            case 3:
                // + <
                x = 0;
                y = -1;
                z = 1;
                break;
            case 4:
                // \/
                x = 1;
                y = 0;
                z = 0;
                break;
            case 5:
                // \/ + 
                x = 1;
                y = 0;
                z = 1;
                break;
            case 6:
                // \/ *
                x = 1;
                y = 0;
                z = -1;
                break;
            case 7:
                // \/ >
                x = 1;
                y = 1;
                z = 0;
                break;
            case 8:
                // /\ > 
                x = -1;
                y = 1;
                z = 0;
                break;
            case 9:
                // + \/ >
                x = 1;
                y = 1;
                z = 1;
                break;
            case 10:
                // + \/ <
                x = 1;
                y = -1;
                z = 1;
                break;
            case 11:
                // * \/ >
                x = 1;
                y = 1;
                z = -1;

                break;
            case 12:
                // + /\ >
                x = -1;
                y = 1;
                z = 1;
                break;
            default:
                Debug.Log("There is only 13 moviments [0 to 12].");
                break;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
