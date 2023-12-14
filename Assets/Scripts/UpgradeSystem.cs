using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{

    [Header("Upgrade Stats")]
    [SerializeField] private float recycleUpgrade = 2;
    [SerializeField] private float moneyUpgrade = 1.5f;
    [SerializeField] private float landfillUpgrade = 2;
    [SerializeField] private float boatUpgrade = 5;
    [SerializeField] private float incineratorUpgrade = 2;

    [Header("Upgrade Elements")]
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text Money;

    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;
    public Button upgradeButton4;
    public Button upgradeButton5;
    public Button upgradeButton6;
    public Button upgradeButton7;
    public Button upgradeButton8;
    public Button upgradeButton9;
    public Button upgradeButton10;
    public Button upgradeButton11;
    public Button upgradeButton12;
    public Button confirmButton;

    private float upgradeMoney;
    private int selectedUpgradeIndex = -1;

    // Variables for upgrade data
    private UpgradeData[] upgrades;
    private class UpgradeData
    {
        public string title;
        public string description;
        public int cost;
        public bool isPurchased;
        public int prerequisiteIndex;  // Index of the prerequisite upgrade, -1 if none

        public UpgradeData(string title, string description, int cost, bool isPurchased, int prerequisiteIndex = -1)
        {
            this.title = title;
            this.description = description;
            this.cost = cost;
            this.isPurchased = isPurchased;
            this.prerequisiteIndex = prerequisiteIndex;

        }
    }

    void Start()
    {

        upgradeMoney = ShopController.instance.money;

        // Initialize upgrade data
        upgrades = new UpgradeData[]
        {
            // Title, Description, cost, ispurchased, prerequisite upgrade
            new UpgradeData("Recycler+", "Makes Recyclers more effective.", 10, false), // Prerequisite: None
            new UpgradeData("Repurpose", "Make 50% more money from Reyclers. <br>Prerequisite: Recycler+", 20, false, 0),  // Prerequisite: Upgrade 1
            new UpgradeData("Deluxe Recyclers", "The BEST Recyclers in the buisness. <br>Prerequisite: Recycler+", 10, false, 0),   // Prerequisite: Upgrade 1
            new UpgradeData("Better Incinerators", "Makes Incinerators more effective at disposing trash.", 10, false), // Prerequisite: None
            new UpgradeData("Green Incineration", "Incinerators won't pollude the air. <br>Prerequisite: Better Incinerators", 20, false, 3),  // Prerequisite: Upgrade 4
            new UpgradeData("Super Incinerators", "Makes Incinerators EVEN more effective at disposing trash. <br>Prerequisite: Better Incinerators", 10, false, 3),   // Prerequisite: Upgrade 4
            new UpgradeData("Better Boats", "Makes Boats better at removing trash", 10, false), // Prerequisite: None
            new UpgradeData("SUPER CLAW", "Gives Boats a better claw for removing trash from the ocean <br>Prerequisite: Better Boats", 20, false, 6),  // Prerequisite: Upgrade 7
            new UpgradeData("Better Boats+", "Makes Boats EVEN better at removing trash <br>Prerequisite: Better Boats", 10, false, 6),   // Prerequisite: Upgrade 7
            new UpgradeData("Bigger landfills", "Makes Landfills bigger so it can store more trash", 10, false), // Prerequisite: None 
            new UpgradeData("EVEN BIGGER LANDFILLS", "Makes Landfills bigger so it can store more trash <br>Prerequisite: Bigger landfills", 20, false, 9),  // Prerequisite: Upgrade 10
            new UpgradeData("Really Big Landfills", "Makes Landfills bigger so it can store more trash <br>Prerequisite: Bigger landfills", 10, false, 9),   // Prerequisite: Upgrade 10
        };

        confirmButton.onClick.AddListener(ConfirmPurchase);
        UpdateButtonColors();
    }

    private void Update()
    {
        Money.text = "Money: " + ShopController.instance.money;

        Debug.Log(upgradeMoney);

        if (upgradeMoney < ShopController.instance.money)
        {
            ShopController.instance.money = upgradeMoney;
        }
        else
        {
            upgradeMoney = ShopController.instance.money;
        }

    }

    public void SelectUpgrade(int index)
    {
        // Check if the index is valid before updating selectedUpgradeIndex
        if (index >= 0 && index < upgrades.Length)
        {
            selectedUpgradeIndex = index;
            Debug.Log("Selected Upgrade: " + upgrades[index].title);
            UpdateButtonColors();
            UpdateUpgradeUI(index);
        }
        else
        {
            Debug.LogError("Invalid upgradeIndex: " + index);
        }
    }

    void ConfirmPurchase()
    {
        if (selectedUpgradeIndex != -1 && !upgrades[selectedUpgradeIndex].isPurchased)
        {
            int cost = upgrades[selectedUpgradeIndex].cost;

            if (ArePrerequisitesFulfilled(selectedUpgradeIndex) && upgradeMoney >= cost)
            {
                upgradeMoney -= cost;
                upgrades[selectedUpgradeIndex].isPurchased = true;

                ApplyUpgrade(selectedUpgradeIndex);

                Debug.Log("Upgrade Purchased: " + upgrades[selectedUpgradeIndex].title);

                UpdateButtonColors();
                UpdateUpgradeUI(selectedUpgradeIndex);
            }
            else
            {
                Debug.Log("Insufficient resources or prerequisites not fulfilled");
            }
        }
        else
        {
            Debug.Log("No upgrade selected or already purchased");
        }
    }
    bool ArePrerequisitesFulfilled(int upgradeIndex)
    {
        // Check if upgradeIndex is within the valid range of the upgrades array
        if (upgradeIndex >= 0 && upgradeIndex < upgrades.Length)
        {
            int prerequisiteIndex = upgrades[upgradeIndex].prerequisiteIndex;

            if (prerequisiteIndex == -1)
            {
                return true;
            }

            // Check if prerequisiteIndex is within the valid range of the upgrades array
            if (prerequisiteIndex >= 0 && prerequisiteIndex < upgrades.Length)
            {
                Debug.Log($"Upgrade {upgradeIndex}, Prerequisite {prerequisiteIndex}, isPurchased: {upgrades[prerequisiteIndex].isPurchased}");
                // If there is no prerequisite, or the prerequisite is already purchased, return true
                if (prerequisiteIndex == -1 || upgrades[prerequisiteIndex].isPurchased)
                {
                    return true;
                }
            }
        }
    
        // If the prerequisite is not purchased or indices are out of bounds, return false
        return false;
    }
    void ApplyUpgrade(int index)
    {
        Debug.Log("Applying Upgrade: " + upgrades[index].title);

        switch (index)
        {
            case 0:
                {
                    PollutionController.instance.recyclePollutionRemove += recycleUpgrade;
                    break;
                }
            case 1:
                {   

                    ShopController.instance.incomeMultiplier += moneyUpgrade;

                    break;
                }
            case 2:
                {
                    PollutionController.instance.recyclePollutionRemove += recycleUpgrade;
                    break;
                }
            case 3:
                {
                    PollutionController.instance.inciniratorPollutionRemove += incineratorUpgrade;
                    break;
                }
            case 4:
                {
                    Debug.Log("lol den gør intet");
                    break;
                }
            case 5:
                {
                    PollutionController.instance.inciniratorPollutionRemove += incineratorUpgrade;
                    break;
                }
            case 6:
                {
                    PollutionController.instance.boatStrength += boatUpgrade;
                    break;
                }
            case 7:
                {
                    PollutionController.instance.boatStrength += boatUpgrade;
                    break;
                }
            case 8:
                {
                    PollutionController.instance.boatStrength += boatUpgrade;
                    break;
                }
            case 9:
                {

                    PollutionController.instance.landfillPollutionRemove += landfillUpgrade;
                    break;
                }
            case 10:
                {
                    PollutionController.instance.landfillPollutionRemove += landfillUpgrade;
                    break;
                }
            case 11:
                {
                    PollutionController.instance.landfillPollutionRemove += landfillUpgrade;
                    break;
                }
        }

    }
    void UpdateUpgradeUI(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex < upgrades.Length)
        {
            Debug.Log("Updating UI for Upgrade: " + upgrades[upgradeIndex].title);
            titleText.text = "Title: " + upgrades[upgradeIndex].title;
            descriptionText.text = "Description: " + upgrades[upgradeIndex].description;
            costText.text = "Cost: " + upgrades[upgradeIndex].cost;
            
        }
        else
        {
            Debug.LogError("Invalid upgradeIndex: " + upgradeIndex);
        }
    }

    void UpdateButtonColors()
    {
        // Standard color is set to blue, darker blue for purchased upgrades
        SetButtonColor(upgradeButton1, upgrades[0].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));
        SetButtonColor(upgradeButton2, upgrades[1].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(1) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton3, upgrades[2].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(2) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton4, upgrades[3].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));
        SetButtonColor(upgradeButton5, upgrades[4].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(4) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton6, upgrades[5].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(5) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton7, upgrades[6].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));
        SetButtonColor(upgradeButton8, upgrades[7].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(7) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton9, upgrades[8].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(8) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton10, upgrades[9].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));
        SetButtonColor(upgradeButton11, upgrades[10].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(10) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        SetButtonColor(upgradeButton12, upgrades[11].isPurchased ? Color.blue : (ArePrerequisitesFulfilled(11) ? new Color(46f / 255f, 115f / 255f, 219f / 255f) : Color.red));
        // Highlights the selected upgrade button
        if (selectedUpgradeIndex != -1)
        {
            Button selectedButton = null;

            switch (selectedUpgradeIndex)
            {
                case 0:
                    selectedButton = upgradeButton1;
                    break;
                case 1:
                    selectedButton = upgradeButton2;
                    break;
                case 2:
                    selectedButton = upgradeButton3;
                    break;
                case 3:
                    selectedButton = upgradeButton4;
                    break;
                case 4:
                    selectedButton = upgradeButton5;
                    break;
                case 5:
                    selectedButton = upgradeButton6;
                    break;
                case 6:
                    selectedButton = upgradeButton7;
                    break;
                case 7:
                    selectedButton = upgradeButton8;
                    break;
                case 8:
                    selectedButton = upgradeButton9;
                    break;
                case 9:
                    selectedButton = upgradeButton10;
                    break;
                case 10:
                    selectedButton = upgradeButton11;
                    break;
                case 11:
                    selectedButton = upgradeButton12;
                    break;
            }
            if (selectedButton != null)
            {
                // highlight in a color
                selectedButton.GetComponent<Image>().color = Color.magenta;
            }
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
}