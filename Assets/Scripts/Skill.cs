using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescription[id]}\nCost:{skillTree.Money}/1$";
        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow
            : skillTree.Money >= 1 ? Color.green : Color.white;

        foreach (var connectedSkills in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkills].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkills].SetActive(skillTree.SkillLevels[id] > 0);
        }   
    }

    public void Buy()
    {
        if (skillTree.Money < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        skillTree.Money -= 1;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();

    }
}
