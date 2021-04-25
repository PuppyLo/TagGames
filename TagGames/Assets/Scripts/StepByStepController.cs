using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepController : MonoBehaviour
{
    public int row, col;

    private GameController gameMN;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gamemanager = GameObject.Find("GameController");
        gameMN = gamemanager.GetComponent<GameController>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        gameMN.countStep += 1;  
        gameMN.row = row;
        gameMN.col = col;
        gameMN.startControl = true;
    }
}
