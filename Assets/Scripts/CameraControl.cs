using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float cameraRotationSpeed = 1f;
    [SerializeField] private float cameraDistance = 8f;
    private Vector3 lookAtPoint = new(1f, 1f, 1f);
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
        cameraCurrentSide = Cube.instance.GettersScript.GetCameraCurrentSide(transform.position);
    }

    private void CameraRotate()
    {
        if (Input.GetKeyDown(rotateCameraLeft))
        {
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

    private bool canTurn = true;

    private IEnumerator RotateAround(Vector3 lookAtPoint, Vector3 direction, bool clockwise)
    {
        if (!canTurn) yield break;
        canTurn = false;
        for (float i = 0; i < 90f; i += Time.deltaTime * cameraRotationSpeed)
        {
            transform.RotateAround(lookAtPoint, direction,
                clockwise ? -Time.deltaTime * cameraRotationSpeed : Time.deltaTime * cameraRotationSpeed);
            yield return null;
        }

        SnapCameraToSide();
        canTurn = true;
        cameraCurrentSide = Cube.instance.GettersScript.GetCameraCurrentSide(transform.position);
    }

    private void SnapCameraToSide()
    {
        transform.position = FindClosestSide();

        transform.rotation = Quaternion.Euler(
            Mathf.Round(transform.rotation.eulerAngles.x / 90f) * 90f,
            Mathf.Round(transform.rotation.eulerAngles.y / 90f) * 90f,
            Mathf.Round(transform.rotation.eulerAngles.z / 90f) * 90f);
    }

    private Vector3 FindClosestSide()
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestSide = Vector3.zero;
        foreach (var side in Cube.instance.cameraSidesPositions)
        {
            float distance = Vector3.Distance(transform.position, side);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSide = side;
            }
        }

        return closestSide;
    }


    private void Update()
    {
        CameraRotate();
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red, 0.1f);
    }


    private void SetCameraPosition()
    {
        transform.position = new Vector3(1f, 1f, cameraDistance);
        transform.LookAt(lookAtPoint);
    }
}