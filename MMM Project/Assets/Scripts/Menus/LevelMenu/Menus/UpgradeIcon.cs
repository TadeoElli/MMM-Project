using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIcon : IconHud
{
    /// <summary>
    /// Esta clase sirve como base para manejar los iconos del menu de mejoras
    /// </summary>
    [SerializeField] private Image image, blockImage,hoverImage, pressedImage;
    [Header("Description")]
    [SerializeField] private GameObject description;
    [Header("Requisites")]
    [SerializeField] private int points; 
    [SerializeField] private int pointsToComplete, currentLevel, maxLevel;


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
    }
}
