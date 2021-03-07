using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// I call SoundManager at next classes:
// EnemyAI
// Player
// Health
// GameplayManager
// RedWallDestroy

public class SoundManager : MonoBehaviour
{
    public static SoundManager play;
    private AudioSource _audioSource;
    private AudioSource _audioForOneShot;
    private Dictionary<string, AudioSource> audioStorage;
    private Dictionary<string, AudioClip> _audioDictionary;
    private static string currentMusicPlay;
    private bool alreadyAdded;
    private static bool isMusicOn = true;
    private static bool isSoundsOn = true;

    public bool IsMusicOn
    {
        get { return isMusicOn; }
        set { isMusicOn = value; }
    }
    public bool IsSoundOn
    {
        get { return isSoundsOn; }
        set { isSoundsOn = value; }
    }

    public void SetIsMusic(bool isMusic)
    {
        IsMusicOn = isMusic;
        switch (isMusic)
        {
            case true:
                PlayerPrefs.SetInt("IsMusic", 1);
                break;
            case false:
                PlayerPrefs.SetInt("IsMusic", 0);
                break;
        }
        PlayerPrefs.Save();
    }
    public bool GetIsMusic()
    {
        int binaryIsMusic = 1;
        if (PlayerPrefs.HasKey("IsMusic"))
        {
            binaryIsMusic = PlayerPrefs.GetInt("IsMusic");
        }
        switch (binaryIsMusic)
        {
            case 0: return false;
                break;
            case 1: return true;
                break;
            default: return true;
                break;
        }
    }

    public void SetIsSound(bool isSound)
    {
        IsSoundOn = isSound;
        switch (isSound)
        {
            case true:
                PlayerPrefs.SetInt("IsSound", 1);
                break;
            case false:
                PlayerPrefs.SetInt("IsSound", 0);
                break;
        }
        PlayerPrefs.Save();
    }
    public bool GetIsSound()
    {
        int binaryIsSound = 1;
        if (PlayerPrefs.HasKey("IsSound"))
        {
            binaryIsSound = PlayerPrefs.GetInt("IsSound");
        }
        switch (binaryIsSound)
        {
            case 0:
                return false;
                break;
            case 1:
                return true;
                break;
            default:
                return true;
                break;
        }
    }


