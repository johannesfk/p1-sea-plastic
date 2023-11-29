using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text Money;

    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;
    public Button confirmButton;

    private int selectedUpgradeIndex = -1;
    private int upgradeMoney = 30;

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
        // Initialize upgrade data
        upgrades = new UpgradeData[]
        {
            // Title, Description, cost, ispurcheased, prerequisite upgrade
            new UpgradeData("Upgrade 1", "Description 1", 10, false), // Prerequisite: None
            new UpgradeData("Upgrade 2", "Description 2 <br>Prerequisite: Upgrade 1", 20, false, 0),  // Prerequisite: Upgrade 1
            new UpgradeData("Upgrade 3", "Description 3", 10, false, 0)   // Prerequisite: Upgrade 1
        };

        // Attach functions to button click events
        upgradeButton1.onClick.AddListener(() => UpgradeClicked(0));
        upgradeButton2.onClick.AddListener(() => UpgradeClicked(1));
        upgradeButton3.onClick.AddListener(() => UpgradeClicked(2));
        confirmButton.onClick.AddListener(ConfirmPurchase);

        // Update UI with initial data
        UpdateUpgradeUI(0);
        UpdateMoneyUI();
    }

    public void SelectUpgrade(int index)
    {
        // Check if the index is valid before updating selectedUpgradeIndex
        if (index >= 0 && index < upgrades.Length)
        {
            selectedUpgradeIndex = index;
            Debug.Log("Selected Upgrade: " + upgrades[index].title);
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

            if (upgradeMoney >= cost && ArePrerequisitesFulfilled(selectedUpgradeIndex))
            {
                upgradeMoney -= cost;
                upgrades[selectedUpgradeIndex].isPurchased = true;

                ApplyUpgrade(selectedUpgradeIndex);

                Debug.Log("Upgrade Purchased: " + upgrades[selectedUpgradeIndex].title);

                UpdateUpgradeUI(selectedUpgradeIndex);
                UpdateMoneyUI();
                UpdateButtonStates();
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
        // Check if all prerequisites for the selected upgrade are fulfilled
        for (int i = 0; i < upgradeIndex; i++)
        {
            if (!upgrades[i].isPurchased)
            {
                return false;
            }
        }
        return true;
    }

    void ApplyUpgrade(int index)
    {
        Debug.Log("Applying Upgrade: " + upgrades[index].title);
        // Add logic to apply the upgrade based on the selected index
    }

    void UpgradeClicked(int upgradeIndex)
    {
        Debug.Log("UpgradeClicked with index: " + upgradeIndex);
        // Handle upgrade logic here
        // You can deduct the cost, apply the upgrade, etc.

        // Update UI with new data
        UpdateUpgradeUI(upgradeIndex);
    }

    void UpdateUpgradeUI(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex < upgrades.Length)
        {
            titleText.text = "Title: " + upgrades[upgradeIndex].title;
            descriptionText.text = "Description: " + upgrades[upgradeIndex].description;
            costText.text = "Cost: " + upgrades[upgradeIndex].cost;
        }
        else
        {
            Debug.LogError("Invalid upgradeIndex: " + upgradeIndex);
        }
    }

    void UpdateMoneyUI()
    {
        Money.text = "Money: " + upgradeMoney;
    }

    // Data structure to represent upgrade information
    
    void UpdateButtonStates()
    {
        // Update button states based on upgrade availability
        for (int i = 0; i < upgrades.Length; i++)
        {
            bool canPurchase = !upgrades[i].isPurchased && ArePrerequisitesFulfilled(i) && upgradeMoney >= upgrades[i].cost;
            if (i == selectedUpgradeIndex)
            {
                // Handle the selected upgrade separately if needed
            }
            else
            {
                // Disable or change appearance based on canPurchase
                if (canPurchase)
                {
                    // Enable or set normal appearance
                }
                else
                {
                    // Disable or set disabled appearance
                }
            }
        }
    }

}