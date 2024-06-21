using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject exampleChip;

    Player activePlayer = Player.One;

    public void Awake() {
        if (exampleChip == null) {
            Debug.LogError("You must set 'exampleChip to a valid game prefab.'");
        }
    }

    // The state of the board
    Player[,] boardData = new Player[6,7] {
        { Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE },
        { Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE },
        { Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE },
        { Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE },
        { Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE },
        { Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE, Player.NONE }
    };

    public void AddStone(int column) 
    {
        Debug.Log($"Add Stone {activePlayer} to column {column}.");
    }
}
