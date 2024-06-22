using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject exampleChip;
    public GameObject winpanel;
    public Text win;
    public GameObject playerSelect1;
    public GameObject playerSelect2;
    public GameObject drawpanel;

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

    public void AddStone(int column) {
    int row = 5;

    if (playercount == 44){
            drawpanel.SetActive(true);
        }

    // Find the first empty row in the given column
    while (boardData[row, column] != Player.NONE) {
        row--;
        //if (row == 0){
            //break;
        //}
    }

    if (row >= 0) {

        PlayerSwitch();
        // Update the board data with the active player's move
        boardData[row, column] = activePlayer;

        // Calculate the destination position for the chip
        Vector3 destination = BoardPositions.GetWorldPosition(row, column);

        // Instantiate the chip above the board (e.g., at row 6)
        GameObject chip = Instantiate(exampleChip, BoardPositions.GetWorldPositionAboveBoard(column), Quaternion.identity);

        // Start the chip drop animation coroutine
        StartCoroutine(ChipDropAnimation(chip.transform, destination));
        
        // Check for win condition
        WinCondition(activePlayer);

        Debug.Log($"Add Stone {activePlayer} to column {column}.");
    }

    else {
        Debug.Log("Column is full");
    }
    
}
    private IEnumerator ChipDropAnimation(Transform chipTransform, Vector3 destination){
    Vector3 startPosition = chipTransform.position;
    Debug.Log($"Animating from {startPosition} to {destination}");

    float elapsed = 0f;
    float duration = 0.5f; // Adjust the duration for a smoother or quicker drop

    while (elapsed < duration){
        float t = elapsed / duration;
        chipTransform.position = Vector3.Lerp(startPosition, destination, t);
        Debug.Log($"Current Position: {chipTransform.position} at time {elapsed}");
        elapsed += Time.deltaTime;
        yield return null;
    }

    chipTransform.position = destination;
    Debug.Log($"Final Position: {chipTransform.position}");
}

    /*public void AddStone(int column) 
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
        Instantiate(exampleChip,BoardPositions.GetWorldPosition(row,column));
        
        WinCondition(activePlayer);
        Debug.Log($"Add Stone {activePlayer} to column {column}.");
    }*/
    /*private IEnumerator ChipAnimation(Vector3 column)
    {
        Vector3 StartPosition = transform.position;
        
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(StartPosition, column, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = column;
    }*/

    public Player PlayerSwitch(){
        if (playercount % 2 == 0){
            activePlayer = Player.One;
            playerSelect1.SetActive(false);
            playerSelect2.SetActive(true);
        }
        else {
            activePlayer = Player.Two;
            playerSelect2.SetActive(false);
            playerSelect1.SetActive(true);
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
