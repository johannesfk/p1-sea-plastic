using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    public void Awake() => skillTree = skillTree = this;


    //  public List<int> SkillLevels;
    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescription;


    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnecterHolder;

    public int Money;

    private void Start()
    {
        Money = 20;

        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10, };
        SkillNames = new[] { "Upgrade 1", "Upgrade 2", "Upgrade 3", "Upgrade 4", "Upgrade 5", "Upgrade 6" };
        SkillDescription = new[]
        {
            "Does a thing 1",
            "Does a thing 2",
            "Does a thing 3",
            "Does a thing 4",
            "Does a thing 5",
            "Does a thing 6",
        };
       foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);

       foreach (var connector in ConnecterHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);

       for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2 };
        SkillList[1].ConnectedSkills = new[] {3};
        UpdateAllSkillUI();
    }
    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList)  skill.UpdateUI();
    }
}

