using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public interface I_Player
{

}

public abstract class Player : Movement
{
    public bool canShoot;
    public bool alive;
    protected Animator animator;
    protected Transform target;
    protected Rigidbody rb;
    protected float right, forward;
    protected float timer;
    protected float demping, originalSpeed, currentSpeed;
    protected I_PlayerSpecific spec;
    protected I_PlayerShoot shoot;
    protected I_Inputs inputs;
    protected List<GameObject> myBullets;
    protected SoundManager _soundManager;
    protected AudioSource _soundWeapon;
    protected AudioSource _soundMove;
    protected PlayersStats playersStats;
    protected VFXTotalSpawner vfXSpawner;
    protected string _thisPlayer;
    protected const float SPEED_BOOST_COUNT = 0.7f; 
   
    private float countdown = 0;
    private string _shootSound;
    private LayerMask playerLayer;
    public UnityAction<int> OnSpeedBoostChange;

    private void DoSpeedBoostChange()
    {
        OnSpeedBoostChange?.Invoke(SpeedBoost);
    }

    public int SpeedBoost
    {
        get
        {
            return MasterController.speedBoosts[_thisPlayer];
        }
        set
        {
            var newValue = MasterController.speedBoosts[_thisPlayer] + value;
            var analize = Mathf.Clamp(newValue, 0, 3);
            if (MasterController.speedBoosts[_thisPlayer] != analize)
            {
                MasterController.speedBoosts[_thisPlayer] = analize;
                var newSpeed = originalSpeed + SPEED_BOOST_COUNT;
                OverrideOriginalSpeed(newSpeed);
                DoSpeedBoostChange();
            }

        }
    }

    private void Start()
    {
        _thisPlayer = this.gameObject.tag;
        AddType();
        AddTexture();
        CheckControllers();
        spec = GetComponent<I_PlayerSpecific>();
        shoot = GetComponent<I_PlayerShoot>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        target = gameObject.transform.GetChild(1);
        transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        myBullets = new List<GameObject>();
        playersStats = PlayersStats.Instance;
        alive = true;
        canShoot = true;
        isMoving = false;
        timer = 0f;
        playerLayer = LayerMask.NameToLayer("Player");
        vfXSpawner = VFXTotalSpawner.Instance;
        MasterController.activePlayers.Add(_thisPlayer, this.gameObject);



        if (MasterController.playerSpeed[_thisPlayer] > speed)
        {
            speed = MasterController.playerSpeed[_thisPlayer];
        }
        else
        {
            MasterController.playerSpeed[_thisPlayer] = speed;
        }

        originalSpeed = speed;
        demping = speed / 2;
        currentSpeed = originalSpeed;
        SpeedBoost = 0;


        _soundManager = SoundManager.play;
        _soundWeapon = transform.Find("SoundWeapon").GetComponent<AudioSource>();
        _soundMove = transform.Find("SoundMove").GetComponent<AudioSource>();
        _shootSound = _soundManager.playerShoot();

        playersStats.OnStarsChanged += StarModifier;

        StarModifier(_thisPlayer);
        EnergyShield energyShield = gameObject.AddComponent<EnergyShield>();
        energyShield.StartImmortality();

        //accuracy achivement
        if (!Conteiner.accuracyAchive.ContainsKey(_thisPlayer))
        {
            Conteiner.accuracyAchive.Add(_thisPlayer, 0);
        }
    }

    public void SetSpeed(bool isSpeed, float speedMidifier = 1)
    {
        if (isSpeed)
        {
            currentSpeed = originalSpeed * speedMidifier;
            speed = currentSpeed;
        }
        else
        {
            currentSpeed = originalSpeed;
            speed = currentSpeed;
        }
        
    }

    public void OverrideOriginalSpeed(float newSpeed)
    {
        originalSpeed = newSpeed;
        speed = originalSpeed;
        demping = speed / 2;
        currentSpeed = originalSpeed;
        MasterController.playerSpeed[_thisPlayer] = newSpeed;
    }

    protected void OnDestroy()
    {
        playersStats.OnStarsChanged -= StarModifier;
        MasterController.activePlayers.Remove(_thisPlayer);
    }
    #region AddInputParams
    private void AddType()
    {
        switch (Conteiner.tankType[_thisPlayer])
        {
            case 1:
                this.gameObject.AddComponent<PlayerAttacker>();
                this.gameObject.AddComponent<ShootBullet>();
                break;
            case 2:
                this.gameObject.AddComponent<PlayerDefender>();
                this.gameObject.AddComponent<ShootBullet>();
                break;
            case 3:
                this.gameObject.AddComponent<PlayerBuffer>();
                this.gameObject.AddComponent<ShootBullet>();
                break;
            case 4:
                this.gameObject.AddComponent<PlayerMelee>();
                //this.gameObject.AddComponent<ShootBullet>();
                //test
                this.gameObject.AddComponent<ShootRiffle>();
                break;
            case 5:
                this.gameObject.AddComponent<PlayerSupport>();
                this.gameObject.AddComponent<ShootBullet>();
                break;
            default:
                Debug.LogError("Can't add type to " + _thisPlayer);
                break;
        }
    }

