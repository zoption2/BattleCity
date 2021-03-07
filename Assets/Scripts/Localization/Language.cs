using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Language : MonoBehaviour
{
    public enum Lingua {Russian = 0, English = 1, Ukrainian = 2 };
    public static Lingua lingua;

    public static Language instance = null;

    public static string CurrentLanguage;
    public static List<I_Translatable> translatables = new List<I_Translatable>();
    
    private Dictionary<string, string> _languages = new Dictionary<string, string>();

    private const string DEFAULT_LANGUAGE = "English";
    private string rus = "Russian";
    private string eng = "English";
    private string ukr = "Ukrainian";
    private string path
    {
        get
        {
            var _path = Application.streamingAssetsPath + "/Localization/";
            return _path;
        }
    }

    //Current using language
    private static Dictionary<string, string> currentLanguageDictionary = new Dictionary<string, string>();

    //Collection of all possible languages
    private static Dictionary<string, Dictionary<string, string>> library = new Dictionary<string, Dictionary<string, string>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CheckForLanguages();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (currentLanguageDictionary != null)
        {
            DoLanguageChange();
        }
    }

    private void CheckForLanguages()
    {
        List<string> _lang = new List<string>();
        _lang.Add(rus);
        _lang.Add(eng);
        _lang.Add(ukr);

        foreach (var item in _lang)
        {
            var text = File.ReadAllText(path + item + ".txt");
            _languages.Add(item, text);
            FillNewLanguage(item);
        }

        if (PlayerPrefs.HasKey("Language"))
        {
            CurrentLanguage = PlayerPrefs.GetString("Language");
        }
        else
        {
            CurrentLanguage = DEFAULT_LANGUAGE;
        }
        currentLanguageDictionary = library[CurrentLanguage];
    }

    private void FillNewLanguage(string language)
    {
        var das = _languages[language].Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        var newDictionary = new Dictionary<string, string>();

        for (int i = 0; i < das.Length; i++)
        {
            //foreach (var item in das)
            //{
            var key = das[i].Split(new string[] { "\t" }, System.StringSplitOptions.None)[0];
            var value = das[i].Split(new string[] { "\t" }, System.StringSplitOptions.None)[1];
            value = value.Replace("//", " ");
            value = value.Replace("/n", "\n");
            newDictionary.Add(key, value);
        }

        library.Add(language, newDictionary);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        SetLanguage(Lingua.Russian);
    //    }
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        SetLanguage(Lingua.English);
    //    }
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        SetLanguage(Lingua.Ukrainian);
    //    }
    //}

    public void SetLanguage(Lingua lingua)
    {
        CurrentLanguage = lingua.ToString();
        currentLanguageDictionary = library[CurrentLanguage];
        DoLanguageChange();
        PlayerPrefs.SetString("Language", CurrentLanguage);
        PlayerPrefs.Save();
    }
    //public void SetEnglishLanguage()
    //{
    //    lingua = Lingua.English;
    //    CurrentLanguage = lingua.ToString();

    //    PlayerPrefs.SetString("Language", CurrentLanguage);
    //    PlayerPrefs.Save();
    //}
    //public void SetUkrainianLanguage()
    //{
    //    lingua = Lingua.Ukrainian;
    //    CurrentLanguage = lingua.ToString();

    //    PlayerPrefs.SetString("Language", CurrentLanguage);
    //    PlayerPrefs.Save();
    //}

    /// <summary>
    /// Get translation based on key.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetText(string index)
    {
        var backText = currentLanguageDictionary[index];
        return backText;
    }

    /// <summary>
    /// Registr object to be translate. 
    /// </summary>
    /// <param name="name"></param>
    public static void AddTranslatable(I_Translatable name)
    {
        if (!translatables.Contains(name))
        translatables.Add(name);
    }

    /// <summary>
    /// Delete object from registr to be translate.
    /// </summary>
    /// <param name="name"></param>
    public static void DeleteTranslatable(I_Translatable name)
    {
        if (translatables.Contains(name))
        translatables.Remove(name);
    }

    /// <summary>
    /// Do translation in all registred objects.
    /// </summary>
    private void DoLanguageChange()
    {
        for (int i = 0; i < translatables.Count; i++)
        {
            translatables[i].DoTranslation();
        }
    }

}
