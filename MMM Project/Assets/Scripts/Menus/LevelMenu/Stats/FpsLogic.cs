using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class FpsLogic : MonoBehaviour
{
/// <summary>
/// Esta clase es para mostrar el contador de fps en el juego
/// </summary>
    private float deltaTime = 0.0f;

    [SerializeField]private TextMeshProUGUI textComp;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f/deltaTime;
        textComp.text = Mathf.FloorToInt(fps).ToString();
    }
}
