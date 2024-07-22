using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridMovementTest
{
    GameObject grid;
    GridManager gridManager;

    [SetUp]
    public void Setup()
    {
        grid = new GameObject();
        gridManager = grid.AddComponent<GridManager>();
    }

    //Tests that the grid exists and the grid manager exists on the scene

    [Test]
    public void AllObjectsExists()
    {
        Assert.IsNotNull(grid);
        Assert.IsNotNull(gridManager);
    }

    //Tests that the tiles of the grid exist on scene

    [Test]
    public void GridTilesAvailable() 
    {
        Assert.IsNotNull(gridManager.Tiles);
    }

    //Checking that the grid size is isantited correctly by the defintion

    [Test]
    public void GridTilesIsEqualToGridSize()
    {
          Assert.AreEqual(gridManager.Tiles.Length, gridManager.gridSize.x * gridManager.gridSize.y);
    }

    //Testing each specific tile to check its existence on scene

    [Test]
    public void GridTilesAreNotNull()
    {
        for (int x = 0; x < gridManager.gridSize.x; x++)
        {
            for (int y = 0; y < gridManager.gridSize.y; y++)
            {
                Assert.IsNotNull(gridManager.Tiles[x, y]);
            }
        }
    }
    

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(grid);
    }
    



}
