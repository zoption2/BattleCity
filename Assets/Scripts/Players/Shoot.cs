using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shoot : MonoBehaviour, I_PlayerShoot
{
    protected GameObject player;
    public bool canShoot;
    public bool alive;
    protected Animator animator;
    protected Transform target;
    protected Rigidbody rb;
    protected float timer;
    protected List<GameObject> myBullets;
    protected SoundManager _soundManager;
    protected AudioSource _soundWeapon;
    protected AudioSource _soundMove;
    protected PlayersStats playersStats;
    protected LineRenderer lineRenderer;
    protected string _thisPlayer;
    protected float countdown = 0;
    protected string _shootSound;



    protected virtual void Start()
    {
        player = this.gameObject;
        _thisPlayer = player.tag;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        target = gameObject.transform.GetChild(1);
        myBullets = new List<GameObject>();
        playersStats = PlayersStats.Instance;
        canShoot = true;
        timer = 0f;

        _soundManager = SoundManager.play;
        _soundWeapon = transform.Find("SoundWeapon").GetComponent<AudioSource>();
        _soundMove = transform.Find("SoundMove").GetComponent<AudioSource>();
        _soundMove.clip = SoundManager.play.Audio("TankEngine");
        _shootSound = _soundManager.playerShoot();



        //accuracy achivement
        if (!Conteiner.accuracyAchive.ContainsKey(_thisPlayer))
        {
            Conteiner.accuracyAchive.Add(_thisPlayer, 0);
        }
    }

    private void Update()
    {
        Timer();
    }

    protected float Timer()
    {
        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;
        return timer;
    }

    protected string playerNumber
    {
        get
        {
            switch (_thisPlayer)
            {
                case "Player_1": return "1";
                case "Player_2": return "2";
                case "Player_3": return "3";
                case "Player_4": return "4";
                default: return "1";
            }
        }
    }

    public abstract void AutoShoot();
        
    

    protected int BulletListLength()
    {
        return myBullets.Count;
    }
    protected void SetMyBullet(GameObject bullet)
    {
        myBullets.Add(bullet);
        //var _bullet = bullet.GetComponent<Bullet>();
        //_bullet.playerShotter = this.gameObject;
        ////MultiBullets++;
    }
    public void DeleteMyBullet(GameObject bullet)
    {
        if (myBullets.Contains(bullet))
        {
            myBullets.Remove(bullet);
        }
        CheckActiveBullet();
        canShoot = true;
    }
    protected void CheckActiveBullet()
    {
        foreach (var item in myBullets)
        {
            if (!item.activeSelf) DeleteMyBullet(item);
        }
    }
}
