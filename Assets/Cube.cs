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

    [SerializeField] private Vector3 orangeSideRotationAxis;
    [SerializeField] private Vector3 greenSideRotationAxis;
    [SerializeField] private Vector3 redSideRotationAxis;
    [SerializeField] private Vector3 blueSideRotationAxis;
    [SerializeField] private Vector3 whiteSideRotationAxis;
    [SerializeField] private Vector3 yellowSideRotationAxis;

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

    private void Shuffle(int shuffleCount)
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

            yield return new WaitForSeconds(rotationAnimationTime + rotationAnimationTime / 2);
            SnapAllCubes();
        }
    }

    public void RotateOrangeSide(bool clockWise)
    {
        RotateSide(cubeState.OrangeSideCubes, orangeSidePoint, clockWise ? 90 : -90, orangeSideRotationAxis);
        cubeState.RotateOrangeSide(clockWise);
        solutionMoves.Add(new[] { "Orange", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateGreenSide(bool clockWise)
    {
        RotateSide(cubeState.GreenSideCubes, greenSidePoint, clockWise ? 90 : -90, greenSideRotationAxis);
        cubeState.RotateGreenSide(clockWise);
        solutionMoves.Add(new[] { "Green", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateRedSide(bool clockWise)
    {
        RotateSide(cubeState.RedSideCubes, redSidePoint, clockWise ? 90 : -90, redSideRotationAxis);
        cubeState.RotateRedSide(clockWise);
        solutionMoves.Add(new[] { "Red", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateBlueSide(bool clockWise)
    {
        RotateSide(cubeState.BlueSideCubes, blueSidePoint, clockWise ? 90 : -90, blueSideRotationAxis);
        cubeState.RotateBlueSide(clockWise);
        solutionMoves.Add(new[] { "Blue", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateWhiteSide(bool clockWise)
    {
        RotateSide(cubeState.WhiteSideCubes, whiteSidePoint, clockWise ? 90 : -90, whiteSideRotationAxis);
        cubeState.RotateWhiteSide(clockWise);
        solutionMoves.Add(new[] { "White", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateYellowSide(bool clockWise)
    {
        RotateSide(cubeState.YellowSideCubes, yellowSidePoint, clockWise ? 90 : -90, yellowSideRotationAxis);
        cubeState.RotateYellowSide(clockWise);
        solutionMoves.Add(new[] { "Yellow", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void SnapAllCubes()
    {
        foreach (Transform cube in transform)
        {
            Vector3 cubePosition = cube.position;
            cube.position = new Vector3(Mathf.Round(cubePosition.x), Mathf.Round(cubePosition.y),
                Mathf.Round(cubePosition.z));
            var rotation = cube.rotation;
            rotation = Quaternion.Euler(Mathf.Round(rotation.eulerAngles.x / 90) * 90,
                Mathf.Round(rotation.eulerAngles.y / 90) * 90, Mathf.Round(rotation.eulerAngles.z / 90) * 90);
            cube.rotation = rotation;
        }
    }

    public void RotateSide(List<GameObject> sideCubes, Vector3 rotationAxis, float rotationAngle, Vector3 rotationPoint)
    {
        StartCoroutine(RotateSideCoroutine(sideCubes, rotationAxis, rotationAngle, rotationPoint));
    }


    private IEnumerator RotateSideCoroutine(List<GameObject> sideCubes, Vector3 rotationAxis, float rotationAngle,
        Vector3 rotationPoint)
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
                cube.transform.RotateAround(rotationPoint, rotationAxis,
                    rotationAngle * Time.deltaTime / rotationAnimationTime);
            }

            time += Time.deltaTime;
            yield return null;
        }

        //snap cubes to grid
        foreach (GameObject cube in sideCubes)
        {
            Vector3 cubePosition = cube.transform.position;
            cube.transform.position = new Vector3(Mathf.Round(cubePosition.x), Mathf.Round(cubePosition.y),
                Mathf.Round(cubePosition.z));
            var rotation = cube.transform.rotation;
            rotation = Quaternion.Euler(Mathf.Round(rotation.eulerAngles.x / 90) * 90,
                Mathf.Round(rotation.eulerAngles.y / 90) * 90, Mathf.Round(rotation.eulerAngles.z / 90) * 90);
            cube.transform.rotation = rotation;
        }

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

                    cube.GetComponent<CubeInfo>().position = new[] { x, y, z };

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

    public Vector3 GetSideRotationAxis(string side)
    {
        switch (side)
        {
            case "Orange":
                return orangeSideRotationAxis;
            case "Green":
                return greenSideRotationAxis;
            case "Red":
                return redSideRotationAxis;
            case "Blue":
                return blueSideRotationAxis;
            case "White":
                return whiteSideRotationAxis;
            case "Yellow":
                return yellowSideRotationAxis;
        }

        return Vector3.zero;
    }

    private void SetMiddleOfSides()
    {
    }

    public Vector3 cameraBlueSidePosition = new (1f, 1f, -8);
    public Vector3 cameraGreenSidePosition = new (1f, 1f, 10);
    public Vector3 cameraOrangeSidePosition = new (-8, 1f, 1f);
    public Vector3 cameraRedSidePosition = new (10, 1f, 1f);
    public Vector3 cameraWhiteSidePosition = new (1f, -8, 1f);
    public Vector3 cameraYellowSidePosition = new (1f, 10, 1f);
    public string GetCameraCurrentSide(Vector3 cameraPosition)
    {
        if (cameraPosition == cameraBlueSidePosition)
        {
            return "Blue";
        }
        if (cameraPosition == cameraGreenSidePosition)
        {
            return "Green";
        }
        if (cameraPosition == cameraOrangeSidePosition)
        {
            return "Orange";
        }
        if (cameraPosition == cameraRedSidePosition)
        {
            return "Red";
        }
        if (cameraPosition == cameraWhiteSidePosition)
        {
            return "White";
        }
        if (cameraPosition == cameraYellowSidePosition)
        {
            return "Yellow";
        }
        return null;
    }

    private int optimizationCount = 0;

    public void SolveCube()
    {
        Stack<string[]> movements = new();
        movements = OptimizeSolution();
        StartCoroutine(SolveCubeCoroutine(movements));
    }

    private IEnumerator SolveCubeCoroutine(Stack<string[]> movements)
    {
        if (movements == null)
        {
            yield break;
        }

        while (movements.Count > 0)
        {
            string[] movement = movements.Pop();
            switch (movement[0])
            {
                case "Orange":
                    RotateOrangeSide(movement[1] == "Clockwise");
                    break;
                case "Green":
                    RotateGreenSide(movement[1] == "Clockwise");
                    break;
                case "Red":
                    RotateRedSide(movement[1] == "Clockwise");
                    break;
                case "Blue":
                    RotateBlueSide(movement[1] == "Clockwise");
                    break;
                case "White":
                    RotateWhiteSide(movement[1] == "Clockwise");
                    break;
                case "Yellow":
                    RotateYellowSide(movement[1] == "Clockwise");
                    break;
            }

            yield return new WaitForSeconds(rotationAnimationTime + rotationAnimationTime / 2);
            SnapAllCubes();
        }

        solutionMoves.Clear();
    }

    private Stack<string[]> OptimizeSolution()
    {
        List<string[]> movements = new List<string[]>();
        for (int i = 0; i < solutionMoves.Count; i++)
        {
            if (i < solutionMoves.Count - 1 && solutionMoves[i][0] == solutionMoves[i + 1][0] &&
                solutionMoves[i][1] != solutionMoves[i + 1][1])
            {
                i += 2;
                continue;
            }

            //if the same move is repeated 4 times, it is the same as not doing it at all
            if (i < solutionMoves.Count - 3 &&
                solutionMoves[i][0] == solutionMoves[i + 1][0] &&
                solutionMoves[i + 1][0] == solutionMoves[i + 2][0] &&
                solutionMoves[i + 2][0] == solutionMoves[i + 3][0])
            {
                i += 4;
                continue;
            }

            //if the same move is repeated 3 times, reverse the direction of one of them and remove the other 2
            if (i < solutionMoves.Count - 2 &&
                solutionMoves[i][0] == solutionMoves[i + 1][0] &&
                solutionMoves[i + 1][0] == solutionMoves[i + 2][0])
            {
                movements.Add(new[]
                    { solutionMoves[i][0], solutionMoves[i][1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                i += 2;
                continue;
            }

            if (i < solutionMoves.Count)
                movements.Add(solutionMoves[i]);
        }

        return ConvertSolutionToMovements(InvertSolution(movements));
    }

    private List<string[]> InvertSolution(List<string[]> moves)
    {
        List<string[]> invertedSolution = new();
        foreach (var move in moves)
        {
            switch (move[0])
            {
                case "Orange":
                    invertedSolution.Add(new[] { "Orange", move[1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                    break;
                case "Green":
                    invertedSolution.Add(new[] { "Green", move[1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                    break;
                case "Red":
                    invertedSolution.Add(new[] { "Red", move[1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                    break;
                case "Blue":
                    invertedSolution.Add(new[] { "Blue", move[1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                    break;
                case "White":
                    invertedSolution.Add(new[] { "White", move[1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                    break;
                case "Yellow":
                    invertedSolution.Add(new[] { "Yellow", move[1] == "Clockwise" ? "CounterClockwise" : "Clockwise" });
                    break;
            }
        }

        return invertedSolution;
    }

    private Stack<string[]> ConvertSolutionToMovements(List<string[]> moves)
    {
        Stack<string[]> movements = new();
        foreach (var move in moves)
        {
            switch (move[0])
            {
                case "Orange":
                    movements.Push(new[] { "Orange", move[1] });
                    break;
                case "Green":
                    movements.Push(new[] { "Green", move[1] });
                    break;
                case "Red":
                    movements.Push(new[] { "Red", move[1] });
                    break;
                case "Blue":
                    movements.Push(new[] { "Blue", move[1] });
                    break;
                case "White":
                    movements.Push(new[] { "White", move[1] });
                    break;
                case "Yellow":
                    movements.Push(new[] { "Yellow", move[1] });
                    break;
            }
        }

        return movements;
    }
}