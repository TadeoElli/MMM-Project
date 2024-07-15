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
    [SerializeField] private Sprite unlockedIcon, lockedIcon;
    [SerializeField] private Image image, hoverImage, pressedImage;
    [SerializeField] private UnityEvent onLevelUp;
    [Header("Description")]
    [SerializeField] private GameObject description, generalInfo;
    [Header("Requisites")]
    [SerializeField] private List<UpgradeIcon> previousSkills;
    [SerializeField] private List<int> previousLevel;
    [Header("Stats")]
    public Observer<int> availableSkillPoints = new Observer<int>(0); //Los puntos de habilidad con los que se empieza
    [SerializeField] private int points;
    [SerializeField] private int pointsToComplete, maxLevel, currentTechLevel;
    public int currentLevel;
    [SerializeField] private bool requireTechLevel, isAvailable;
    [SerializeField] private TextMeshProUGUI textComp;

    private void Start()
    {
        CheckIconStatus();
        isInteractable = true;
    }
    protected override void Update()
    {
        base.Update();
    }
    
    public void CheckIconStatus()
    {
        if (previousSkills != null)
        {
            for (int i = 0; i < previousSkills.Count; i++)
            {
                if (previousSkills[i].currentLevel < previousLevel[i])
                {
                    image.sprite = lockedIcon;
                    return;
                }
            }

        }
        if (currentLevel <= maxLevel)
        {
            isAvailable = true;
            image.sprite = unlockedIcon;
        }
        else
        {
            isAvailable=false;
            image.sprite = lockedIcon;
        }

    }

    protected override void OnClickEnter()
    {
        if (hoverImage != null) hoverImage.gameObject.SetActive(true);
        if (description != null) { description.SetActive(true); }
        if(generalInfo != null) generalInfo.SetActive(false);
    }
    protected override void OnClickExit()
    {
        if (hoverImage != null) hoverImage.gameObject.SetActive(false);
        if (pressedImage != null) pressedImage.gameObject.SetActive(false);
        if (description != null) { description.SetActive(false); }
        if (generalInfo != null) generalInfo.SetActive(true);
    }
    protected override void OnClickDown()
    {
        if (pressedImage != null) pressedImage.gameObject.SetActive(true);
    }
    protected override void OnClickUp()
    {
        if (pressedImage != null) pressedImage.gameObject.SetActive(false);
        if(isAvailable)
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
        }
    }
    private void CheckForNextLevel()
    {
        if (textComp != null)
            textComp.text = points + "/" + pointsToComplete;
        if (points >= pointsToComplete)
        {
            points = 0;
            currentLevel++;
            SetObtained();
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
    public void SetObtained()
    {
        if(textComp != null && currentLevel >= maxLevel)
        {
            textComp.text = pointsToComplete + "/" + pointsToComplete;
            textComp.color = Color.green;
        }
    }

}
