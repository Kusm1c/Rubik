using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public MakeSkyBox MakeSkyBox { get; }

    public ShuffleScript ShuffleScript { get; }

    public RotateSideScript RotateSide1 { get; }

    public SettersScript SettersScript { get; }

    public GettersScript GettersScript { get; }

    public Solver Solver { get; }

    private string cameraCurrentSide;

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
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BestTime")) PlayerPrefs.SetFloat(" BestTime", 0);

        cubeSpawner.CreateCube();
        MakeSkyBox.InvertedNormalSphere();
        SettersScript.SetCameraSidePositions();
        ResetChronometer();

        for (var i = 0; i < 10; i++) decoCubee.CreateDecoCube();
    }

    public bool hasBeenShuffled;
    public bool isShuffling;
    public bool gameStarted;
    public bool isSolving;

    public float colorLerpInSecond = 0.3f;

    private void Update()
    {
        emissiveScript.Emissive();

        if (isShuffling) return;
        if (!hasBeenShuffled)
        {
            StartNewGame();
            return;
        }

        Solver.PlayerSideRotation();
        UpdateChronometer();
        if (Input.GetKeyDown(KeyCode.G) && !isSolving)
        {
            isSolving = true;
            Solver.SolveCube();
        }

        if (!gameStarted) return;
        if (IsSolved())
        {
            StopChronometer();
            if (!gaveUp) UpdateBestTime();

            isSolving = false;
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

    private readonly CubeSpawner cubeSpawner;

    public Cube()
    {
        MakeSkyBox = new MakeSkyBox(this);
        ShuffleScript = new ShuffleScript(this);
        RotateSide1 = new RotateSideScript(this);
        SettersScript = new SettersScript(this);
        GettersScript = new GettersScript(this);
        Solver = new Solver(this);
        cubeSpawner = new CubeSpawner(this);
        new KeyboardLayout(this);
        emissiveScript = new EmissiveScript(this);
        decoCubee = new DecoCube(this);
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
            var timeSpan = TimeSpan.FromSeconds(bestTime);
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
            var timeSpan = TimeSpan.FromSeconds(chronometer);
            chronometerText.text = timeSpan.ToString("mm':'ss':'ff");
        }
    }

    public void UpdateBestTime()
    {
        if (chronometer < bestTime)
        {
            bestTime = chronometer;
            var timeSpan = TimeSpan.FromSeconds(bestTime);
            bestTimeText.text = timeSpan.ToString("mm':'ss':'ff");
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
    }

    #endregion

    #region isSolved

    public bool gaveUp;
    private readonly EmissiveScript emissiveScript;
    private readonly DecoCube decoCubee;

    public bool IsSolved()
    {
        return cubeState.IsSolved();
    }

    #endregion
}