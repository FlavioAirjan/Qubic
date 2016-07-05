using UnityEngine;
using System.Collections;

public class SimulatedMoviments{


    //Vetor de cubos do movimento.
    public SimulatedCube[] cubesOnThisMovement;

    //Conta os cubos livres no caminho.
    public int countFreeCubes;

    //Cada movimento tem um valor.
    public int value;

  
    public SimulatedMoviments (int sizeCube) {

        value = 0;
        countFreeCubes = sizeCube;
        cubesOnThisMovement = new SimulatedCube[sizeCube];


    }

    public void updateValuePath()
    {
        int tempFreeCubes = 0;
        int tempValue = 0;
        foreach (SimulatedCube cube in cubesOnThisMovement)
        {
            tempValue += cube.value;
            if (cube.value == 0)
            {
                tempFreeCubes++;
            }
        }
        value = tempValue;
        countFreeCubes = tempFreeCubes;
    }


}
