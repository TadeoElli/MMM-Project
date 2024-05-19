using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Image> triangles;
    [SerializeField] private List<TextMeshProUGUI> waveDirections;
    [SerializeField] public Direction _direction;
    void StartState()
    {
        switch (_direction)
        {
            case Direction.Left:
                triangles[0].color = Color.red;
                waveDirections[0].color = Color.red;
                break;
            case Direction.Right:
                triangles[1].color = Color.red;
                waveDirections[2].color = Color.red;
                break;
            case Direction.Both:
                triangles[0].color = Color.red;
                triangles[1].color = Color.red;
                waveDirections[1].color = Color.red;
                break;
            default:
                break;
        }
    }

    public void ResetMenu()
    {
        triangles.ForEach(x => x.color = Color.gray);
        waveDirections.ForEach(x=> x.color = Color.gray);
        StartState();    
    }


}
