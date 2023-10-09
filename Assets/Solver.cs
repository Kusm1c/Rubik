using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver
{
    private Cube cubeMainScript;
    public List<string[]> solutionMoves = new();
    private readonly MakeSkyBox makeSkyBox;
    private readonly ShuffleScript shuffleScript;
    private readonly RotateSideScript rotateSideScript;
    private readonly SettersScript settersScript;
    private readonly GettersScript gettersScript;

    public Solver(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public IEnumerator SolveCubeCoroutine(Stack<string[]> movements)
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
                    cubeMainScript.RotateSide1.RotateOrangeSide(movement[1] == "Clockwise");
                    break;
                case "Green":
                    cubeMainScript.RotateSide1.RotateGreenSide(movement[1] == "Clockwise");
                    break;
                case "Red":
                    cubeMainScript.RotateSide1.RotateRedSide(movement[1] == "Clockwise");
                    break;
                case "Blue":
                    cubeMainScript.RotateSide1.RotateBlueSide(movement[1] == "Clockwise");
                    break;
                case "White":
                    cubeMainScript.RotateSide1.RotateWhiteSide(movement[1] == "Clockwise");
                    break;
                case "Yellow":
                    cubeMainScript.RotateSide1.RotateYellowSide(movement[1] == "Clockwise");
                    break;
            }

            yield return new WaitForSeconds(cubeMainScript.rotationAnimationTime + cubeMainScript.rotationAnimationTime / 2);
            cubeMainScript.RotateSide1.SnapAllCubes();
        }

        solutionMoves.Clear();
    }

    public void PlayerSideRotation()
    {
        if (Input.GetKeyDown(cubeMainScript.rotateClockwise))
        {
            if (!cubeMainScript.isChronometerRunning)
            {
                cubeMainScript.StartChronometer();
                cubeMainScript.isChronometerRunning = true;
            }
            cubeMainScript.RotateSide1.RotateCurrentSideClockwise();
        }
        else if (Input.GetKeyDown(cubeMainScript.rotateCounterClockwise))
        {
            if (!cubeMainScript.isChronometerRunning)
            {
                cubeMainScript.StartChronometer();
                cubeMainScript.isChronometerRunning = true;
            }
            cubeMainScript.RotateSide1.RotateCurrentSideCounterClockwise();
        }
    }

    public void SolveCube()
    {
        cubeMainScript.gaveUp = true;
        Stack<string[]> movements = new();
        movements = OptimizeSolution();
        cubeMainScript.StartCoroutine(SolveCubeCoroutine(movements));
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

            if (i < solutionMoves.Count - 3 &&
                solutionMoves[i][0] == solutionMoves[i + 1][0] &&
                solutionMoves[i + 1][0] == solutionMoves[i + 2][0] &&
                solutionMoves[i + 2][0] == solutionMoves[i + 3][0])
            {
                i += 3;
                continue;
            }

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