    #region Singleton
    private void Awake()
    {
        if (play == null)
        {
            play = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        alreadyAdded = false;
    }
    #endregion

    //private void Start()
    //{
    //    AddSounds();
    //}
    private void OnEnable()
    {
        _audioDictionary = new Dictionary<string, AudioClip>();
        audioStorage = new Dictionary<string, AudioSource>();
        _audioForOneShot = gameObject.AddComponent<AudioSource>();
        if (alreadyAdded == false)
        {
            AddSounds();
        }
    }


    //return AudioClip by string name from dictionary
    public AudioClip Audio(string soundName)
    {
        var audio = _audioDictionary[soundName];
        return audio;
    }

    // play single Audioclip with specific AudioSource
    public void PlayShortAudio(string soundName, AudioSource audioSource, float volume = 1, bool isPitch = false)
    {
        if (!_audioDictionary.ContainsKey(soundName))
        {
            Debug.LogError(soundName + "does not found at you playlist.");
            return;
        }

        if (!IsSoundOn)
        {
            volume = 0;
        }
        AudioSource AS = audioSource;
        var newSound = _audioDictionary[soundName];
        if (isPitch)
        {
            float randomePitch = Random.Range(0.5f, 1.5f);
            AS.pitch = randomePitch;
        }
        AS.PlayOneShot(newSound, volume);
    }

    // play single Audioclip with standart AudioSource
    public void PlayShortAudio(string soundName, float volume = 1, bool isPitch = false)
    {
        if (!_audioDictionary.ContainsKey(soundName))
        {
            Debug.LogError(soundName + "does not found at you playlist.");
            return;
        }

        if (!IsSoundOn)
        {
            volume = 0;
        }
        AudioSource AS = _audioForOneShot;
        var newSound = _audioDictionary[soundName];
        if (isPitch)
        {
            float randomePitch = Random.Range(0.5f, 1.5f);
            AS.pitch = randomePitch;
        }
        AS.PlayOneShot(newSound, volume);
        AS.pitch = 1;
    }

    // create new Audiosource for play long music
    public void PlayContinuousAudio(string soundName, float volume = 1)
    {
        if (!_audioDictionary.ContainsKey(soundName))
        {
            Debug.LogError(soundName + "does not found at you playlist.");
            return;
        }

        if (!IsMusicOn)
        {
            volume = 0;
        }
        AudioSource source = gameObject.AddComponent<AudioSource>();
        audioStorage.Add(soundName, source);
        var newSound = _audioDictionary[soundName];
        currentMusicPlay = soundName;

        source.volume = volume;
        source.clip = newSound;
        source.Play();
    }

    //затухание играющего звука
    /// <summary>
    /// Stop playing continuous sound. Write "Any" for stop current music.
    /// </summary>
    /// <param name="songName"></param>
    /// <returns></returns>
    public void StopPlaySound(string songName)
    {
        StartCoroutine(stopPlaySound(songName));
    }

    /// <summary>
    /// Stop playing continuous music/sound immediately. Write "Any", if to stop any sound needed.
    /// </summary>
    /// <param name="songName"></param>
    public void StopPlaySoundImmediately(string songName)
    {
        if (songName == "Any") songName = currentMusicPlay;

        if (songName == null || !audioStorage.ContainsKey(songName))
        {
            Debug.LogError(songName + "doesn't playing now. Can't stop it.");
            return;
        }

        AudioSource AS = audioStorage[songName];
        Destroy(AS);
        audioStorage.Remove(songName);
    }

    /// <summary>
    /// Write "Any" if you want to stop current music.
    /// </summary>
    /// <param name="songName"></param>
    /// <returns></returns>
    private IEnumerator stopPlaySound(string songName )

    {
        if (songName == "Any") songName = currentMusicPlay;

        if (songName == null || !audioStorage.ContainsKey(songName))
        {
            Debug.LogError(songName + "doesn't playing now. Can't stop it.");
            yield return null;
        }

        AudioSource AS = audioStorage[songName];
        var firstVolume = AS.volume;
         while (firstVolume > 0)
         {
             AS.volume -= Time.deltaTime / 5;
             firstVolume = AS.volume;
             yield return null;
         }
         Destroy(AS);
         audioStorage.Remove(songName);
    }

    //sound of player's shooting
    public string playerShoot()
    {
        string[] shots = {"TankShoot_1", "TankShoot_2", "TankShoot_3", "TankShoot_4" };
        var _shot = shots[Random.Range(0, shots.Length - 1)];
        return _shot;
    }

    //sound of enemies shooting
    public string enemyShoot()
    {
        string[] shots = { "EnemyShoot_0", "EnemyShoot_1" };
        var _shot = shots[Random.Range(0, shots.Length - 1)];
        return _shot;
    }

    //sound of explosion
    public string explosionSound()
    {
        string[] explosive = { "Explosion_1", "Explosion_2", "Explosion_3" };
        var explosion = explosive[Random.Range(0, explosive.Length - 1)];
        return explosion;
    }

    //sound of hit tank
    //public string hitTankSound()
    //{
    //    string[] explosive = { "HitTank_1", "HitTank_2", "HitTank_3", "HitTank_4" };
    //    var explosion = explosive[Random.Range(0, explosive.Length - 1)];
    //    return explosion;
    //}

    //sound of hit bricks
    public string hitBricksSound()
    {
        string[] explosive = { "HitBricks_1" , "HitBricks_2" , "HitBricks_3" , "HitBricks_4" , "HitBricks_5" };
        var explosion = explosive[Random.Range(0, explosive.Length - 1)];
        return explosion;
    }
    
    //background gameplay music
    public string GameplaySound()
    {
        string[] Gameplay = { "GameMusic_1", "GameMusic_2", "GameMusic_3", "GameMusic_4", "GameMusic_5", "GameMusic_6" };
        var game = Gameplay[Random.Range(0, Gameplay.Length - 1)];
        return game;
    }

    //endgame music
    public string EndgameSound()
    {
        string[] Gameplay = { "EndMusic_1", "EndMusic_2"};
        var game = Gameplay[Random.Range(0, Gameplay.Length - 1)];
        return game;
    }
    private void AddSounds()
    {
        alreadyAdded = true;
        var TankShoot0 = Resources.Load("Sounds/TankShoot_0") as AudioClip;
        _audioDictionary.Add("TankShoot_0", TankShoot0);
        var TankShoot1 = Resources.Load("Sounds/TankShoot_1") as AudioClip;
        _audioDictionary.Add("TankShoot_1", TankShoot1);
        var TankShoot2 = Resources.Load("Sounds/TankShoot_2") as AudioClip;
        _audioDictionary.Add("TankShoot_2", TankShoot2);
        var TankShoot3 = Resources.Load("Sounds/TankShoot_3") as AudioClip;
        _audioDictionary.Add("TankShoot_3", TankShoot3);
        var TankShoot4 = Resources.Load("Sounds/TankShoot_4") as AudioClip;
        _audioDictionary.Add("TankShoot_4", TankShoot4);

        //var HitTank1 = Resources.Load("Sounds/TankShoot_1") as AudioClip;
        //_audioDictionary.Add("HitTank_1", HitTank1);
        //var HitTank2 = Resources.Load("Sounds/TankShoot_1") as AudioClip;
        //_audioDictionary.Add("HitTank_2", HitTank2);
        //var HitTank3 = Resources.Load("Sounds/TankShoot_1") as AudioClip;
        //_audioDictionary.Add("HitTank_3", HitTank3);
        //var HitTank4 = Resources.Load("Sounds/TankShoot_1") as AudioClip;
        //_audioDictionary.Add("HitTank_4", HitTank4);

        var HitBricks1 = Resources.Load("Sounds/HitBricks_1") as AudioClip;
        _audioDictionary.Add("Hit", HitBricks1);
        //var HitBricks2 = Resources.Load("Sounds/HitBricks_2") as AudioClip;
        //_audioDictionary.Add("HitBricks_2", HitBricks2);
        //var HitBricks3 = Resources.Load("Sounds/HitBricks_3") as AudioClip;
        //_audioDictionary.Add("HitBricks_3", HitBricks3);
        //var HitBricks4 = Resources.Load("Sounds/HitBricks_4") as AudioClip;
        //_audioDictionary.Add("HitBricks_4", HitBricks4);
        //var HitBricks5 = Resources.Load("Sounds/HitBricks_5") as AudioClip;
        //_audioDictionary.Add("HitBricks_5", HitBricks5);

        var Explosion1 = Resources.Load("Sounds/Explosion_1") as AudioClip;
        _audioDictionary.Add("Explosion_1", Explosion1);
        var Explosion2 = Resources.Load("Sounds/Explosion_2") as AudioClip;
        _audioDictionary.Add("Explosion_2", Explosion2);
        var Explosion3 = Resources.Load("Sounds/Explosion_3") as AudioClip;
        _audioDictionary.Add("Explosion_3", Explosion3);

        var EnemyShoot1 = Resources.Load("Sounds/EnemyShoot_0") as AudioClip;
        _audioDictionary.Add("EnemyShoot_0", EnemyShoot1);
        var EnemyShoot2 = Resources.Load("Sounds/EnemyShoot_0") as AudioClip;
        _audioDictionary.Add("EnemyShoot_1", EnemyShoot2);

        var TankEngine = Resources.Load("Sounds/TankRun") as AudioClip;
        _audioDictionary.Add("TankEngine", TankEngine);

        var MainSong = Resources.Load("Sounds/MainSong") as AudioClip;
        _audioDictionary.Add("MainSong", MainSong);

        var GameIntro = Resources.Load("Sounds/GameIntro") as AudioClip;
        _audioDictionary.Add("GameIntro", GameIntro);

        var GameMusic1 = Resources.Load("Sounds/GameMusic_1") as AudioClip;
        _audioDictionary.Add("GameMusic_1", GameMusic1);
        var GameMusic2 = Resources.Load("Sounds/GameMusic_2") as AudioClip;
        _audioDictionary.Add("GameMusic_2", GameMusic2);
        var GameMusic3 = Resources.Load("Sounds/GameMusic_3") as AudioClip;
        _audioDictionary.Add("GameMusic_3", GameMusic3);
        var GameMusic4 = Resources.Load("Sounds/GameMusic_4") as AudioClip;
        _audioDictionary.Add("GameMusic_4", GameMusic4);
        var GameMusic5 = Resources.Load("Sounds/GameMusic_5") as AudioClip;
        _audioDictionary.Add("GameMusic_5", GameMusic5);
        var GameMusic6 = Resources.Load("Sounds/GameMusic_6") as AudioClip;
        _audioDictionary.Add("GameMusic_6", GameMusic6);

        var EndMusic1 = Resources.Load("Sounds/EndMusic_1") as AudioClip;
        _audioDictionary.Add("EndMusic_1", EndMusic1);
        var EndMusic2 = Resources.Load("Sounds/EndMusic_2") as AudioClip;
        _audioDictionary.Add("EndMusic_2", EndMusic2);

        var BigLaser = Resources.Load("Sounds/BigLaser") as AudioClip;
        _audioDictionary.Add("BigLaser", BigLaser);

        var BigLaserCharge = Resources.Load("Sounds/BigLaserCharge") as AudioClip;
        _audioDictionary.Add("BigLaserCharge", BigLaserCharge);

        var DirectedBy = Resources.Load("Sounds/DirectedBy") as AudioClip;
        _audioDictionary.Add("DirectedBy", DirectedBy);

        var Astronomia = Resources.Load("Sounds/Astronomia") as AudioClip;
        _audioDictionary.Add("Astronomia", Astronomia);


    }
}
