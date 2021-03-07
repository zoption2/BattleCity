using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankColor : MonoBehaviour
{
    private Texture _oneHealth;
    private Texture _twoHealth;
    private Texture _threeHealth;
    private Texture _fourHealth;
    private Health health;
    private Renderer [] _renderer;

    private void Start()
    {
        _oneHealth = Resources.Load("Textures/Tanks/Enemies/1") as Texture;
        _twoHealth = Resources.Load("Textures/Tanks/Enemies/2") as Texture;
        _threeHealth = Resources.Load("Textures/Tanks/Enemies/3") as Texture;
        _fourHealth = Resources.Load("Textures/Tanks/Enemies/4") as Texture;
        _renderer = GetComponentsInChildren<Renderer>();
        health = GetComponent<Health>();

        CheckTexture(health.GetTotalHealth());
    }

    public void CheckTexture(float health)
    {
        foreach (var item in _renderer)
        {
            switch (health)
            {
                case 1:  item.material.mainTexture = _oneHealth;
                    break;
                case 2:  item.material.mainTexture = _twoHealth;
                    break;
                case 3:  item.material.mainTexture = _threeHealth;
                    break;
                case 4:  item.material.mainTexture = _fourHealth;
                    break;
                default:
                    break;
            }
        }

    }
}
