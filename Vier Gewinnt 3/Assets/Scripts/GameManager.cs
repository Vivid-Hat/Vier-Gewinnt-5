using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject exampleChip;
    public GameObject winpanel;
    public Text win;

    int playercount = 2;

    public Player activePlayer = Player.One;

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
        int row = 5;
        PlayerSwitch();
        while (boardData[row, column] != Player.NONE) {
            row--;
            if (row == 0){
                break;
            }
        }
        
        boardData[row,column] = activePlayer;
        Instantiate(exampleChip,BoardPositions.GetWorldPosition(row,column),Quaternion.identity);
        
        WinCondition(activePlayer);
        Debug.Log($"Add Stone {activePlayer} to column {column}.");
    }

    public Player PlayerSwitch(){
        if (playercount % 2 == 0){
            activePlayer = Player.One;
        }
        else {
            activePlayer = Player.Two;
        }
        playercount ++;
        return activePlayer;
    }

    bool WinCondition(Player countPlayer){

    //horizontal
    for (int x = 0; x < boardData.GetLength(0) - 3; x++){
        for (int y = 0; y < boardData.GetLength(1); y++){
            if (boardData[x, y] == countPlayer && boardData[x + 1, y] == countPlayer && boardData[x + 2, y] == countPlayer && boardData[x + 3, y] == countPlayer){
                if (countPlayer == Player.One){
                    win.text = "Spieler 1 hat gewonnen!";
                }
                if (countPlayer == Player.Two){
                    win.text = "Spieler 2 hat gewonnen!";
                }
                winpanel.SetActive(true);
                return true;
            }
        }
    }
    //vertical
    for (int x = 0; x < boardData.GetLength(0); x++){
        for (int y = 0; y < boardData.GetLength(1) - 3; y++){
            if (boardData[x, y] == countPlayer && boardData[x, y + 1] == countPlayer && boardData[x, y + 2] == countPlayer && boardData[x, y + 3] == countPlayer){
                if (countPlayer == Player.One){
                    win.text = "Spieler 1 hat gewonnen!";
                }
                if (countPlayer == Player.Two){
                    win.text = "Spieler 2 hat gewonnen!";
                }
                winpanel.SetActive(true);
                return true;
            }
        }
    }
    //diagonal 
    for (int x = 0; x < boardData.GetLength(0) - 3; x++){
        for (int y = 0; y < boardData.GetLength(1) - 3; y++){
            if(boardData[x, y] == countPlayer && boardData[x + 1, y + 1] == countPlayer && boardData[x + 2, y + 2] == countPlayer && boardData[x + 3, y + 3] == countPlayer){
                if (countPlayer == Player.One){
                        win.text = "Spieler 1 hat gewonnen!";
                }
                if (countPlayer == Player.Two){
                        win.text = "Spieler 2 hat gewonnen!";
                }
                winpanel.SetActive(true);
                return true;
            }
        }       
    }
    //diagonal von oben nach unten
    for (int x = 0; x < boardData.GetLength(0) - 3; x++){
        for (int y = 0; y < boardData.GetLength(1) - 3; y++){
            if (boardData [x, y + 3] == countPlayer && boardData[x + 1, y + 2] == countPlayer && boardData[x + 2, y + 1] == countPlayer && boardData[x + 3, y] == countPlayer){
                if (countPlayer == Player.One){
                    win.text = "Spieler 1 hat gewonnen!";
                }
                if (countPlayer == Player.Two){
                    win.text = "Spieler 2 hat gewonnen!";
                }
                winpanel.SetActive(true);
                return true;
            }
        }
    }
    return false;
}
}
