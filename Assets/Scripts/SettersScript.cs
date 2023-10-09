using System;
using System.Collections.Generic;
using UnityEngine;

public class SettersScript
{
    private Cube cubeMainScript;

    public SettersScript(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public void SetMiddleOfSides(int x, int y, int z)
    {
        if (x == 0 && y == (cubeMainScript.sideLength - 1) / 2 && z == (cubeMainScript.sideLength - 1) / 2)
        {
            cubeMainScript.orangeSidePoint = Vector3.left;
            cubeMainScript.orangeSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (cubeMainScript.sideLength - 1) / 2 && y == (cubeMainScript.sideLength - 1) / 2 && z == cubeMainScript.sideLength - 1)
        {
            cubeMainScript.greenSidePoint = Vector3.forward;
            cubeMainScript.greenSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == 2 && y == (cubeMainScript.sideLength - 1) / 2 && z == (cubeMainScript.sideLength - 1) / 2)
        {
            cubeMainScript.redSidePoint = Vector3.right;
            cubeMainScript.redSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (cubeMainScript.sideLength - 1) / 2 && y == (cubeMainScript.sideLength - 1) / 2 && z == 0)
        {
            cubeMainScript.blueSidePoint = Vector3.back;
            cubeMainScript.blueSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (cubeMainScript.sideLength - 1) / 2 && y == 0 && z == (cubeMainScript.sideLength - 1) / 2)
        {
            cubeMainScript.whiteSidePoint = Vector3.down;
            cubeMainScript.whiteSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (cubeMainScript.sideLength - 1) / 2 && y == cubeMainScript.sideLength - 1 && z == (cubeMainScript.sideLength - 1) / 2)
        {
            cubeMainScript.yellowSidePoint = Vector3.up;
            cubeMainScript.yellowSideRotationAxis = new Vector3(x, y, z);
        }
    }

    public void SetCubeInsideLists()
    {
        List<GameObject> everySideCubes = new List<GameObject>();
        foreach (Transform cube in cubeMainScript.transform)
        {
            everySideCubes.Add(cube.gameObject);
        }

        foreach (var cube in everySideCubes)
        {
            Vector3 cubePosition = cube.transform.position;

            if (Math.Abs(cubePosition.x) == 0)
            {
                cubeMainScript.cubeState.OrangeSideCubes.Add(cube);
                cubeMainScript.solvedCubeState.OrangeSideCubes.Add(cube);
            }
            else if (Math.Abs(cubePosition.x - 2) < .1f)
            {
                cubeMainScript.cubeState.RedSideCubes.Add(cube);
                cubeMainScript.solvedCubeState.RedSideCubes.Add(cube);
            }

            if (Math.Abs(cubePosition.y) == 0)
            {
                cubeMainScript.cubeState.WhiteSideCubes.Add(cube);
                cubeMainScript.solvedCubeState.WhiteSideCubes.Add(cube);
            }
            else if (Math.Abs(cubePosition.y - 2) < .1f)
            {
                cubeMainScript.cubeState.YellowSideCubes.Add(cube);
                cubeMainScript.solvedCubeState.YellowSideCubes.Add(cube);
            }

            if (Math.Abs(cubePosition.z) == 0)
            {
                cubeMainScript.cubeState.BlueSideCubes.Add(cube);
                cubeMainScript.solvedCubeState.BlueSideCubes.Add(cube);
            }
            else if (Math.Abs(cubePosition.z - 2) < .1f)
            {
                cubeMainScript.cubeState.GreenSideCubes.Add(cube);
                cubeMainScript.solvedCubeState.GreenSideCubes.Add(cube);
            }
        }
    }

    public void SetCameraSidePositions()
    {
        cubeMainScript.cameraSidesPositions = new List<Vector3>()
        {
            cubeMainScript.cameraBlueSidePosition,
            cubeMainScript.cameraGreenSidePosition,
            cubeMainScript.cameraOrangeSidePosition,
            cubeMainScript.cameraRedSidePosition,
            cubeMainScript.cameraWhiteSidePosition,
            cubeMainScript.cameraYellowSidePosition
        };
    }
}