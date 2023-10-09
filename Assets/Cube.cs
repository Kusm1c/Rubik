using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public GameObject rubickCubeSingleCube;
    int sideLength = 3;

    public float rotationAnimationTime = 0.5f;
    
    [SerializeField] KeyCode rotateClockwise = KeyCode.E;
    [SerializeField] KeyCode rotateCounterClockwise = KeyCode.Q;
    
    Vector3 orangeSidePoint;
    Vector3 greenSidePoint;
    Vector3 redSidePoint;
    Vector3 blueSidePoint;
    Vector3 whiteSidePoint;
    Vector3 yellowSidePoint;

    public CubeState cubeState = new();
    public CubeState solvedCubeState = new();
    
    Vector3 orangeSideRotationAxis;
    Vector3 greenSideRotationAxis;
    Vector3 redSideRotationAxis;
    Vector3 blueSideRotationAxis;
    Vector3 whiteSideRotationAxis;
    Vector3 yellowSideRotationAxis;
    
    int totalNumberOfCubes;
    bool canRotate = true;
    
    public int numberOfShuffles = 10;

    public List<int[]> movementsSolution = new();
    
    public Canvas canvas;
    
    public static Cube instance;
    
    public List<string[]> solutionMoves = new();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateCube();
        InvertedNormalSphere();
        totalNumberOfCubes = transform.childCount;
        Shuffle(numberOfShuffles);
    }
    [SerializeField] private Material sphereMaterial;
    private void InvertedNormalSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(1.5f, 1.5f, 1.5f);
        sphere.transform.localScale = new Vector3(30, 30, 30);
        sphere.transform.rotation = Quaternion.Euler(0, 0, 0);
        sphere.GetComponent<MeshRenderer>().material = sphereMaterial;
        //invert normals
        Mesh mesh = sphere.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;
        
        //invert triangles
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] triangles = mesh.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3)
            {
                (triangles[j], triangles[j + 1]) = (triangles[j + 1], triangles[j]);
            }
            mesh.SetTriangles(triangles, i);
        }
    }

    private void Shuffle (int shuffleCount)
    {
        StartCoroutine(ShuffleCoroutine(shuffleCount));
    }

    private IEnumerator ShuffleCoroutine(int shuffleCount)
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            int side = Random.Range(0, 6);
            int direction = Random.Range(0, 2);
            switch (side)
            {
                case 0:
                    RotateOrangeSide(direction == 0);

                    break;
                case 1:
                    RotateGreenSide(direction == 0);

                    break;
                case 2:
                    RotateRedSide(direction == 0);

                    break;
                case 3:
                    RotateBlueSide(direction == 0);

                    break;
                case 4:
                    RotateWhiteSide(direction == 0);

                    break;
                case 5:
                    RotateYellowSide(direction == 0);

                    break;
            }
            yield return new WaitForSeconds(rotationAnimationTime + rotationAnimationTime/2);
            SnapAllCubes();
        }
    }
    
    public void RotateOrangeSide(bool clockWise)
    {
        RotateSide(cubeState.OrangeSideCubes, orangeSidePoint, clockWise ? 90 : -90, orangeSideRotationAxis);
        cubeState.RotateOrangeSide(clockWise);
        solutionMoves.Add(new[] {"Orange", clockWise ? "Clockwise" : "CounterClockwise"});
    }
    
    public void RotateGreenSide(bool clockWise)
    {
        RotateSide(cubeState.GreenSideCubes, greenSidePoint, clockWise ? 90 : -90, greenSideRotationAxis);
        cubeState.RotateGreenSide(clockWise);
        solutionMoves.Add(new[] {"Green", clockWise ? "Clockwise" : "CounterClockwise"});
    }
    
    public void RotateRedSide(bool clockWise)
    {
        RotateSide(cubeState.RedSideCubes, redSidePoint, clockWise ? 90 : -90, redSideRotationAxis);
        cubeState.RotateRedSide(clockWise);
        solutionMoves.Add(new[] {"Red", clockWise ? "Clockwise" : "CounterClockwise"});
    }
    
    public void RotateBlueSide(bool clockWise)
    {
        RotateSide(cubeState.BlueSideCubes, blueSidePoint, clockWise ? 90 : -90, blueSideRotationAxis);
        cubeState.RotateBlueSide(clockWise);
        solutionMoves.Add(new[] {"Blue", clockWise ? "Clockwise" : "CounterClockwise"});
    }
    
    public void RotateWhiteSide(bool clockWise)
    {
        RotateSide(cubeState.WhiteSideCubes, whiteSidePoint, clockWise ? 90 : -90, whiteSideRotationAxis);
        cubeState.RotateWhiteSide(clockWise);
        solutionMoves.Add(new[] {"White", clockWise ? "Clockwise" : "CounterClockwise"});
    }
    
    public void RotateYellowSide(bool clockWise)
    {
        RotateSide(cubeState.YellowSideCubes, yellowSidePoint, clockWise ? 90 : -90, yellowSideRotationAxis);
        cubeState.RotateYellowSide(clockWise);
        solutionMoves.Add(new[] {"Yellow", clockWise ? "Clockwise" : "CounterClockwise"});
    }

    public void SnapAllCubes()
    {
        foreach (Transform cube in transform)
        {
            Vector3 cubePosition = cube.position;
            cube.position = new Vector3(Mathf.Round(cubePosition.x), Mathf.Round(cubePosition.y), Mathf.Round(cubePosition.z));
            var rotation = cube.rotation;
            rotation = Quaternion.Euler(Mathf.Round(rotation.eulerAngles.x / 90) * 90, Mathf.Round(rotation.eulerAngles.y / 90) * 90, Mathf.Round(rotation.eulerAngles.z / 90) * 90);
            cube.rotation = rotation;
        }
    }

    public void RotateSide(List<GameObject> sideCubes, Vector3 rotationAxis, float rotationAngle, Vector3 rotationPoint)
    {
        StartCoroutine(RotateSideCoroutine(sideCubes, rotationAxis, rotationAngle, rotationPoint));
    }


    private IEnumerator RotateSideCoroutine(List<GameObject> sideCubes, Vector3 rotationAxis, float rotationAngle, Vector3 rotationPoint)
    {
        //rotate cubes animation
        if (!canRotate)
        {
            yield break;
        }
        canRotate = false;
        float time = 0;
        while (time < rotationAnimationTime)
        {
            foreach (GameObject cube in sideCubes)
            {
                cube.transform.RotateAround(rotationPoint, rotationAxis, rotationAngle * Time.deltaTime / rotationAnimationTime);
            }
            time += Time.deltaTime;
            yield return null;
        }
        //snap cubes to grid
        foreach (GameObject cube in sideCubes)
        {
            Vector3 cubePosition = cube.transform.position;
            cube.transform.position = new Vector3(Mathf.Round(cubePosition.x), Mathf.Round(cubePosition.y), Mathf.Round(cubePosition.z));
            var rotation = cube.transform.rotation;
            rotation = Quaternion.Euler(Mathf.Round(rotation.eulerAngles.x / 90) * 90, Mathf.Round(rotation.eulerAngles.y / 90) * 90, Mathf.Round(rotation.eulerAngles.z / 90) * 90);
            cube.transform.rotation = rotation;
        }
        Debug.Log("RotateSideCoroutine finished");
        UpdateSideCubes();
        canRotate = true;
        yield return null;
    }

    private void UpdateSideCubes()
    {
        cubeState.ClearCubeState();
        SetCubeInsideLists();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSideRotation();
    }

    string cameraCurrentSide;
    private void PlayerSideRotation()
    {
        // Check for user input to rotate the cube sides
        if (Input.GetKeyDown(rotateClockwise))
        {
            RotateCurrentSideClockwise();
        }
        else if (Input.GetKeyDown(rotateCounterClockwise))
        {
            RotateCurrentSideCounterClockwise();
        }
    }

    private void RotateCurrentSideClockwise()
    {
        // Get the current side based on the camera's position
        string currentSide = GetCameraCurrentSide(Camera.main.transform.position);

        switch (currentSide)
        {
            // Rotate the corresponding side clockwise
            case "Blue":
                RotateBlueSide(clockWise: true);
                break;
            case "Green":
                RotateGreenSide(clockWise: true);
                break;
            case "Red":
                RotateRedSide(clockWise: true);
                break;
            case "Orange":
                RotateOrangeSide(clockWise: true);
                break;
            case "White":
                RotateWhiteSide(clockWise: true);
                break;
            case "Yellow":
                RotateYellowSide(clockWise: true);
                break;
        }
    }

    private void RotateCurrentSideCounterClockwise()
    {
        // Get the current side based on the camera's position
        string currentSide = GetCameraCurrentSide(Camera.main.transform.position);

        switch (currentSide)
        {
            // Rotate the corresponding side counterclockwise
            case "Blue":
                RotateBlueSide(clockWise: false);
                break;
            case "Green":
                RotateGreenSide(clockWise: false);
                break;
            case "Red":
                RotateRedSide(clockWise: false);
                break;
            case "Orange":
                RotateOrangeSide(clockWise: false);
                break;
            case "White":
                RotateWhiteSide(clockWise: false);
                break;
            case "Yellow":
                RotateYellowSide(clockWise: false);
                break;
        }
    }

    private void CreateCube()
    {
        for (int x = 0; x < sideLength; x++)
        {
            for (int y = 0; y < sideLength; y++)
            {
                for (int z = 0; z < sideLength; z++)
                {
                    GameObject cube = Instantiate(rubickCubeSingleCube, new Vector3(x, y, z), Quaternion.identity);
                    cube.transform.parent = transform;
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
                    
                    cube.GetComponent<CubeInfo>().position = new[] {x, y, z};
                    
                    SetMiddleOfSides(x, y, z);
                }
            }
        }
        
        SetCubeInsideLists();
    }

    private void SetMiddleOfSides(int x, int y, int z)
    {
        if (x == 0 && y == (sideLength - 1) / 2 && z == (sideLength - 1) / 2)
        {
            orangeSidePoint = Vector3.left;
            orangeSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (sideLength - 1) / 2 && y == (sideLength - 1) / 2 && z == sideLength - 1)
        {
            greenSidePoint = Vector3.forward;
            greenSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == 2 && y == (sideLength - 1) / 2 && z == (sideLength - 1) / 2)
        {
            redSidePoint = Vector3.right;
            redSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (sideLength - 1) / 2 && y == (sideLength - 1) / 2 && z == 0)
        {
            blueSidePoint = Vector3.back;
            blueSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (sideLength - 1) / 2 && y == 0 && z == (sideLength - 1) / 2)
        {
            whiteSidePoint = Vector3.down;
            whiteSideRotationAxis = new Vector3(x, y, z);
        }

        if (x == (sideLength - 1) / 2 && y == sideLength - 1 && z == (sideLength - 1) / 2)
        {
            yellowSidePoint = Vector3.up;
            yellowSideRotationAxis = new Vector3(x, y, z);
        }
    }

    private void SetCubeInsideLists()
    {
        List<GameObject> everySideCubes = new List<GameObject>();
        foreach (Transform cube in transform)
        {
            everySideCubes.Add(cube.gameObject);
        }

        foreach (var cube in everySideCubes)
        {
            Vector3 cubePosition = cube.transform.position;

            if (Math.Abs(cubePosition.x) == 0)
            {
                cubeState.OrangeSideCubes.Add(cube);
                solvedCubeState.OrangeSideCubes.Add(cube);
            }
            else if (Math.Abs(cubePosition.x - 2) < .1f)
            {
                cubeState.RedSideCubes.Add(cube);
                solvedCubeState.RedSideCubes.Add(cube);
            }

            if (Math.Abs(cubePosition.y) == 0)
            {
                cubeState.WhiteSideCubes.Add(cube);
                solvedCubeState.WhiteSideCubes.Add(cube);
            }
            else if (Math.Abs(cubePosition.y - 2) < .1f)
            {
                cubeState.YellowSideCubes.Add(cube);
                solvedCubeState.YellowSideCubes.Add(cube);
            }

            if (Math.Abs(cubePosition.z) == 0)
            {
                cubeState.BlueSideCubes.Add(cube);
                solvedCubeState.BlueSideCubes.Add(cube);
            }
            else if (Math.Abs(cubePosition.z - 2) < .1f)
            {
                cubeState.GreenSideCubes.Add(cube);
                solvedCubeState.GreenSideCubes.Add(cube);
            }
        }
    }
    
    public string GetCameraCurrentSide(Vector3 cameraPosition)
    {
        if (cameraPosition == new Vector3(1.25f, 1.25f, -10))
        {
            return "Blue";
        }
        if (cameraPosition == new Vector3(12.5f, 1.25f, 1.25f))
        {
            return "Red";
        }
        if (cameraPosition == new Vector3(1.25f, 1.25f, 12.5f))
        {
            return "Green";
        }
        if (cameraPosition == new Vector3(-10, 1.25f, 1.25f))
        {
            return "Orange";
        }
        if (cameraPosition == new Vector3(1.25f, -10, 1.25f))
        {
            return "White";
        }
        if (cameraPosition == new Vector3(1.25f, 12.5f, 1.25f))
        {
            return "Yellow";
        }
        return null;
    }
    
    private int optimizationCount = 0;
    public void SolveCube()
    {
        OptimizeSolution();
    }

    private void OptimizeSolution()
    {
        // for (int i = 0; i < solutionMoves.Count - 1; i++)
        // {
        //     if (solutionMoves[i][1] == "Clockwise" && solutionMoves[i + 1][1] == "CounterClockwise")
        //     {
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         i--;
        //         optimizationCount++;
        //         Debug.Log("Optimization count: " + optimizationCount);
        //         
        //     }
        //     else if (solutionMoves[i][1] == "CounterClockwise" && solutionMoves[i + 1][1] == "Clockwise")
        //     {
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         i--;
        //         optimizationCount++;
        //         Debug.Log("Optimization count: " + optimizationCount);
        //     }
        // }
        //
        // for (int i = 0; i < solutionMoves.Count - 2; i++)
        // {
        //     if (solutionMoves[i][1] == solutionMoves[i + 1][1] && solutionMoves[i + 1][1] == solutionMoves[i + 2][1])
        //     {
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.Insert(i, new[] {solutionMoves[i][0], solutionMoves[i][1] == "Clockwise" ? "CounterClockwise" : "Clockwise"});
        //         i--;
        //         optimizationCount++;
        //         Debug.Log("Optimization count: " + optimizationCount);
        //     }
        // }
        //
        // for (int i = 0; i < solutionMoves.Count - 3; i++)
        // {
        //     if (solutionMoves[i][1] == solutionMoves[i + 1][1] && solutionMoves[i + 1][1] == solutionMoves[i + 2][1] && solutionMoves[i + 2][1] == solutionMoves[i + 3][1])
        //     {
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         solutionMoves.RemoveAt(i);
        //         i--;
        //         optimizationCount++;
        //         Debug.Log("Optimization count: " + optimizationCount);
        //     }
        // }
        
        if (optimizationCount > 0)
        {
            optimizationCount = 0;
            OptimizeSolution();
        }
        else
        {
            Debug.Log("Solution optimized the list of moves");
            foreach (var move in solutionMoves)
            {
                Debug.Log(move[0] + " " + move[1]);
            }
            StartCoroutine(SolveCubeCoroutine());
        }
    }

    private IEnumerator SolveCubeCoroutine()
    {
        foreach (var move in solutionMoves)
        {
            switch (move[0])
            {
                case "Orange":
                    RotateOrangeSide(move[1] == "Clockwise");
                    break;
                case "Green":
                    RotateGreenSide(move[1] == "Clockwise");
                    break;
                case "Red":
                    RotateRedSide(move[1] == "Clockwise");
                    break;
                case "Blue":
                    RotateBlueSide(move[1] == "Clockwise");
                    break;
                case "White":
                    RotateWhiteSide(move[1] == "Clockwise");
                    break;
                case "Yellow":
                    RotateYellowSide(move[1] == "Clockwise");
                    break;
            }
            yield return new WaitForSeconds(rotationAnimationTime + rotationAnimationTime/2);
            SnapAllCubes();
        }
    }
}