using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class TranslatableDropdown : MonoBehaviour, I_Translatable
{
    [SerializeField] private List<string> keys;

    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }
    private void OnEnable()
    {
        AddToTranslatable();
        DoTranslation();
    }
    private void OnDestroy()
    {
        DeleteFromTranslatable();
    }

    public void AddToTranslatable()
    {
        Language.AddTranslatable(this);
    }

    public void DeleteFromTranslatable()
    {
        Language.DeleteTranslatable(this);
    }

    public void DoTranslation()
    {
        if (dropdown.options.Count != keys.Count)
        {
            Debug.LogWarning($"Different count of values for translating in {dropdown.name}");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            var _newText = Language.GetText(keys[i]);

            if (_newText == null)
            {
                Debug.LogWarning($"There is no key for {keys[i].ToString()} in Dropdown {dropdown.name}");
            }
            else
            {
                dropdown.options[i].text = _newText;
            }
            
        }
    }
}
