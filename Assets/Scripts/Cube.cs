using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public GameObject rubickCubeSingleCube;
    public int sideLength = 3;

    public float rotationAnimationTime = 0.5f;

    [SerializeField] public KeyCode rotateClockwise = KeyCode.E;
    [SerializeField] public KeyCode rotateCounterClockwise = KeyCode.Q;

    public Vector3 orangeSidePoint;
    public Vector3 greenSidePoint;
    public Vector3 redSidePoint;
    public Vector3 blueSidePoint;
    public Vector3 whiteSidePoint;
    public Vector3 yellowSidePoint;

    public CubeState cubeState = new();
    public CubeState solvedCubeState = new();

    [SerializeField] public Vector3 orangeSideRotationAxis;
    [SerializeField] public Vector3 greenSideRotationAxis;
    [SerializeField] public Vector3 redSideRotationAxis;
    [SerializeField] public Vector3 blueSideRotationAxis;
    [SerializeField] public Vector3 whiteSideRotationAxis;
    [SerializeField] public Vector3 yellowSideRotationAxis;

    public bool canRotate = true;

    public int numberOfShuffles = 10;

    public static Cube instance;

    public MakeSkyBox MakeSkyBox => makeSkyBox;

    public ShuffleScript ShuffleScript => shuffleScript;

    public RotateSideScript RotateSide1 => rotateSideScript;

    public SettersScript SettersScript => settersScript;

    public GettersScript GettersScript => gettersScript;
    
    public KeyboardLayout KeyboardLayout => keyboardLayout;

    public Solver Solver => solver;
    string cameraCurrentSide;

    public Vector3 cameraBlueSidePosition = new(1f, 1f, -8);
    public Vector3 cameraGreenSidePosition = new(1f, 1f, 10);
    public Vector3 cameraOrangeSidePosition = new(-8, 1f, 1f);
    public Vector3 cameraRedSidePosition = new(10, 1f, 1f);
    public Vector3 cameraWhiteSidePosition = new(1f, -8, 1f);
    public Vector3 cameraYellowSidePosition = new(1f, 10, 1f);
    public string[] sides;

    private int optimizationCount = 0;
    public IEnumerable<Vector3> cameraSidesPositions;
    private Cube cube1;
    [SerializeField] public Material sphereMaterial;
    
    [SerializeField] public Material redMaterial;
    [SerializeField] public Material orangeMaterial;
    [SerializeField] public Material greenMaterial;
    [SerializeField] public Material blueMaterial;
    [SerializeField] public Material whiteMaterial;
    [SerializeField] public Material yellowMaterial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("BestTime"))
        {
            PlayerPrefs.SetFloat(" BestTime", 0);
        }

        cubeSpawner.CreateCube();
        MakeSkyBox.InvertedNormalSphere();
        SettersScript.SetCameraSidePositions();
        ResetChronometer();
        
        for (int i = 0; i < 10; i++)
        {
            CreateDecoCube();
        }
    }
    
    public bool hasBeenShuffled = false;
    public bool isShuffling = false;
    public bool gameStarted = false;
    public int numberOfMovesInSolution = 0;
    
    private float colorLerpInSecond = 0.3f;
    void Update()
    {
        //if redside is Solved then make material red glow and if not then make it not glow using the hdrp
        if (cubeState.RedSideIsSolved())
        {
            //up the emission intensity to 5
            redMaterial.EnableKeyword("_EMISSION");
            redMaterial.SetColor("_EmissionColor", Color.red * Mathf.Lerp(0, 5, Time.time * colorLerpInSecond));
        }
        else
        {
            redMaterial.DisableKeyword("_EMISSION");
            redMaterial.SetColor("_EmissionColor", Color.red * Mathf.Lerp(5, 0, Time.time * colorLerpInSecond));
        }
        
        if (cubeState.OrangeSideIsSolved())
        {
            //up the emission intensity to 5
            orangeMaterial.EnableKeyword("_EMISSION");
            orangeMaterial.SetColor("_EmissionColor", new Color(1, 0.5f, 0) * Mathf.Lerp(0, 5, Time.time * colorLerpInSecond));
        }
        else
        {
            orangeMaterial.DisableKeyword("_EMISSION");
            orangeMaterial.SetColor("_EmissionColor", new Color(1, 0.5f, 0) * Mathf.Lerp(5, 0, Time.time * colorLerpInSecond));
        }
        
        if (cubeState.GreenSideIsSolved())
        {
            //up the emission intensity to 5
            greenMaterial.EnableKeyword("_EMISSION");
            greenMaterial.SetColor("_EmissionColor", Color.green * Mathf.Lerp(0, 5, Time.time * colorLerpInSecond));
        }
        else
        {
            greenMaterial.DisableKeyword("_EMISSION");
            greenMaterial.SetColor("_EmissionColor", Color.green * Mathf.Lerp(5, 0, Time.time * colorLerpInSecond));
        }
        
        if (cubeState.BlueSideIsSolved())
        {
            //up the emission intensity to 5
            blueMaterial.EnableKeyword("_EMISSION");
            blueMaterial.SetColor("_EmissionColor", Color.blue * Mathf.Lerp(0, 5, Time.time * colorLerpInSecond));
        }
        else
        {
            blueMaterial.DisableKeyword("_EMISSION");
            blueMaterial.SetColor("_EmissionColor", Color.blue * Mathf.Lerp(5, 0, Time.time * colorLerpInSecond));
        }
        
        if (cubeState.WhiteSideIsSolved())
        {
            //up the emission intensity to 5
            whiteMaterial.EnableKeyword("_EMISSION");
            whiteMaterial.SetColor("_EmissionColor", Color.white * Mathf.Lerp(0, 5, Time.time * colorLerpInSecond));
        }
        else
        {
            whiteMaterial.DisableKeyword("_EMISSION");
            whiteMaterial.SetColor("_EmissionColor", Color.white * Mathf.Lerp(5, 0, Time.time * colorLerpInSecond));
        }
        
        if (cubeState.YellowSideIsSolved())
        {
            //up the emission intensity to 5
            yellowMaterial.EnableKeyword("_EMISSION");
            yellowMaterial.SetColor("_EmissionColor", Color.yellow * Mathf.Lerp(0, 5, Time.time * colorLerpInSecond));
        }
        else
        {
            yellowMaterial.DisableKeyword("_EMISSION");
            yellowMaterial.SetColor("_EmissionColor", Color.yellow * Mathf.Lerp(5, 0, Time.time * colorLerpInSecond));
        }
        
        
        numberOfMovesInSolution = Solver.solutionMoves.Count;
        if (isShuffling) return;
        if (!hasBeenShuffled)
        {
            StartNewGame();
            return;
        }
        Solver.PlayerSideRotation();
        UpdateChronometer();
        if (Input.GetKeyDown(KeyCode.G))
        {
            Solver.SolveCube();
        }
        
        if (!gameStarted) return;
        if (IsSolved())
        {
            StopChronometer();
            if (!gaveUp)
            {
                UpdateBestTime();
            }
            hasBeenShuffled = false;
            gameStarted = false;
            Solver.solutionMoves.Clear();
        }
    }

    private void StartNewGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsSolved())
            {
                cubeSpawner.DestroyCube();
                cubeSpawner.CreateCube();
            }
            SettersScript.SetCameraSidePositions();
            ShuffleScript.Shuffle(numberOfShuffles);
            ResetChronometer();
            gaveUp = false;
        }
    }

    private readonly Solver solver;
    private readonly MakeSkyBox makeSkyBox;
    private readonly ShuffleScript shuffleScript;
    private readonly RotateSideScript rotateSideScript;
    private readonly SettersScript settersScript;
    private readonly GettersScript gettersScript;
    private readonly CubeSpawner cubeSpawner;
    private readonly KeyboardLayout keyboardLayout;

    public Cube()
    {
        makeSkyBox = new MakeSkyBox(this);
        shuffleScript = new ShuffleScript(this);
        rotateSideScript = new RotateSideScript(this);
        settersScript = new SettersScript(this);
        gettersScript = new GettersScript(this);
        solver = new Solver(this);
        cubeSpawner = new CubeSpawner(this);
        keyboardLayout = new KeyboardLayout(this);
    }

    #region Chronometer
    
    [SerializeField] private TMP_Text chronometerText;
    [SerializeField] private TMP_Text bestTimeText;
    
    private float chronometer;
    private float bestTime;
    public bool isChronometerRunning;
    
    public void StartChronometer()
    {
        isChronometerRunning = true;
    }
    
    public void StopChronometer()
    {
        isChronometerRunning = false;
    }
    
    public void ResetChronometer()
    {
        chronometer = 0;
        chronometerText.text = "00:00:00";
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
            TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);
            bestTimeText.text = timeSpan.ToString("mm':'ss':'ff");
        }
        else
        {
            bestTimeText.text = "00:00:00";
        }
    }
    
    private void UpdateChronometer()
    {
        if (isChronometerRunning)
        {
            chronometer += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(chronometer);
            chronometerText.text = timeSpan.ToString("mm':'ss':'ff");
        }
    }
    
    public void UpdateBestTime()
    {
        if (chronometer < bestTime)
        {
            bestTime = chronometer;
            TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);
            bestTimeText.text = timeSpan.ToString("mm':'ss':'ff");
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
    }

    #endregion

    #region isSolved
    
    public bool gaveUp = false;
    public bool IsSolved()
    {
        return cubeState.IsSolved();
    }

    #endregion

    #region DecoCubes
    public void CreateDecoCube()
    {
        GameObject decoCubeGameObject = new GameObject("DecoCube");
        DecoCube decoCube = decoCubeGameObject.AddComponent<DecoCube>();

        // Instantiate and arrange cubes in a cube formation
        for (int x = 0; x < sideLength; x++)
        {
            for (int y = 0; y < sideLength; y++)
            {
                for (int z = 0; z < sideLength; z++)
                {
                    GameObject cube = Instantiate(rubickCubeSingleCube, new Vector3(x, y, z), Quaternion.identity);
                    cube.transform.parent = decoCubeGameObject.transform;
                }
            }
        }

        // Set random position within a specific range
        Vector3 randomPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        while (randomPosition.magnitude < 10)
        {
            randomPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        }
        decoCubeGameObject.transform.position = randomPosition;

        // Set random rotation
        decoCubeGameObject.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        // Set DecoCube component properties
        decoCube.cube = decoCubeGameObject;
        decoCube.rotationSpeed = Random.Range(1f, 10f);
        decoCube.movementSpeed = Random.Range(1f, 10f);
        decoCube.movementDirection = Random.onUnitSphere; // Random unit vector for movement
        decoCube.rotationDirection = Random.onUnitSphere; // Random unit vector for rotation
    }

    #endregion

    public Vector3 GetRotationAxis(string side)
    {
        return side switch
        {
            "Orange" => orangeSideRotationAxis,
            "Green" => greenSideRotationAxis,
            "Red" => redSideRotationAxis,
            "Blue" => blueSideRotationAxis,
            "White" => whiteSideRotationAxis,
            "Yellow" => yellowSideRotationAxis,
            _ => Vector3.zero
        };
    }

    public Vector3 GetRotationPoint(Vector3 rotationAxis)
    {
return rotationAxis switch
        {
            Vector3 v when v == orangeSideRotationAxis => orangeSidePoint,
            Vector3 v when v == greenSideRotationAxis => greenSidePoint,
            Vector3 v when v == redSideRotationAxis => redSidePoint,
            Vector3 v when v == blueSideRotationAxis => blueSidePoint,
            Vector3 v when v == whiteSideRotationAxis => whiteSidePoint,
            Vector3 v when v == yellowSideRotationAxis => yellowSidePoint,
            _ => Vector3.zero
        };
    }
}

public class DecoCube : MonoBehaviour
{
    public GameObject cube;
    public float rotationSpeed;
    public float movementSpeed;
    public Vector3 movementDirection;
    public Vector3 rotationDirection;

    private void Update()
    {
        cube.transform.RotateAround(cube.transform.position, movementDirection, movementSpeed * Time.deltaTime);
        cube.transform.Rotate(rotationDirection * (rotationSpeed * Time.deltaTime));
    }
}