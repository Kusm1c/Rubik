using UnityEngine;

public class DecoCube : MonoBehaviour
{
    public GameObject cube;
    public float rotationSpeed;
    public float movementSpeed;
    public Vector3 movementDirection;
    public Vector3 rotationDirection;

    private void Update()
    {
        cube.transform.RotateAround(cube.transform.position, movementDirection, movementSpeed * Time.deltaTime);
        cube.transform.Rotate(rotationDirection * (rotationSpeed * Time.deltaTime));
    }
    
    private Cube cube1;

    public DecoCube(Cube cube1)
    {
        this.cube1 = cube1;
    }

    public void CreateDecoCube()
    {
        GameObject decoCubeGameObject = new GameObject("DecoCube");
        DecoCube decoCube = decoCubeGameObject.AddComponent<DecoCube>();

        // Instantiate and arrange cubes in a cube formation
        for (int x = 0; x < cube1.sideLength; x++)
        {
            for (int y = 0; y < cube1.sideLength; y++)
            {
                for (int z = 0; z < cube1.sideLength; z++)
                {
                    GameObject cube = Object.Instantiate(cube1.rubickCubeSingleCube, new Vector3(x, y, z), Quaternion.identity);
                    cube.transform.parent = decoCubeGameObject.transform;
                }
            }
        }

        // Set random position within a specific range
        Vector3 randomPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        while (randomPosition.magnitude < 10)
        {
            randomPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        }

        decoCubeGameObject.transform.position = randomPosition;

        // Set random rotation
        decoCubeGameObject.transform.rotation =
            Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        // Set DecoCube component properties
        decoCube.cube = decoCubeGameObject;
        decoCube.rotationSpeed = Random.Range(1f, 10f);
        decoCube.movementSpeed = Random.Range(1f, 10f);
        decoCube.movementDirection = Random.onUnitSphere; // Random unit vector for movement
        decoCube.rotationDirection = Random.onUnitSphere; // Random unit vector for rotation
    }
}