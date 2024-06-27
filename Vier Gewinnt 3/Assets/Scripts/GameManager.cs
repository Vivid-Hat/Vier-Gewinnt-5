using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject exampleChip;              //Referenziert public Variablen
    public GameObject winpanel;
    public Text win;
    public GameObject playerSelect1;
    public GameObject playerSelect2;
    public GameObject drawpanel;

    int playercount = 2;                        //Startet die playercount Variable bei 2

    public Player activePlayer = Player.One;    //Setzt den erst aktiven Spieler auf Spieler 1

    public void Awake() {                        
        if (exampleChip == null) {
            Debug.LogError("You must set 'exampleChip to a valid game prefab.'"); //Dient nur zum Debug, falls das chip prefab noch nicht zugewiesen wurde

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

    public void AddStone(int column) {       //AddStone Funktion
    int row = 5;                             //Es gibt 6 Zeilen pro Spalte. Jede Spalte startet bei der Zeile 5(unten) --> 0(oben). Variable wird initialisiert

        if (playercount == 44){              //Zuvor definierter playercount ist wichtig für Spielerwechsel, aber kann auch für game state draw detection genutzt werden
                                             //42 Felder die besetzt werden können. counter startet bei 2, also 44 bis alle Felder belegt sind
            drawpanel.SetActive(true);       //Wenn das passiert ist, aktiviere das draw panel 
        }

    while (boardData[row, column] != Player.NONE) {         //Findet die erste Zeile die noch nicht besetzt ist
        row--;                                              //Setzt die Zeile eins hoch wenn besetzt
    }

    if (row >= 0) {                                         //Stellt sicher, dass die Spalte noch nicht voll ist

        PlayerSwitch();                                     //Führt die Funktion PlayerSwitch aus und ändert damit den Spieler
        boardData[row, column] = activePlayer;              //Updated boardData über den Zug vom aktiven Spieler
        Vector3 destination = BoardPositions.GetWorldPosition(row, column);                    //Berechnet die Zielposition vom Chip
        GameObject chip = Instantiate(exampleChip, BoardPositions.GetWorldPositionAboveBoard(column), Quaternion.identity);     //Setzt den Chip über dem Board ein. Zeile 6
        StartCoroutine(ChipDropAnimation(chip.transform, destination));                        //Startet die Animation für den Fall des Chips
        WinCondition(activePlayer);                                                            //Checkt ob durch den Zug der aktive Spieler gewonnen hat
    }

    else {
        Debug.Log("Column is full");                        //Wenn die row kleiner als 0 --> Spalte ist voll
    }
    
}
    public Player PlayerSwitch(){                           //Funktion für Spielerwechsel
        if (playercount % 2 == 0){                          //Wenn der playercount durch 2 geteilt wird und keinen Rest hat
            activePlayer = Player.One;                      //setze den aktiven Spieler auf Spieler 1
            playerSelect1.SetActive(false);                 //Deaktiviert die UI für Spieler 1
            playerSelect2.SetActive(true);                  //Aktiviert die UI für Spieler 2
        }
        else{                                              //Wenn das nicht der Fall ist {...}
            activePlayer = Player.Two;                      //Aktiver Spieler ist Spieler 2
            playerSelect2.SetActive(false);                 //Deaktiviert die UI für Spieler 2
            playerSelect1.SetActive(true);                  //Aktiviert die UI für Spieler 1 
        }
        playercount++;                                     //Erhöht den playercount um 1
        return activePlayer;                                //gibt den aktiven Spiler zurück
    }

    private IEnumerator ChipDropAnimation(Transform chipTransform, Vector3 destination){           //Animation
    Vector3 startPosition = chipTransform.position;                                                //Speichert die Startposition vom Chip

    float elapsed = 0f;                                                                            //Verstrichene Zeit startet bei 0
    float duration = 0.5f;                                                                         //Dauer der Animtion

    while (elapsed < duration){                                                                    //Solange die Laufzeit kleiner ist als die Dauer der Animation mache {...}
        float t = elapsed / duration;                                                              //Definiert t als Zeitvariable
        chipTransform.position = Vector3.Lerp(startPosition, destination, t);                      //Berecchnet die Position des chips auf dem Weg von Startposition zu Zielposition
        elapsed += Time.deltaTime;                                                                 //Erhöht verstrichene Zeit pro frame
        yield return null;                                                                         //Warten bis nächster Frame
    }
        chipTransform.position = destination;                                                      //Die Endposition des Chips auf Zielposition setzen
}


    bool WinCondition(Player countPlayer){

    //Horizontal
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
    //Vertikal
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
    //Diagonal von unten nach oben
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
    //Diagonal von oben nach unten
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
