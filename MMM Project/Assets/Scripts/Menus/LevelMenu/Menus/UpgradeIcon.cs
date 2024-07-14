using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeIcon : IconHud
{
    /// <summary>
    /// Esta clase sirve como base para manejar los iconos del menu de mejoras
    /// </summary>
    [SerializeField] private Image image, hoverImage, pressedImage;
    [SerializeField] private UnityEvent onLevelUp;
    [Header("Description")]
    [SerializeField] private GameObject description;
    [Header("Requisites")]
    [SerializeField] private List<UpgradeIcon> previousSkills;
    [SerializeField] private List<int> previousLevel;
    [Header("Stats")]
    public Observer<int> availableSkillPoints = new Observer<int>(0); //Los puntos de habilidad con los que se empieza
    [SerializeField] private int points;
    [SerializeField] private int pointsToComplete, maxLevel, currentTechLevel;
    public int currentLevel;
    [SerializeField] private bool requireTechLevel;
    [SerializeField] private TextMeshProUGUI textComp;


    protected override void Update()
    {
        base.Update();
        if (previousSkills != null)
        {
            for (int i = 0; i < previousSkills.Count; i++)
            {
                if (previousSkills[i].currentLevel < previousLevel[i])
                    return;
            }

        }
        if (currentLevel < maxLevel)
            isInteractable = true;
        else
            isInteractable = false;
    }

    protected override void OnClickEnter()
    {
        if (hoverImage != null) hoverImage.gameObject.SetActive(true);
        if (description != null) { description.SetActive(true); }
    }
    protected override void OnClickExit()
    {
        if (hoverImage != null) hoverImage.gameObject.SetActive(false);
        if (pressedImage != null) pressedImage.gameObject.SetActive(false);
        if (description != null) { description.SetActive(false); }
    }
    protected override void OnClickDown()
    {
        if (pressedImage != null) pressedImage.gameObject.SetActive(true);
    }
    protected override void OnClickUp()
    {
        if (pressedImage != null) pressedImage.gameObject.SetActive(false);
        IncreasePoints();
    }
    private void IncreasePoints()
    {
        if (availableSkillPoints.Value > 0)
        {
            if (requireTechLevel)
            {
                if (currentTechLevel > 0)
                {
                    points++;
                    currentTechLevel--;
                    availableSkillPoints.Value--;
                    CheckForNextLevel();
                }
            }
            else
            {
                points++;
                availableSkillPoints.Value--;
                CheckForNextLevel();
            }
            if (textComp != null)
                textComp.text = points + "/" + pointsToComplete;
        }
    }
    private void CheckForNextLevel()
    {
        if (points >= pointsToComplete)
        {
            points = 0;
            currentLevel++;
            onLevelUp?.Invoke();
        }
    }

    public void SetAvailablePointsValue(int amount)
    {
        availableSkillPoints.Value = amount;
    }
    public void IncreaseTechLevelPoints()
    {
        currentTechLevel++;
    }
    public void SetNextPointsToLevelUp()
    {
        pointsToComplete = pointsToComplete * 2;
    }

}
