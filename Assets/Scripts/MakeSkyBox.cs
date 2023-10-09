using UnityEngine;

public class MakeSkyBox
{
    private Cube cubeMainScript;

    public MakeSkyBox(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }

    public void InvertedNormalSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(1.5f, 1.5f, 1.5f);
        sphere.transform.localScale = new Vector3(120, 120, 120);
        sphere.transform.rotation = Quaternion.Euler(0, 0, 0);
        sphere.GetComponent<MeshRenderer>().material = cubeMainScript.sphereMaterial;
        Mesh mesh = sphere.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }

        mesh.normals = normals;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] triangles = mesh.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3)
            {
                (triangles[j], triangles[j + 1]) = (triangles[j + 1], triangles[j]);
            }

            mesh.SetTriangles(triangles, i);
        }
    }
}