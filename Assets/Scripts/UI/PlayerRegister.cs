using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

public class PlayerRegister : MonoBehaviour, I_DontDestroy
{
    public string thisPlayer;

    private InputDevice _inputDevice;
    private string controllerName;

    
    private void Start()
    {
        GlobalWatcher.instance.AddDontDestroeble(this);
        switch (PrepareForGame.playerRegister.Count)
        {
            case 0:
                PrepareForGame.playerRegister.Add("Player_1"); thisPlayer = "Player_1";  AddController(1); Conteiner.tankControl.Add("Player_1", _inputDevice);
                break;
            case 1:
                PrepareForGame.playerRegister.Add("Player_2"); thisPlayer = "Player_2";  AddController(2); Conteiner.tankControl.Add("Player_2", _inputDevice);
                break;
            case 2:
                PrepareForGame.playerRegister.Add("Player_3"); thisPlayer = "Player_3"; AddController(3); Conteiner.tankControl.Add("Player_3", _inputDevice);
                break;
            case 3:
                PrepareForGame.playerRegister.Add("Player_4"); thisPlayer = "Player_4"; AddController(4); Conteiner.tankControl.Add("Player_4", _inputDevice);
                break;
            default:
                break;
        }
        DontDestroyOnLoad(this.gameObject);
    }

 
    private void AddController(int playerNumber)
    {
        var control = gameObject.GetComponent<MultiplayerEventSystem>();
        var playerPanel = control.playerRoot = GameObject.Find("Player" + playerNumber);
        playerPanel.transform.Find("ReadyPanel").gameObject.SetActive(false);
        var firstSelect = control.firstSelectedGameObject = GameObject.Find("NameDrop" + playerNumber);
        control.SetSelectedGameObject(firstSelect);

        gameObject.AddComponent<DeleteOnLoad>();

        var inputs = gameObject.GetComponent<PlayerInput>();
        var devices = inputs.devices;
        foreach (var item in devices)
        {
            _inputDevice = item;
            controllerName = item.name;
        }
    }

    public GameObject ReturnGameObject()
    {
        return this.gameObject;
    }
}
