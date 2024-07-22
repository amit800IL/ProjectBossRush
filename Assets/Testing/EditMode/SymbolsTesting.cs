using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SymbolsTesting
{
    private SymbolTable symbolTable;

    [SetUp]
    public void Setup()
    {
        symbolTable = new SymbolTable();
    }

    //Testing that symbol is added to the symbo system at position 1

    [Test]
    public void IsSymbolAdded()
    {
        SymbolTable newSymbolTable = new SymbolTable(1);

        symbolTable.Add(newSymbolTable);

        Assert.AreEqual(1, symbolTable.table[1]);
    }

    //Testing that symbol is removed from the symbol system at position 1 after addition

    [Test]
    public void IsSymbolRemoved()
    {
        SymbolTable newSymbolTable = new SymbolTable(1);

        symbolTable.Add(newSymbolTable);
        symbolTable.Remove(newSymbolTable);

        Assert.AreEqual(0, symbolTable.table[1]);
    }

    //Testing that the symbolTable contains symbol after addition by calling the contains method

    [Test]
    public void ContainsSymbols()
    {
        SymbolTable newSymbolTable = new SymbolTable(1);

        symbolTable.Add(newSymbolTable);

        Assert.IsTrue(symbolTable.Contains(newSymbolTable));
    }

    //Testing the symbol system output by writing its string and comparing it to the real output of the symbol

    [Test]
    public void PrintTableOutput()
    {
        SymbolTable newSymbolTable = new SymbolTable(1);
        symbolTable.Add(newSymbolTable);

        string expectedOutput = "Fighter0\nMage1\nRanger0\nRogue0\nMender0\nDefense0\n";
        Assert.AreEqual(expectedOutput, symbolTable.PrintTable());
    }

    //Testing the symbol system short string output by writing its expected output string and comparing it to the real output

    [Test]
    public void ToShortStringOutput()
    {
        SymbolTable newSymbolTable = new SymbolTable(1);
        symbolTable.Add(newSymbolTable);

        string expectedOutput = "010000";
        Assert.AreEqual(expectedOutput, symbolTable.ToShortString());
    }
}