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
        confirmButton.onClick.AddListener(ConfirmPurchase);

        // Update UI with initial data
        UpdateUpgradeUI(0);
        UpdateMoneyUI();
        UpdateButtonColors();
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

            if (upgradeMoney >= cost && ArePrerequisitesFulfilled(selectedUpgradeIndex))
            {
                upgradeMoney -= cost;
                upgrades[selectedUpgradeIndex].isPurchased = true;

                ApplyUpgrade(selectedUpgradeIndex);

                Debug.Log("Upgrade Purchased: " + upgrades[selectedUpgradeIndex].title);

                UpdateUpgradeUI(selectedUpgradeIndex);
                UpdateMoneyUI();
                UpdateButtonColors();
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
        int prerequisiteIndex = upgrades[upgradeIndex].prerequisiteIndex;

        // If there is no prerequisite, or the prerequisite is already purchased, return true
        if (prerequisiteIndex == -1 || upgrades[prerequisiteIndex].isPurchased)
        {
            return true;
        }

        // If the prerequisite is not purchased, return false
        return false;
    }

    void ApplyUpgrade(int index)
    {
        Debug.Log("Applying Upgrade: " + upgrades[index].title);
        // Add logic to apply the upgrade based on the selected index
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

    void UpdateMoneyUI()
    {
        Money.text = "Money: " + upgradeMoney;
    }

    void UpdateButtonColors()
    {
        // Standard color is set to blue and if the upgrade is Purcheased its set to a darker blue
        SetButtonColor(upgradeButton1, upgrades[0].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));
        SetButtonColor(upgradeButton2, upgrades[1].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));
        SetButtonColor(upgradeButton3, upgrades[2].isPurchased ? Color.blue : new Color(46f / 255f, 115f / 255f, 219f / 255f));

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
            }

            if (selectedButton != null)
            {
                // highlight in a different color
                selectedButton.GetComponent<Image>().color = Color.green;
            }
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
}