using UnityEngine;

public class BoolScripts
{
    public static bool IsCameraAtSide(Vector3 cameraPosition, Vector3 vector3)
    {
        return cameraPosition.x >= vector3.x - 1 && cameraPosition.x <= vector3.x + 1 &&
               cameraPosition.y >= vector3.y - 1 && cameraPosition.y <= vector3.y + 1 &&
               cameraPosition.z >= vector3.z - 1 && cameraPosition.z <= vector3.z + 1;
    }
}