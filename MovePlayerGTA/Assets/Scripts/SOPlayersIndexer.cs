using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Scriptable Objects/Players Indexer", fileName ="Players Indexer")]

public class SOPlayersIndexer : ScriptableObject 
{
    [HideInInspector]
    public int currentPlayerIndexer = 0;

    [HideInInspector]
    public int lastPlayerIndexer = 0;
}
