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
    public Text RoboticonPriceText;
    public string baseRoboticonPriceText;

    //Phase 3 variables
    public GameObject InstallRobitconPanel;
    public GameObject CustomiseRoboticonPanel;
    public Text CurrentCustomisationName;
    public Text CurrentCustomisationText;
    public string CustomisationNameTemplate;
    public string CurrentCustomisationDescription;
    public GameObject UpgradeOne;
    public GameObject UpgradeTwo;
    public Image CurrentUpgradeImage;

    //Phase 4 variables
    public GameObject ProductionDisplay;
    public Text ProductionText;
    public string BaseProductionText;

    //Phase 5 variables
    public GameObject MarketPanel;
    public Text OreStockText;
    public Text PowerStockText;
    public string StockTextTemplate;
    public Text OreBuyText;
    public Text OreSellText;
    public Text PowerBuyText;
    public Text PowerSellText;
    public string BuyTextTemplate;
    public string SellTextTemplate;

    private void Awake()
    {
        //Set the static reference to the controller to the only controller in the scene
        controller = GetComponent<UIController>();
    }

    void Start () {
        //Get the base tile description text, before formatting
        baseTileText = selectedTileText.text;
        baseRoboticonStockText = MarketRoboticonStock.text;
        baseRoboticonPriceText = RoboticonPriceText.text;
        BaseProductionText = ProductionText.text;
        CurrentCustomisationDescription = CurrentCustomisationText.text;
        CustomisationNameTemplate = CurrentCustomisationName.text;
        StockTextTemplate = OreStockText.text;
        BuyTextTemplate = OreBuyText.text;
        SellTextTemplate = OreSellText.text;
    }

    public static void DisplayTileInfo(PhysicalTile t)
    {
        string roboticonName = "No roboticon installed";

        if (t.ContainedTile.InstalledRoboticon == null)
        {
            roboticonName = "No roboticon installed";
        }
        else
        {
            if(t.ContainedTile.InstalledRoboticon.InstalledCustomisations.Count == 0)
            {
                roboticonName = "Roboticon Installed: Base";
            }
            else
            {
                roboticonName = "Roboticon Installed: " + t.ContainedTile.InstalledRoboticon.InstalledCustomisations[0].Name;
            }
        }
        controller.selectedTilePanel.SetActive(true);
        controller.selectedTileText.text = string.Format(controller.baseTileText, t.ContainedTile.Owner == null? "Nobody" : t.ContainedTile.Owner.PlayerName, t.Price, t.Ore.ToString("00"), t.Power.ToString("00"), roboticonName);
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
        if (t.SecondsRemaining == -1)
        {
            controller.TimerText.text = "Infinite";
        }
        else
        {
            controller.TimerText.text = t.SecondsRemaining.ToString() + "s";
        }
    }

    public static void UpdateResourceDisplay(AbstractPlayer player)
    {
        controller.MoneyDisplay.text = player.Inv.Money.ToString("000");
        controller.OreDisplay.text = player.Inv.GetItemAmount(ItemType.Ore).ToString("000");
        controller.PowerDisplay.text = player.Inv.GetItemAmount(ItemType.Power).ToString("000");
        controller.RoboticonDisplay.text = player.InstalledRoboticonCount.ToString("00") + "/" + player.Inv.GetItemAmount(ItemType.Roboticon).ToString("00");
    }

    public static void ShowBuyRoboticon(Market m)
    {
        controller.BuyRoboticonPanel.SetActive(true);
        controller.MarketRoboticonStock.text = string.Format(controller.baseRoboticonStockText, m.Stock.GetItemAmount(ItemType.Roboticon));
        controller.RoboticonPriceText.text = string.Format(controller.baseRoboticonPriceText, m.GetBuyPrice(ItemType.Roboticon).ToString("00"));
    }

    public static void HideBuyRoboticon()
    {
        controller.BuyRoboticonPanel.SetActive(false);
    }

    public static void ShowInstallRoboticon()
    {
        controller.InstallRobitconPanel.SetActive(true);
    }

    public static void ShowCustomiseRoboticon(Roboticon rc)
    {
        string spritePath;
        string customisationName;

        if (rc.InstalledCustomisations.Count == 0)
        {
            spritePath = "Sprites/BaseRobo";
            customisationName = "Base";
            ShowUpgrades();  
        }
        else
        {
            spritePath = rc.InstalledCustomisations[0].SpritePath;
            customisationName = rc.InstalledCustomisations[0].Name;

            //Remove upgrades shown for now if an upgrade has been applied
            HideUpgrades();
        }

        controller.CustomiseRoboticonPanel.SetActive(true);

        controller.CurrentUpgradeImage.sprite = Resources.Load<Sprite>(spritePath);
        controller.CurrentCustomisationName.text = string.Format(controller.CustomisationNameTemplate, customisationName);
        controller.CurrentCustomisationText.text = string.Format(controller.CurrentCustomisationDescription, rc.ProductionMultiplier(ItemType.Ore).ToString("00"), rc.ProductionMultiplier(ItemType.Power).ToString("00"));
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

    public static void ShowMarketDisplay(Market m)
    {
        controller.MarketPanel.SetActive(true);
        controller.OreStockText.text = string.Format(controller.StockTextTemplate, m.Stock.GetItemAmount(ItemType.Ore).ToString("00"));
        controller.PowerStockText.text = string.Format(controller.StockTextTemplate, m.Stock.GetItemAmount(ItemType.Power).ToString("00"));
        controller.OreBuyText.text = string.Format(controller.BuyTextTemplate, m.GetBuyPrice(ItemType.Ore).ToString("00"));
        controller.PowerBuyText.text = string.Format(controller.BuyTextTemplate, m.GetBuyPrice(ItemType.Power).ToString("00"));
        controller.OreSellText.text = string.Format(controller.SellTextTemplate, m.GetSellPrice(ItemType.Ore).ToString("00"));
        controller.PowerSellText.text = string.Format(controller.SellTextTemplate, m.GetSellPrice(ItemType.Power).ToString("00"));
    }

    public static void HideMarketDisplay()
    {
        controller.MarketPanel.SetActive(false);
    }

    private static void ShowUpgrades()
    {
        controller.UpgradeOne.SetActive(true);
        controller.UpgradeTwo.SetActive(true);
    }

    private static void HideUpgrades()
    {
        controller.UpgradeOne.SetActive(false);
        controller.UpgradeTwo.SetActive(false);
    }
}