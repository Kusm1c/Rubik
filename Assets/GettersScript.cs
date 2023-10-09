using UnityEngine;

public class GettersScript
{
    private Cube cubeMainScript;

    public GettersScript(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public string GetCameraCurrentSide(Vector3 cameraPosition)
    {
        if (BoolScripts.IsCameraAtSide(cameraPosition, cubeMainScript.cameraBlueSidePosition))
        {
            return "Blue";
        }

        if (   BoolScripts.IsCameraAtSide(cameraPosition, cubeMainScript.cameraGreenSidePosition))
        {
            return "Green";
        }

        if (   BoolScripts.IsCameraAtSide(cameraPosition, cubeMainScript.cameraOrangeSidePosition))
        {
            return "Orange";
        }

        if (   BoolScripts.IsCameraAtSide(cameraPosition, cubeMainScript.cameraRedSidePosition))
        {
            return "Red";
        }

        if (   BoolScripts.IsCameraAtSide(cameraPosition, cubeMainScript.cameraWhiteSidePosition))
        {
            return "White";
        }

        if (   BoolScripts.IsCameraAtSide(cameraPosition, cubeMainScript.cameraYellowSidePosition))
        {
            return "Yellow";
        }

        return null;
    }
}