    private void AddTexture()
    {
        var render = transform.GetComponentsInChildren<Renderer>();
        var texture = Conteiner.tankTexture[_thisPlayer];
        foreach (var item in render)
        {
            item.material.SetTexture("_BaseMap", texture);
        }
    }

    protected void CheckControllers()
    {
        // List<GameObject> eventControllers = new List<GameObject>();
        var allControllersInGame = GameObject.FindObjectsOfType<PlayerRegister>();
        foreach (var item in allControllersInGame)
        {
            if (item.thisPlayer == this.gameObject.tag)
            {
                item.gameObject.GetComponent<PlayerInput>().defaultActionMap = "Tank";
                item.gameObject.GetComponent<MultiplayerEventSystem>().playerRoot = this.gameObject;
                item.gameObject.GetComponent<MultiplayerEventSystem>().firstSelectedGameObject = this.gameObject;
            }
            else Debug.Log("WTF, cant find controller for " + this.gameObject.tag);
        }
    }
    #endregion


    //меняем башню танка согласно количеству звёзд
    public void StarModifier(string name)
    {
        if (name == _thisPlayer)
        {
            target.gameObject.SetActive(false);
            int stars = MasterController.playerStars[name];
            target = gameObject.transform.GetChild(stars + 1);
            target.gameObject.SetActive(true);
            AddTexture();
        }
    }

    public void MoveUp()
    {
        if (!isMoving)
        {
            MoveVertical(1, rb);
            MoveOnGraund();
        }
    }

    public void MoveDown()
    {
        if (!isMoving)
        {
            MoveVertical(-1, rb);
            MoveOnGraund();
        }
    }

    public void MoveRight()
    {
        if (!isMoving)
        {
            MoveHorizontal(1, rb);
            MoveOnGraund();
        }
    }

    public void MoveLeft()
    {
        if (!isMoving)
        {
            MoveHorizontal(-1, rb);
            MoveOnGraund();
        }
    }


    protected void MoveOnGraund()
    {
        if (Mathf.Abs(right) > 0 || Mathf.Abs(forward) > 0)
        {
            animator.SetBool("isRunAnimation", true);
        }
        else
        {
            animator.SetBool("isRunAnimation", false);
        } 
    }

    //protected float Timer()
    //{
    //    timer -= Time.deltaTime;
    //    if (timer < 0) timer = 0;
    //    return timer;
    //}

    public abstract void Shoot();
    public abstract void UseFirstSkill();
    public abstract void UseSecondSkill();
    public abstract void UseThirdSkill();

    public void UseBoosterUI()
    {
        MasterController.isPlayerBoosterUI[_thisPlayer] = !MasterController.isPlayerBoosterUI[_thisPlayer];
    }

    public void UsePause()
    {
        PauseMenu.instance.Pause();
    }


    protected void OnTriggerEnter(Collider collider)
    {

        collider.TryGetComponent<Ressurection>(out Ressurection ressurection);
        if (ressurection != null && !MasterController.isPlayerAlive[ressurection._playerName] && MasterController.playerLives[_thisPlayer] > 0 && ressurection._playerNumber <= MasterController.totalPlayersInGame)
        {
            ressurection.DoSpawning(_thisPlayer);
        }

        if (collider.GetComponent<Respawn>() != null)
        {
            collider.GetComponent<Respawn>().AddTankAtPoint(this.gameObject);
        }
    }
    protected void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Swamp")
        {
            speed = currentSpeed;
        }

        if (collider.GetComponent<Ressurection>() != null)
        {
            //countdown = 0;
            //collider.GetComponent<Ressurection>().SetRespawn();
            collider.GetComponent<Ressurection>().AbortSpawning();
        }

        if (collider.GetComponent<Respawn>() != null)
        {
            collider.GetComponent<Respawn>().DeleteTankFromPoint(this.gameObject);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Swamp")
        {
            if (MasterController.buffAnchorActive[_thisPlayer])
            {
                return;
            }
            else
            {
                demping = currentSpeed / 2;
                speed = demping;
            }
        }


        // var resurrection = other.GetComponent<Ressurection>(); 

        //if (resurrection != null && !MasterController.isPlayerAlive[resurrection._playerName] && MasterController.playerLives[_thisPlayer] > 0 && resurrection._playerNumber <= MasterController.totalPlayersInGame)
        //{
        //    countdown += Time.fixedDeltaTime;
        //    resurrection.Respawn(countdown);
        //    if (countdown > resurrection.timeForRes)
        //    {
        //        resurrection.ResurrectPlayer(_thisPlayer);
        //        resurrection.SetWarning(false);
        //        resurrection.SetRespawn();
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (MasterController.stageClear)
        {
            if (collision.gameObject.layer == 14)
            {
                vfXSpawner.PlayEffect("MakeLove", transform.position, Quaternion.identity, 1.5f, transform);
            }
        }
    }
}
