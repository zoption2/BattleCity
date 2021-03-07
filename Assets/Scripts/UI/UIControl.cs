using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Dropdown dropdownColor;
    [SerializeField] private Dropdown dropdownName;
    [SerializeField] private Dropdown dropdownType;
    [SerializeField] private Button playerReady;
    [SerializeField] private int playerNumber;
    [SerializeField] private Image typeImage;
    [SerializeField] private Button addName;
    [SerializeField] private InputField inputName;
    [SerializeField] private Transform nameDropSize;
    [SerializeField] private Transform colorDropSize;

    private Texture yellowT, redT, liteGreenT, aquaT, purpleT, deepBlueT, limeT, whiteT, blackT, pinkT, militaryBlue, militaryGreen, militaryRed;
    private Sprite attack, defend, assist, melee, esquire;
    private Texture textureFinal;
    private GameObject spikes;
    private string nameFinal;
    private int typeNumber;
    private Image background;
    private bool areYouReady;
    private bool nameReady = false;
    private bool typeReady = false;
    private bool colorReady = false;
    [SerializeField] public List<string> PlayersNames;

 
    private void ImportNames()
    {
        var path = Application.streamingAssetsPath + "/Names.txt";

        var data = File.ReadAllText(path);
        var names = JsonUtility.FromJson<Names>(data);
        PlayersNames = names.names;

        dropdownName.AddOptions(PlayersNames);
        var size = nameDropSize.GetComponent<RectTransform>();
        size.sizeDelta = new Vector2(size.sizeDelta.x, 40 * PlayersNames.Count);
        
    }

    private void ShowAllColors()
    {
        var size = colorDropSize.GetComponent<RectTransform>();
        var count = dropdownColor.options.Count;
        size.sizeDelta = new Vector2(size.sizeDelta.x, 40 * count);
    }
   
    
    [System.Serializable]
    private class Names
    {
        public List<string> names = new List<string>();
    }

    private void Start()
    {
        loadTextures();
        background = GetComponent<Image>();
        areYouReady = false;
        dropdownName.Select();
        PrepareForGame.AddPlayer(playerNumber, false);
        ImportNames();
        ShowAllColors();
    }

    public void InputNewName()
    {
        inputName.gameObject.SetActive(true);
        inputName.ActivateInputField();
    }
    public void AddNewName(string name)
    {
        var path = Application.streamingAssetsPath + "/Names.txt";

        Names names = new Names();
        names.names = PlayersNames;
        names.names.Add(name);
        var newNames = JsonUtility.ToJson(names);
        File.WriteAllText(path, newNames);

        dropdownName.ClearOptions();
        inputName.gameObject.SetActive(false);
        ImportNames();
        dropdownName.Select();
    }

    public void DeleteName()
    {
        nameReady = false;
        var path = Application.streamingAssetsPath + "/Names.txt";

        PlayersNames.Remove(nameFinal);
        Names names = new Names();
        names.names = PlayersNames;
        var newNames = JsonUtility.ToJson(names);
        File.WriteAllText(path, newNames);

        dropdownName.ClearOptions();
        ImportNames();
        dropdownName.Select();
    }

    private void loadTextures()
    {
        yellowT = Resources.Load("Textures/Tanks/Player/Tank_Color_Yellow") as Texture;
        redT = Resources.Load("Textures/Tanks/Player/Tank_Color_Red") as Texture;
        liteGreenT = Resources.Load("Textures/Tanks/Player/Tank_Color_LiteGreen") as Texture;
        aquaT = Resources.Load("Textures/Tanks/Player/Tank_Color_Aqua") as Texture;
        purpleT = Resources.Load("Textures/Tanks/Player/Tank_Color_Violet") as Texture;
        deepBlueT = Resources.Load("Textures/Tanks/Player/Tank_Color_Blue") as Texture;
        limeT = Resources.Load("Textures/Tanks/Player/Tank_Color_Lime") as Texture;
        whiteT = Resources.Load("Textures/Tanks/Player/Tank_Color_White") as Texture;
        blackT = Resources.Load("Textures/Tanks/Player/Tank_Color_Black") as Texture;
        pinkT = Resources.Load("Textures/Tanks/Player/Tank_Color_Pink") as Texture;

        militaryBlue = Resources.Load("Textures/Tanks/Player/Tank_Color_Military_3") as Texture;
        militaryGreen = Resources.Load("Textures/Tanks/Player/Tank_Color_Military_2") as Texture;
        militaryRed = Resources.Load("Textures/Tanks/Player/Tank_Color_Military_1") as Texture;

        attack = Resources.Load("Textures/UI/Attacker", typeof(Sprite)) as Sprite;
        defend = Resources.Load("Textures/UI/Defender", typeof(Sprite)) as Sprite;
        assist = Resources.Load("Textures/UI/Buffer", typeof(Sprite)) as Sprite;
        melee = Resources.Load("Textures/UI/Melee", typeof(Sprite)) as Sprite;
        esquire = Resources.Load("Textures/UI/Esquire", typeof(Sprite)) as Sprite;
    }

    public void SetTextures()
    {
        var integer = dropdownColor.value;
        spikes?.SetActive(false);
        var allTextures = player.GetComponentsInChildren<Renderer>();

        
        foreach (var item in allTextures)
        {
            item.material.SetTexture("_BaseMap", newTexture(integer));
        }

        if (typeNumber == 4)
        {
            spikes?.SetActive(true);
        }

        textureFinal = newTexture(integer);
        Conteiner.tankTexture[playerName()] = textureFinal;
        colorReady = true;
    }

    public void SetName()
    {
        var name = dropdownName.value;
        nameFinal = newName(name);
        Conteiner.tankName[playerName()] = nameFinal;
        nameReady = true;
    }

    public void SetType()
    {
        typeNumber = dropdownType.value; //0 - null, 1 - attacker, 2 - defender, 3 - buffer, 4 - Juggernault, 5 - Esquire
        if (typeNumber == 4)
        {
            AddSpikesToTank(true);
        }
        else AddSpikesToTank(false);

        Conteiner.tankType[playerName()] = typeNumber;
        SetTypeImage(playerName());
        typeReady = true;
    }

    //отобразить модель шипов на Джаггернауте
    private void AddSpikesToTank(bool needAdd)
    {
        var conteinerSpikes = PrefabsConteiner.instance.spikes;
        if (needAdd && spikes == null)
        {
            spikes = Instantiate(conteinerSpikes, player.transform);
            //spikes.transform.rotation = Quaternion.LookRotation(player.transform.forward);
            //spikes.transform.position = player.transform.position;
        }
        else if (needAdd && spikes !=null)
        {
            spikes.SetActive(true);
        }
        else
        {
            spikes?.SetActive(false);
        }

    }


    private void SetTypeImage(string playerName)
    {
        typeImage.enabled = true;
        var image = typeImage.GetComponent<Image>();
        var _image = GetTypeImage(playerName);
        image.sprite = _image;
    }

    private Sprite GetTypeImage(string playerName)
    {
        var player = Conteiner.tankType[playerName];
        switch (player)
        {
            case 1: return attack;
            case 2: return defend;
            case 3: return assist;
            case 4: return melee;
            case 5: return esquire;
            default: return null;
        }
    }
    public void isReady()
    {
        if (areYouReady)
        {
            areYouReady = !areYouReady;
            Unready();
        }
        else if (nameReady && typeReady && colorReady)
        {
            areYouReady = !areYouReady;
            Ready();
        }
    }
    private void Ready()
    {
        var baseColor = new Color32(38, 175, 38, 255);
        background.color = baseColor;
        dropdownColor.interactable = false;
        dropdownName.interactable = false;
        dropdownType.interactable = false;
        PrepareForGame.allPlayers_Ready[playerNumber] = true;
        PrepareForGame.AllPlayers_ready();
    }

    private void Unready()
    {
        background.color = new Color32(38, 38, 38, 255);
        dropdownColor.interactable = true;
        dropdownName.interactable = true;
        dropdownType.interactable = true;
        PrepareForGame.allPlayers_Ready[playerNumber] = false;
    }

    private Texture newTexture (int value)
    {
        switch (value)
        {
            case 0: return null;
            case 1: return redT;
            case 2: return purpleT;
            case 3: return aquaT;
            case 4: return yellowT;
            case 5: return liteGreenT;
            case 6: return limeT;
            case 7: return deepBlueT;
            case 8: return whiteT;
            case 9: return blackT;
            case 10: return pinkT;
            case 11: return militaryBlue;
            case 12: return militaryGreen;
            case 13: return militaryRed;
            default: return null;      
        }
    }

    private string newName (int value)
    {
        var name = PlayersNames[value];
        return name;
        //switch (value)
        //{
        //    case 0: return null;
        //    case 1: return "Наташа";
        //    case 2: return "Вика";
        //    case 3: return "Саша";
        //    case 4: return "Виталик";
        //    case 5: return "Сергей";
        //    case 6: return "Антон";

        //    default: return "Незнакомец";
        //}
    }

    private string playerName()
    {
        switch (playerNumber)
        {
            case 1: return "Player_1";
            case 2: return "Player_2";
            case 3: return "Player_3";
            case 4: return "Player_4";
            default: return null;
        }
    }
}
