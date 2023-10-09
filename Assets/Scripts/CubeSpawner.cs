using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner
{
    private Cube cubeMainScript;

    public CubeSpawner(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public void CreateCube()
    {
        for (int x = 0; x < cubeMainScript.sideLength; x++)
        {
            for (int y = 0; y < cubeMainScript.sideLength; y++)
            {
                for (int z = 0; z < cubeMainScript.sideLength; z++)
                {
                    GameObject cube = (GameObject)Object.Instantiate((Object)cubeMainScript.rubickCubeSingleCube, new Vector3(x, y, z), Quaternion.identity);
                    cube.transform.parent = cubeMainScript.transform;
                    cube.GetComponent<CubeInfo>().colors = new List<string>();

                    cube.GetComponent<CubeInfo>().colors.Add(x switch
                    {
                        0 => "Blue",
                        2 => "Red",
                        _ => null
                    });

                    cube.GetComponent<CubeInfo>().colors.Add(y switch
                    {
                        0 => "White",
                        2 => "Yellow",
                        _ => null
                    });

                    cube.GetComponent<CubeInfo>().colors.Add(z switch
                    {
                        0 => "Orange",
                        2 => "Green",
                        _ => null
                    });

                    cube.GetComponent<CubeInfo>().position = new[] { x, y, z };

                    cubeMainScript.SettersScript.SetMiddleOfSides(x, y, z);
                }
            }
        }

        cubeMainScript.SettersScript.SetCubeInsideLists();
    }

    public void DestroyCube()
    {
        foreach (Transform cube in cubeMainScript.transform)
        {
            Object.Destroy(cube.gameObject);
        }
    }
}