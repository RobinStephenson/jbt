using System.Collections;
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

    //Resource Display variables
    public Text MoneyDisplay;
    public Text OreDisplay;
    public Text PowerDisplay;
    public Text RoboticonDisplay;

    //Message variables
    public Text MessageText;

    //Timer variables
    public Text TimerText;

    //Phase 2 variables
    public GameObject BuyRoboticonPanel;
    public Text MarketRoboticonStock;
    public string baseRoboticonStockText;

    //Phase 3 variables
    public GameObject InstallRobitconPanel;
    public GameObject CustomiseRoboticonPanel;

    //Phase 4 variables
    public GameObject ProductionDisplay;
    public Text ProductionText;
    public string BaseProductionText;

    private void Awake()
    {
        //Set the static reference to the controller to the only controller in the scene
        controller = GetComponent<UIController>();
    }

    void Start () {
        //Get the base tile description text, before formatting
        baseTileText = selectedTileText.text;
        baseRoboticonStockText = MarketRoboticonStock.text;
        BaseProductionText = ProductionText.text;
    }
	
	void Update () {
        
	}

    public static void DisplayTileInfo(PhysicalTile t)
    {
        controller.selectedTilePanel.SetActive(true);
        controller.selectedTileText.text = string.Format(controller.baseTileText, t.ContainedTile.Owner == null? "Nobody" : t.ContainedTile.Owner.PlayerName, t.Price, t.Ore.ToString("00"), t.Power.ToString("00"), "No roboticon installed");
    }

    public static void HideTileInfo()
    {
        controller.selectedTilePanel.SetActive(false);
    }

    public static void UpdateTurnInfo(int turn, int phase, string playerName)
    {
        controller.TurnDisplay.text = "Turn " + turn.ToString("00");
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

    public static void UpdateResourceDisplay(Inventory playerInv)
    {
        controller.MoneyDisplay.text = playerInv.Money.ToString("000");
        controller.OreDisplay.text = playerInv.GetItemAmount(ItemType.Ore).ToString("000");
        controller.PowerDisplay.text = playerInv.GetItemAmount(ItemType.Power).ToString("000");
        controller.RoboticonDisplay.text = "00" + "/" + playerInv.GetItemAmount(ItemType.Roboticon).ToString("00");
    }

    public static void ShowBuyRoboticon(Market m)
    {
        controller.BuyRoboticonPanel.SetActive(true);
        controller.MarketRoboticonStock.text = string.Format(controller.baseRoboticonStockText, m.Stock.GetItemAmount(ItemType.Roboticon));
    }

    public static void HideBuyRoboticon()
    {
        controller.BuyRoboticonPanel.SetActive(false);
    }

    public static void ShowInstallRoboticon()
    {
        controller.InstallRobitconPanel.SetActive(true);
    }

    public static void ShowCustomiseRoboticon()
    {
        controller.CustomiseRoboticonPanel.SetActive(true);
    }

    public static void HideInstallRoboticon()
    {
        controller.InstallRobitconPanel.SetActive(false);
    }

    public static void HideCustomiseRoboticon()
    {
        controller.CustomiseRoboticonPanel.SetActive(false);
    }

    public static void ShowProductionDisplay(Dictionary<ItemType, int> production)
    {
        controller.ProductionDisplay.SetActive(true);
        controller.ProductionText.text = string.Format(controller.BaseProductionText, production[ItemType.Ore].ToString("000"), production[ItemType.Power].ToString("000"));
    }

    public static void HideProductionDisplay()
    {
        controller.ProductionDisplay.SetActive(false);
    }

    public static void ShowMarketDisplay()
    {

    }

    public static void HideMarketDisplay()
    {

    }
}