using UnityEngine;

public class EmissiveScript
{
    private Cube cube1;

    public EmissiveScript(Cube cube1)
    {
        this.cube1 = cube1;
    }

    public void Emissive()
    {
        if (cube1.cubeState.RedSideIsSolved())
        {
            cube1.redMaterial.SetColor("_EmissionColor",
                Color.red * Mathf.Lerp(1, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.redMaterial.SetColor("_EmissionColor",
                Color.red * Mathf.Lerp(5, 1, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.OrangeSideIsSolved())
        {
            cube1.orangeMaterial.SetColor("_EmissionColor",
                new Color(1, 0.5f, 0) * Mathf.Lerp(1, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.orangeMaterial.SetColor("_EmissionColor",
                new Color(1, 0.5f, 0) * Mathf.Lerp(5, 1, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.GreenSideIsSolved())
        {
            cube1.greenMaterial.SetColor("_EmissionColor",
                Color.green * Mathf.Lerp(1, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.greenMaterial.SetColor("_EmissionColor",
                Color.green * Mathf.Lerp(5, 1, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.BlueSideIsSolved())
        {
            cube1.blueMaterial.SetColor("_EmissionColor",
                Color.blue * Mathf.Lerp(1, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.blueMaterial.SetColor("_EmissionColor",
                Color.blue * Mathf.Lerp(5, 1, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.WhiteSideIsSolved())
        {
            cube1.whiteMaterial.SetColor("_EmissionColor",
                Color.white * Mathf.Lerp(1, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.whiteMaterial.SetColor("_EmissionColor",
                Color.white * Mathf.Lerp(5, 1, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.YellowSideIsSolved())
        {
            cube1.yellowMaterial.SetColor("_EmissionColor",
                Color.yellow * Mathf.Lerp(1, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.yellowMaterial.SetColor("_EmissionColor",
                Color.yellow * Mathf.Lerp(5, 1, Time.time * cube1.colorLerpInSecond));
        }
    }
}