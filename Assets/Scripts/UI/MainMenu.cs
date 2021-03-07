using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button onePlayer;
    [SerializeField] private Button twoPlayers;
    [SerializeField] private Button threePlayers;
    [SerializeField] private Button fourPlayers;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject _players;
    [SerializeField] private GameObject _difficult;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _title;
    [SerializeField] private Button fistSelected;

    [SerializeField] private Image blink;

    private float transparency;
    private AnimationCurve curve;
    private bool isMusic;
    private bool isSound;

    private void Start()
    {
        SoundManager.play.StopPlaySoundImmediately("Any");
        OpenDifficultPanel();
        _title.SetActive(true);
        transparency = blink.GetComponent<Image>().color.a;
        SoundManager.play.PlayContinuousAudio("MainSong", 0.3f);
    }

    public void BackFromSettings()
    {
        SoundManager.play.SetIsMusic(isMusic);
        SoundManager.play.SetIsSound(isSound);
        OpenDifficultPanel();
    }

    public void OpenDifficultPanel()
    {
        _players.SetActive(false);
        _settings.SetActive(false);
        _difficult.SetActive(true);
        _main.SetActive(true);
        // fistSelected.Select();
        _difficult.transform.GetChild(0).gameObject.GetComponent<Selectable>().Select();

        if(!SoundManager.play.GetIsMusic())
        {
            SoundManager.play.StopPlaySoundImmediately("Any");
        }
    }

    public void SetMusicOn()
    {
        var music = _settings.transform.Find("IsMusic").GetComponent<Toggle>();
        isMusic = music.isOn;
        Debug.Log("IsMusic: " + isMusic);
    }
    public void SetSoundOn()
    {
        var sound = _settings.transform.Find("IsSound").GetComponent<Toggle>();
        isSound = sound.isOn;
    }


    public void OpenSettingsMenu()
    {
        if (_players.activeSelf)
        {
            _players.SetActive(false);
        }
        if (_difficult.activeSelf)
        {
            _difficult.SetActive(false);
        }
        if (_main.activeSelf)
        {
            _main.SetActive(false);
        }
        _settings.SetActive(true);
        _settings.transform.GetChild(0).gameObject.GetComponent<Selectable>().Select();

        var _Music = SoundManager.play.GetIsMusic();
        var _Sound = SoundManager.play.GetIsSound();
        var music = _settings.transform.Find("IsMusic").GetComponent<Toggle>();
        var sound = _settings.transform.Find("IsSound").GetComponent<Toggle>();

        music.isOn = _Music;
        isMusic = _Music;
        sound.isOn = _Sound;
        isSound = _Sound;
    }


    public void SetDifficult(int dif)
    {
        switch (dif)
        {
            case 1:
                MasterController.difficult = Difficult.normal;
                break;
            case 2:
                MasterController.difficult = Difficult.hard;
                break;
        }

        _difficult.SetActive(false);
        _players.SetActive(true);
        _players.transform.GetChild(0).gameObject.GetComponent<Selectable>().Select();
        //onePlayer.Select();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Players(int players)
    {
        MasterController.totalPlayersInGame = players;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            blink.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }

        SceneManager.LoadScene("CharacterCreator");
    }

}
