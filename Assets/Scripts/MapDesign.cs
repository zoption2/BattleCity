using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDesign : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private Material ground;
    [Space]
    [SerializeField] private Texture snow;
    [SerializeField] private Texture asphalt;
    [SerializeField] private Texture sand;
    [Space]
    [Header("Walls")]
    [Space]
    [SerializeField] private Material walls;
    [Space]
    [SerializeField] private Material steel;
    [SerializeField] private GameObject snowfield;
    [Space]
    [Header("Weather Volume")]
    [SerializeField] private GameObject volume_snow;
    [SerializeField] private GameObject volume_sand;
    [SerializeField] private GameObject volume_city;

    private GameObject currentVolume;
    private GameObject currentSnowfield;

    private enum Weather { snow, city, sand }
    [SerializeField]private Weather weather;
    private Color[] wallsColor;

    private void Awake()
    {
        //weather = (Weather)Random.Range(0, 1);
        weather = Weather.city;
        switch (weather)
        {
            case Weather.snow:
                wallsColor = new Color[3];
                wallsColor[0] = new Color(0.66f, 0.56f, 0.50f);
                wallsColor[1] = new Color(0.51f, 0.81f, 0.51f);
                wallsColor[2] = new Color(0.83f, 0.36f, 0.36f);
                Color color = wallsColor[Random.Range(0, wallsColor.Length)];

                ground.SetTexture("_BaseMap", snow);
                ground.SetTextureScale("_BaseMap", new Vector2(5f, 4f));
                ground.color = new Color(0.9f, 0.9f, 0.9f);
                walls.color = color;
                steel.color = new Color(0.5f, 0.53f, 0.56f);
                currentSnowfield = Instantiate(snowfield);
                currentVolume = Instantiate(volume_snow);
                break;
            case Weather.sand:
                ground.SetTexture("_BaseMap", sand);
                ground.SetTextureScale("_BaseMap", new Vector2(6f, 5f));
                ground.color = new Color(0.9f, 0.9f, 0.9f);
                walls.color = new Color(0.66f, 0.46f, 0.43f);
                currentVolume = Instantiate(volume_sand);
                break;
            case Weather.city:

                ground.SetTexture("_BaseMap", asphalt);
                ground.SetTextureScale("_BaseMap", new Vector2(1.2f, 1f));
                ground.color = new Color(0.69f, 0.69f, 0.69f);
                walls.color = new Color(0.98f, 0.91f, 0.32f);
                steel.color = new Color(0.89f, 0.89f, 0.89f);
                currentVolume = Instantiate(volume_city);
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        Destroy(currentSnowfield);
        Destroy(currentVolume);
    }
}
