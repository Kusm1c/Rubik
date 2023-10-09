using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float cameraRotationSpeed = 1f;
    [SerializeField] private float cameraDistance = 8f;
    private Vector3 lookAtPoint = new(1.25f, 1.25f, 1.25f);
    [SerializeField] private KeyCode rotateCameraLeft = KeyCode.A;
    [SerializeField] private KeyCode rotateCameraRight = KeyCode.D;
    [SerializeField] private KeyCode rotateCameraUp = KeyCode.Z;
    [SerializeField] private KeyCode rotateCameraDown = KeyCode.S;
    [SerializeField] private KeyCode rotateCameraClockwise = KeyCode.E;
    [SerializeField] private KeyCode rotateCameraCounterClockwise = KeyCode.Q;
    
    public string cameraCurrentSide;
    
    public static CameraControl instance { get; private set; }
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetCameraPosition();
        cameraCurrentSide = Cube.instance.GetCameraCurrentSide(transform.position);
        Debug.Log(cameraCurrentSide);
    }
    
    //lets make the camera rotate around the cube from a side to another in one press
    private void CameraRotate()
    {
        if (Input.GetKeyDown(rotateCameraLeft))
        {
            // _camera.transform.RotateAround(lookAtPoint, Vector3.up, 90f);
            //same as above but with a coroutine
            StartCoroutine(RotateAround(lookAtPoint, transform.up, false));
        }
        else if (Input.GetKeyDown(rotateCameraRight))
        {
            StartCoroutine(RotateAround(lookAtPoint, transform.up, true));
        }
        else if (Input.GetKeyDown(rotateCameraUp))
        {
            StartCoroutine(RotateAround(lookAtPoint, transform.right, false));
        }
        else if (Input.GetKeyDown(rotateCameraDown))
        {
            StartCoroutine(RotateAround(lookAtPoint, transform.right, true));
        }
        else if (Input.GetKeyDown(rotateCameraClockwise))
        {
            StartCoroutine(RotateAround(lookAtPoint, transform.forward, true));
        }
        else if (Input.GetKeyDown(rotateCameraCounterClockwise))
        {
            StartCoroutine(RotateAround(lookAtPoint, transform.forward, false));
        }
    }

    private IEnumerator RotateAround(Vector3 lookAtPoint, Vector3 direction, bool clockwise)
    {
        
        //rotate the camera around the cube a whole 90° at the speed of cameraRotationSpeed
        for (int i = 0; i < 90; i++)
        {
            transform.RotateAround(lookAtPoint, direction, cameraRotationSpeed * (clockwise ? -1 : 1));
            yield return null;
        }
        SnapCameraToSide();
        cameraCurrentSide = Cube.instance.GetCameraCurrentSide(transform.position);
    }

    private void SnapCameraToSide()
    {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / 1.25f) * 1.25f,
            Mathf.Round(transform.position.y / 1.25f) * 1.25f,
            Mathf.Round(transform.position.z / 1.25f) * 1.25f);
    }


    private void Update()
    {
        CameraRotate();
    }



    private void SetCameraPosition()
    {
        transform.position = new Vector3(1.25f, 1.25f, -cameraDistance);
        transform.LookAt(lookAtPoint);
    }
}

