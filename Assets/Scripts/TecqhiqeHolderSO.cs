using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TechniqeHolder", menuName = "ScriptableObject/TechniqeHolder")]

public class TecqhiqeHolderSO : ScriptableObject
{
    public List<TeqnicqeHolder> teqnicqeHolder = new List<TeqnicqeHolder>();
}

[System.Serializable]
public class TeqnicqeHolder
{
   public Technique technique;
   public TecqhiqeHolderSO teqhiqeHolderSO;
}
