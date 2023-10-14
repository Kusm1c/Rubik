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
            //up the emission intensity to 5
            cube1.redMaterial.EnableKeyword("_EMISSION");
            cube1.redMaterial.SetColor("_EmissionColor",
                Color.red * Mathf.Lerp(0, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.redMaterial.DisableKeyword("_EMISSION");
            cube1.redMaterial.SetColor("_EmissionColor",
                Color.red * Mathf.Lerp(5, 0, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.OrangeSideIsSolved())
        {
            //up the emission intensity to 5
            cube1.orangeMaterial.EnableKeyword("_EMISSION");
            cube1.orangeMaterial.SetColor("_EmissionColor",
                new Color(1, 0.5f, 0) * Mathf.Lerp(0, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.orangeMaterial.DisableKeyword("_EMISSION");
            cube1.orangeMaterial.SetColor("_EmissionColor",
                new Color(1, 0.5f, 0) * Mathf.Lerp(5, 0, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.GreenSideIsSolved())
        {
            //up the emission intensity to 5
            cube1.greenMaterial.EnableKeyword("_EMISSION");
            cube1.greenMaterial.SetColor("_EmissionColor",
                Color.green * Mathf.Lerp(0, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.greenMaterial.DisableKeyword("_EMISSION");
            cube1.greenMaterial.SetColor("_EmissionColor",
                Color.green * Mathf.Lerp(5, 0, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.BlueSideIsSolved())
        {
            //up the emission intensity to 5
            cube1.blueMaterial.EnableKeyword("_EMISSION");
            cube1.blueMaterial.SetColor("_EmissionColor",
                Color.blue * Mathf.Lerp(0, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.blueMaterial.DisableKeyword("_EMISSION");
            cube1.blueMaterial.SetColor("_EmissionColor",
                Color.blue * Mathf.Lerp(5, 0, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.WhiteSideIsSolved())
        {
            //up the emission intensity to 5
            cube1.whiteMaterial.EnableKeyword("_EMISSION");
            cube1.whiteMaterial.SetColor("_EmissionColor",
                Color.white * Mathf.Lerp(0, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.whiteMaterial.DisableKeyword("_EMISSION");
            cube1.whiteMaterial.SetColor("_EmissionColor",
                Color.white * Mathf.Lerp(5, 0, Time.time * cube1.colorLerpInSecond));
        }

        if (cube1.cubeState.YellowSideIsSolved())
        {
            //up the emission intensity to 5
            cube1.yellowMaterial.EnableKeyword("_EMISSION");
            cube1.yellowMaterial.SetColor("_EmissionColor",
                Color.yellow * Mathf.Lerp(0, 5, Time.time * cube1.colorLerpInSecond));
        }
        else
        {
            cube1.yellowMaterial.DisableKeyword("_EMISSION");
            cube1.yellowMaterial.SetColor("_EmissionColor",
                Color.yellow * Mathf.Lerp(5, 0, Time.time * cube1.colorLerpInSecond));
        }
    }
}