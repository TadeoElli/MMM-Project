using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIcon : IconHud
{
    /// <summary>
    /// Esta clase sirve como base para manejar los iconos del menu de mejoras
    /// </summary>
    [SerializeField] private Image image, hoverImage, pressedImage;
    [Header("Description")]
    [SerializeField] private GameObject description;
    [Header("Requisites")]
    [SerializeField] private List<UpgradeIcon> previousSkills;
    [SerializeField] private List<int> previousLevel;
    [Header("Stats")]
    public Observer<int> availableSkillPoints = new Observer<int>(0); //Los puntos de habilidad con los que se empieza
    [SerializeField] private int points; 
    [SerializeField] private int pointsToComplete, maxLevel;
    public int currentLevel;

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
        if(currentLevel < maxLevel)
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
        if(availableSkillPoints.Value > 0)
        {
            points++;
            availableSkillPoints.Value--;
            CheckForNextLevel();
        }
    }
    private void CheckForNextLevel()
    {
        if(points >= pointsToComplete)
        {
            points = 0;
            currentLevel++;
        }
    }
    public void SetAvailablePointsValue(int amount)
    {
        availableSkillPoints.Value = amount;
    }

}
