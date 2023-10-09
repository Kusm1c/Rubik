using System.Collections;
using UnityEngine;

public class ShuffleScript
{
    private Cube cubeMainScript;

    public ShuffleScript(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public void Shuffle(int shuffleCount)
    {
        cubeMainScript.isShuffling = true;
        cubeMainScript.gameStarted = false;
        cubeMainScript.StartCoroutine((IEnumerator)ShuffleCoroutine(shuffleCount));
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
                    cubeMainScript.RotateSide1.RotateOrangeSide(direction == 0);

                    break;
                case 1:
                    cubeMainScript.RotateSide1.RotateGreenSide(direction == 0);

                    break;
                case 2:
                    cubeMainScript.RotateSide1.RotateRedSide(direction == 0);

                    break;
                case 3:
                    cubeMainScript.RotateSide1.RotateBlueSide(direction == 0);

                    break;
                case 4:
                    cubeMainScript.RotateSide1.RotateWhiteSide(direction == 0);

                    break;
                case 5:
                    cubeMainScript.RotateSide1.RotateYellowSide(direction == 0);

                    break;
            }

            yield return new WaitForSeconds(cubeMainScript.rotationAnimationTime + cubeMainScript.rotationAnimationTime / 2);
            cubeMainScript.RotateSide1.SnapAllCubes();
        }
        cubeMainScript.isShuffling = false;
        cubeMainScript.hasBeenShuffled = true;
    }
}