using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSideScript
{
    private readonly Cube cubeMainScript;

    public RotateSideScript(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public void RotateOrangeSide(bool clockWise)
    {
        if (!cubeMainScript.canRotate) return;
        cubeMainScript.canRotate = false;
        RotateSide(cubeMainScript.cubeState.OrangeSideCubes, cubeMainScript.orangeSidePoint, clockWise ? 90 : -90, cubeMainScript.orangeSideRotationAxis);
        cubeMainScript.cubeState.RotateOrangeSide(clockWise);
        // cubeMainScript.Solver.solutionMoves.Add(new[] { "Orange", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateGreenSide(bool clockWise)
    {
        if (!cubeMainScript.canRotate) return;
        cubeMainScript.canRotate = false;
        RotateSide(cubeMainScript.cubeState.GreenSideCubes, cubeMainScript.greenSidePoint, clockWise ? 90 : -90, cubeMainScript.greenSideRotationAxis);
        cubeMainScript.cubeState.RotateGreenSide(clockWise);
        // cubeMainScript.Solver.solutionMoves.Add(new[] { "Green", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateRedSide(bool clockWise)
    {
        if (!cubeMainScript.canRotate) return;
        cubeMainScript.canRotate = false;
        RotateSide(cubeMainScript.cubeState.RedSideCubes, cubeMainScript.redSidePoint, clockWise ? 90 : -90, cubeMainScript.redSideRotationAxis);
        cubeMainScript.cubeState.RotateRedSide(clockWise);
        // cubeMainScript.Solver.solutionMoves.Add(new[] { "Red", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateBlueSide(bool clockWise)
    {
        if (!cubeMainScript.canRotate) return;
        cubeMainScript.canRotate = false;
        RotateSide(cubeMainScript.cubeState.BlueSideCubes, cubeMainScript.blueSidePoint, clockWise ? 90 : -90, cubeMainScript.blueSideRotationAxis);
        cubeMainScript.cubeState.RotateBlueSide(clockWise);
        // cubeMainScript.Solver.solutionMoves.Add(new[] { "Blue", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateWhiteSide(bool clockWise)
    {
        if (!cubeMainScript.canRotate) return;
        cubeMainScript.canRotate = false;
        RotateSide(cubeMainScript.cubeState.WhiteSideCubes, cubeMainScript.whiteSidePoint, clockWise ? 90 : -90, cubeMainScript.whiteSideRotationAxis);
        cubeMainScript.cubeState.RotateWhiteSide(clockWise);
        // cubeMainScript.Solver.solutionMoves.Add(new[] { "White", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void RotateYellowSide(bool clockWise)
    {
        if (!cubeMainScript.canRotate) return;
        cubeMainScript.canRotate = false;
        RotateSide(cubeMainScript.cubeState.YellowSideCubes, cubeMainScript.yellowSidePoint, clockWise ? 90 : -90, cubeMainScript.yellowSideRotationAxis);
        cubeMainScript.cubeState.RotateYellowSide(clockWise);
        // cubeMainScript.Solver.solutionMoves.Add(new[] { "Yellow", clockWise ? "Clockwise" : "CounterClockwise" });
    }

    public void SnapAllCubes()
    {
        foreach (Transform cube in cubeMainScript.transform)
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

    private void RotateSide(List<GameObject> sideCubes, Vector3 rotationAxis, float rotationAngle, Vector3 rotationPoint)
    {
        cubeMainScript.StartCoroutine(RotateSideCoroutine(sideCubes, rotationAxis, rotationAngle, rotationPoint));
    }

    private IEnumerator RotateSideCoroutine(List<GameObject> sideCubes, Vector3 rotationAxis, float rotationAngle,
        Vector3 rotationPoint)
    {
        float time = 0;
        while (time < cubeMainScript.rotationAnimationTime)
        {
            foreach (GameObject cube in sideCubes)
            {
                cube.transform.RotateAround(rotationPoint, rotationAxis,
                    rotationAngle * Time.deltaTime / cubeMainScript.rotationAnimationTime);
            }

            time += Time.deltaTime;
            yield return null;
        }
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
        yield return null;
        cubeMainScript.canRotate = true;
    }

    private void UpdateSideCubes()
    {
        cubeMainScript.cubeState.ClearCubeState();
        cubeMainScript.SettersScript.SetCubeInsideLists();
    }

    public void RotateCurrentSideClockwise()
    {
        string currentSide = cubeMainScript.GettersScript.GetCameraCurrentSide(Camera.main.transform.position);
        if (!cubeMainScript.canRotate)
        {
            Debug.Log("Can't rotate");
            return;
        }
        switch (currentSide)
        {
            case "Blue":
                RotateBlueSide(clockWise: true);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Blue", "Clockwise" });
                break;
            case "Green":
                RotateGreenSide(clockWise: true);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Green", "Clockwise" });
                break;
            case "Red":
                RotateRedSide(clockWise: true);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Red", "Clockwise" });
                break;
            case "Orange":
                RotateOrangeSide(clockWise: true);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Orange", "Clockwise" });
                break;
            case "White":
                RotateWhiteSide(clockWise: true);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "White", "Clockwise" });
                break;
            case "Yellow":
                RotateYellowSide(clockWise: true);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Yellow", "Clockwise" });
                break;
        }
    }

    public void RotateCurrentSideCounterClockwise()
    {
        string currentSide = cubeMainScript.GettersScript.GetCameraCurrentSide(Camera.main.transform.position);
        if (!cubeMainScript.canRotate)
        {
            Debug.Log("Can't rotate");
            return;
        }
        switch (currentSide)
        {
            case "Blue":
                RotateBlueSide(clockWise: false);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Blue", "CounterClockwise" });
                break;
            case "Green":
                RotateGreenSide(clockWise: false);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Green", "CounterClockwise" });
                break;
            case "Red":
                RotateRedSide(clockWise: false);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Red", "CounterClockwise" });
                break;
            case "Orange":
                RotateOrangeSide(clockWise: false);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Orange", "CounterClockwise" });
                break;
            case "White":
                RotateWhiteSide(clockWise: false);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "White", "CounterClockwise" });
                break;
            case "Yellow":
                RotateYellowSide(clockWise: false);
                cubeMainScript.Solver.solutionMoves.Add(new[] { "Yellow", "CounterClockwise" });
                break;
        }
    }
}