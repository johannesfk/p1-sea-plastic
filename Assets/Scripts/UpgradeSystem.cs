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

    // selectedUpgradeIndex is set to a number that's not in any array. So that default means no upgrade is selected.
    private int selectedUpgradeIndex = -1;

    // Variables for upgrade data, such as titles, descriptions, costs.
    private string[] upgradeTitles = { "Upgrade 1", "Upgrade 2", "Upgrade 3" };
    private string[] upgradeDescriptions = { "Description 1", "Description 2", "Description 3" };
    private int[] upgradeCosts = { 10, 20, 10 };
    private int upgradeMoney = 30;

    void Start()
    {
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
        // This method is called when an upgrade button is clicked
        selectedUpgradeIndex = index;
        Debug.Log("Selected Upgrade: " + upgradeTitles[index]);
    }

    void ConfirmPurchase()
    {
        // This method is called when the confirm button is clicked
        if (selectedUpgradeIndex != -1)
        {
            int cost = upgradeCosts[selectedUpgradeIndex];

            // Check if the player has enough resources to purchase the upgrade
            if (upgradeMoney >= cost)
            {
                // Deduct the cost from player resources
                upgradeMoney -= cost;

                // Apply the upgrade
                ApplyUpgrade(selectedUpgradeIndex);

                // Log or perform any other actions related to successful purchase
                Debug.Log("Upgrade Purchased: " + upgradeTitles[selectedUpgradeIndex]);

                // Reset selectedUpgradeIndex to indicate that no upgrade is currently selected
                selectedUpgradeIndex = -1;

                // Update UI to reflect changes
                UpdateMoneyUI();
            }
            else
            {
                // Log or perform actions for insufficient resources
                Debug.Log("Insufficient resources to purchase upgrade");
            }
        }
        else
        {
            // Log or perform actions for no upgrade selected
            Debug.Log("No upgrade selected");
        }
    }

    void ApplyUpgrade(int index)
    {
        // Add logic to apply the upgrade based on the selected index
        // For simplicity, let's just log the upgrade application
        Debug.Log("Applying Upgrade: " + upgradeTitles[index]);
    }

    void UpgradeClicked(int upgradeIndex)
    {
        // Handle upgrade logic here
        // You can deduct the cost, apply the upgrade, etc.

        // Update UI with new data
        UpdateUpgradeUI(upgradeIndex);
    }

    void UpdateUpgradeUI(int upgradeIndex)
    {
        // Update the UI with data based on the selected upgrade
        titleText.text = "Title: " + upgradeTitles[upgradeIndex];
        descriptionText.text = "Description: " + upgradeDescriptions[upgradeIndex];
        costText.text = "Cost: " + upgradeCosts[upgradeIndex];
    }

    void UpdateMoneyUI()
    {
        // Update the UI with the current money value
        Money.text = "Money: " + upgradeMoney;
    }
}