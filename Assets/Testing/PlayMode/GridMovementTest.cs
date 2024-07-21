using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class GridMovementTest
{
    GameObject grid;
    GridManager gridManager;
    GameObject mage;

    [SetUp]
    public void Setup()
    {
        grid = new GameObject();
       mage = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Heroes/Magician"));
        gridManager = grid.AddComponent<GridManager>();
    }
    [Test]
    public void AllObjectsExists()
    {
        Assert.IsNotNull(grid);
        Assert.IsNotNull(gridManager);
        Assert.IsNotNull(mage);
    }

    [Test]
    public void GridTilesAvailable() 
    {
        Assert.IsNotNull(gridManager.Tiles);
    }
    [Test]
    public void GridTilesIsEqualToGridSize()
    {
          Assert.AreEqual(gridManager.Tiles.Length, gridManager.gridSize.x * gridManager.gridSize.y);
    }
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
        Object.DestroyImmediate(mage);
    }
    



}
