using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class TranslatableParametric : MonoBehaviour, I_Translatable
{
    [SerializeField] private string key;
    [SerializeField] private List<string> parameters;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
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
        var _newText = Language.GetText(key);
        if (_newText == null)
        {
            Debug.LogError($"Translation by key does not exist for {gameObject}");
        }
        else
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                string str = "#" + i.ToString();
                _newText = _newText.Replace(str, parameters[i]);
                text.text = _newText;
            }
        }
    }
}
