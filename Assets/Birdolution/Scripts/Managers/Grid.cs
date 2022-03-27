using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    [SerializeField] private GameObject emptyCube;
    [SerializeField] private GameObject[] landCubes;

    public enum CubeTypes
    {
        empty = 0,
        land = 1,
        decorative = 2,
    }

    [SerializeField] public int gridXLenght = 7;
    [SerializeField] public int gridZLenght = 7;

    public Vector2 centerCoord;

    public Cube[,] LevelGrid;

    [SerializeField] private Transform gridHolder;

    private void Awake()
    {
        instance = this;

        LevelGrid = new Cube[gridXLenght, gridZLenght];

        for (int x = 0; x < gridXLenght; x++)
        {
            for (int z = 0; z < gridZLenght; z++)
            {
                if (x == centerCoord.x && z == centerCoord.y)
                {
                    CreateCube(x, z, CubeTypes.land, GetRandomLand());
                }
                else
                {
                    CreateCube(x, z, CubeTypes.empty, emptyCube);
                }
            }
        }
    }

    private void CreateCube(int x, int z, Grid.CubeTypes type, GameObject prefab)
    {
        var cubeObj = Instantiate(prefab, new Vector3(x, -0.5f, z), default, gridHolder);
        var cube = cubeObj.GetComponent<Cube>();
        cube.SetValues(x, z, type);
        LevelGrid[x, z] = cube;
    }

    public List<Cube> GetNeighbors(int cellRow, int cellCol)
    {
        var minRow = cellRow == 0 ? 0 : cellRow - 1;
        var maxRow = cellRow == LevelGrid.GetUpperBound(0) ? cellRow : cellRow + 1;
        var minCol = cellCol == 0 ? 0 : cellCol - 1;
        var maxCol = cellCol == LevelGrid.GetUpperBound(1) ? cellCol : cellCol + 1;

        var results = new List<Cube>();

        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxCol; col++)
            {
                if (row == cellRow && col == cellCol) continue;

                if(LevelGrid[row, col].cubeType == CubeTypes.land)
                {
                    if(!(LevelGrid[row, col].x != cellRow && LevelGrid[row, col].z != cellCol))
                        results.Add(LevelGrid[row, col]);
                }
            }
        }

        return results;
    }

    public void ReplaceCube(int x, int z, GameObject tile)
    {
        Destroy(LevelGrid[x, z].gameObject);
        var cubeObj = Instantiate(tile, new Vector3(x, 0, z), default);
        var cube = cubeObj.GetComponent<Cube>();
        cube.SetValues(x, z, CubeTypes.land);
        LevelGrid[x, z] = cube;

        Destroy(Instantiate(ObjectManager.instance.placingParticle, new Vector3(x, 1, z), default), 1f);

        BankManager.instance.RemoveMoney(BankManager.Prices.landPrice);
    }

    public GameObject GetRandomLand()
    {
        return landCubes[Random.Range(0, landCubes.Length)];
    }

    public void TogglePlacingIndicators(bool active)
    {
        for (int x = 0; x < gridXLenght; x++)
        {
            for (int z = 0; z < gridZLenght; z++)
            {
                var landCube = LevelGrid[x, z].GetComponent<LandCube>();
                if (landCube)
                {
                    landCube.ShowPlacingIndication(active);
                }
            }
        }
    }

    public void ToggleTilePlacingIndicators()
    {
        for (int x = 0; x < gridXLenght; x++)
        {
            for (int z = 0; z < gridZLenght; z++)
            {
                var emptyCube = LevelGrid[x, z].GetComponent<EmptyCube>();
                if (emptyCube)
                {
                    emptyCube.TogglePlacingIndication();
                }
            }
        }
    }
}
