using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Tank", menuName = "Tanks", order = 51)]
public class Conteiner : ScriptableObject
{
    public static Dictionary<string, string> tankName;
    public static Dictionary<string, int> tankType;
    public static Dictionary<string, Texture> tankTexture;
    public static Dictionary<string, InputDevice> tankControl;
   
    //achives
    public static Dictionary<string, int> starsAssist;
    public static Dictionary<string, int> accuracyAchive;

    [HideInInspector] public static Conteiner Instance;

    public static void ReloudConteiner()
    {
        tankName = new Dictionary<string, string>();
        tankType = new Dictionary<string, int>();
        tankTexture = new Dictionary<string, Texture>();
        tankControl = new Dictionary<string, InputDevice>();
        starsAssist = new Dictionary<string, int>();
        accuracyAchive = new Dictionary<string, int>();
    }
}
