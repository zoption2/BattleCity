using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Missile : MonoBehaviour
{
    [HideInInspector] public GameObject playerShotter;
    [HideInInspector] public string whoIsShooter;
    public float speed = 8;
    public bool isTurret = false;
    protected bool canDestroySteel = false;
    protected Rigidbody _rigidbody;
    protected Renderer _renderer;
    protected Transform _child;
    protected BoxCollider _collider;
    protected LineRenderer _lineRenderer;
    protected Texture playerYellow;
    protected Texture playerPurple;
    protected Texture playerRed;
    protected Texture playerBlue;
    protected SoundManager soundManager;
    protected VFXTotalSpawner vFX;
    protected bool isActive;
    protected bool needCheckTexture;

    private bool LevelStarted = false;
    private bool mayIDestroysteel { get { return StarsBoost.CanDestroySteel(whoIsShooter); } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponentInChildren<Renderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        soundManager = SoundManager.play;
        if (GetComponent<LineRenderer>() != null)
        {
            _lineRenderer = GetComponent<LineRenderer>();
        } 
        _child = transform.GetChild(0);
        playerYellow = Resources.Load("Textures/Missile/Yellow") as Texture;
        playerPurple = Resources.Load("Textures/Missile/Pink") as Texture;
        playerRed = Resources.Load("Textures/Missile/Red") as Texture;
        playerBlue = Resources.Load("Textures/Missile/Blue") as Texture;
        vFX = VFXTotalSpawner.Instance;

        if (!MasterController.whoIsShooter.ContainsKey(this.gameObject.GetInstanceID()))
        MasterController.whoIsShooter.Add(this.gameObject.GetInstanceID(), null);
    }
 
    public void SetShooter(GameObject playerShooter)
    {
        this.playerShotter = playerShooter;
        whoIsShooter = this.playerShotter.tag;
        isActive = true;
        canDestroySteel = mayIDestroysteel;
        CheckTextures();
    }

    public void SetTurretShooter(GameObject playerShooter, string turretOwner)
    {
        this.playerShotter = playerShooter;
        whoIsShooter = turretOwner;
        isActive = true;
        canDestroySteel = mayIDestroysteel;
        CheckTextures();
    }

    public void SetEnemyShooter(GameObject enemy)
    {
        this.playerShotter = enemy;
        whoIsShooter = playerShotter.name;
        isActive = true;
    }
    private void Start()
    {
        LevelStarted = true;
    }
    protected void OnDisable()
    {
        if (LevelStarted)
        DoHitEffects();
    }
    protected abstract void DoHitEffects();


    private void CheckTextures()
    {
        if (needCheckTexture)
        {
            switch (whoIsShooter)
            {
                case "Player_1":
                    _renderer.material.mainTexture = playerYellow;
                    break;
                case "Player_2":
                    _renderer.material.mainTexture = playerPurple;
                    break;
                case "Player_3":
                    _renderer.material.mainTexture = playerRed;
                    break;
                case "Player_4":
                    _renderer.material.mainTexture = playerBlue;
                    break;
                default:
                    break;
            }
        }
        else Debug.LogWarning("I cant detect who is shooter!");
    }
}
