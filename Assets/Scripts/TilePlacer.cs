using UnityEngine;


[ExecuteInEditMode]
public class TilePlacer : MonoBehaviour
{
    public GameObject tilePrefab; // The tile prefab to place
    public int rows = 10; // Number of rows
    public int columns = 10; // Number of columns
    public float tileSizeX = 5.0f; // Size of each tile in X direction
    public float tileSizeZ = 5.0f; // Size of each tile in Z direction

    void Start()
    {
        PlaceTiles();
    }

    void PlaceTiles()
    {
        // Clear previous tiles to prevent duplication
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * tileSizeX, 0, row * tileSizeZ);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
