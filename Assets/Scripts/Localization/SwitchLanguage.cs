using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class SwitchLanguage : MonoBehaviour
{
    private Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        StartedLanguage();
    }

    public void SetLanguage()
    {
        var number = dropdown.value;
        switch (number)
        {
            case 0:
                Language.instance.SetLanguage(Language.Lingua.English);
                break;
            case 1:
                Language.instance.SetLanguage(Language.Lingua.Russian);
                break;
            case 2:
                Language.instance.SetLanguage(Language.Lingua.Ukrainian);
                break;
            default:
                break;
        }
    }

    private void StartedLanguage()
    {
        var lang = Language.CurrentLanguage;
        switch (lang)
        {
            case "English": dropdown.value = 0;
                break;
            case "Russian": dropdown.value = 1;
                break;
            case "Ukrainian": dropdown.value = 2;
                break;
            default: 
                break;
        }
    }

}
