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

    public Solver Solver => solver;
    string cameraCurrentSide;

    public Vector3 cameraBlueSidePosition = new(1f, 1f, -8);
    public Vector3 cameraGreenSidePosition = new(1f, 1f, 10);
    public Vector3 cameraOrangeSidePosition = new(-8, 1f, 1f);
    public Vector3 cameraRedSidePosition = new(10, 1f, 1f);
    public Vector3 cameraWhiteSidePosition = new(1f, -8, 1f);
    public Vector3 cameraYellowSidePosition = new(1f, 10, 1f);

    private int optimizationCount = 0;
    public IEnumerable<Vector3> cameraSidesPositions;
    private Cube cube1;
    [SerializeField] public Material sphereMaterial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        cubeSpawner.CreateCube();
        MakeSkyBox.InvertedNormalSphere();
        SettersScript.SetCameraSidePositions();
        ShuffleScript.Shuffle(numberOfShuffles);
        ResetChronometer();
    }
    
    void Update()
    {
        Solver.PlayerSideRotation();
        UpdateChronometer();
        
        StartNewGame();
        
        if (IsSolved())
        {
            StopChronometer();
            if (!gaveUp)
            {
                UpdateBestTime();
            }
        }
    }

    private void StartNewGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeSpawner.DestroyCube();
            cubeSpawner.CreateCube();
            SettersScript.SetCameraSidePositions();
            ShuffleScript.Shuffle(numberOfShuffles);
            ResetChronometer();
            gaveUp = false;
        }
    }

    #region Solver
    public void SolveButton()
    {
        Solver.SolveCube();
    }
    #endregion

    private readonly Solver solver;
    private readonly MakeSkyBox makeSkyBox;
    private readonly ShuffleScript shuffleScript;
    private readonly RotateSideScript rotateSideScript;
    private readonly SettersScript settersScript;
    private readonly GettersScript gettersScript;
    private readonly CubeSpawner cubeSpawner;

    public Cube()
    {
        makeSkyBox = new MakeSkyBox(this);
        shuffleScript = new ShuffleScript(this);
        rotateSideScript = new RotateSideScript(this);
        settersScript = new SettersScript(this);
        gettersScript = new GettersScript(this);
        solver = new Solver(this);
        cubeSpawner = new CubeSpawner(this);
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

    
    
}