﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A singleton used to control all the UI elements in the scene to clean up code in TurnController a bit
/// </summary>
public class UIController : MonoBehaviour {

    private static UIController controller;

    //Variables for displaying selected tile info
    public GameObject selectedTilePanel;
    public Text selectedTileText;
    public string baseTileText;

    //Turn Display variables
    public Text TurnDisplay;
    public Text PhaseDisplay;
    public Text PlayerDisplay;

    //Message variables
    public Text MessageText;

    //Timer variables
    public Text TimerText;
    //Timeout t;

    void Start () {
        //Set the static reference to the controller to the only controller in the scene
        controller = GetComponent<UIController>();

        //Get the base tile description text, before formatting
        baseTileText = selectedTileText.text;
        //t = new Timeout(60);
    }
	
	void Update () {
        //t.UpdateTimer();
        //Debug.Log(t.SecondsRemaining.ToString());
	}

    public static void DisplayTileInfo(PhysicalTile t)
    {
        controller.selectedTilePanel.SetActive(true);
        controller.selectedTileText.text = string.Format(controller.baseTileText, "Nobody", 15, t.Ore.ToString("00"), t.Power.ToString("00"), "No roboticon installed");
    }

    public static void HideTileInfo()
    {
        controller.selectedTilePanel.SetActive(false);
    }

    public static void UpdateTurnInfo(int turn, int phase, string playerName)
    {
        controller.TurnDisplay.text = "Turn " + turn.ToString();
        controller.PhaseDisplay.text = "Phase " + phase.ToString();
        controller.PlayerDisplay.text = playerName;
    }

    public static void DisplayMessage(string message)
    {
        controller.MessageText.text = message;
    }

    public static void UpdateTimerDisplay(Timeout t)
    {
        controller.TimerText.text = t.SecondsRemaining.ToString() + "s";
    }
}