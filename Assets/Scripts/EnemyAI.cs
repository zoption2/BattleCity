using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyLife { alive, freeze, shootPosition, dead };
public class EnemyAI : Movement
{
    public bool canShoot;
    public EnemyLife enemyLife;
    [SerializeField] private LayerMask blockingLayer;
    [SerializeField] private LayerMask bricksLayer;

    private Rigidbody rb;
    private Transform target;
    private Collider trigger;
    private float right, forward;
    private List<Direction> freeWays;
    private float timer;
    private float clock = 0.3f;

    private SoundManager _soundManager;
    private AudioSource _soundWeapon;
    private AudioSource _soundMove;
    enum Direction {Up, Down, Left, Right};
    private string enemyName;
    private string _shootSound;
    private Coroutine coroutine;

    private void Start()
    {
        this.gameObject.name = this.gameObject.name + Random.Range(1, 9999).ToString();
        enemyName = this.gameObject.name;
        trigger = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        target = gameObject.transform.GetChild(1);

        _soundManager = SoundManager.play;
        _soundWeapon = transform.Find("SoundWeapon").GetComponent<AudioSource>();
        _soundMove = transform.Find("SoundMove").GetComponent<AudioSource>();
        //_shootSound = SoundManager.enemyShoot();

        prepareTime();
        canShoot = true;
        timer = 0;
        RandomDirection();

        EnergyShield energyShield = gameObject.AddComponent<EnergyShield>();
        energyShield.StartImmortality();
    }
    private float prepareTime()
    {
        return Random.Range(0.5f, 1.5f);
    }

    private float Timer()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
        }
        return timer;
    }

    private void Update()
    {
        Timer();

        switch (enemyLife)
        {
            case EnemyLife.alive:
                Fire();
                ChooseDirection();
                break;
            case EnemyLife.shootPosition:
                Fire();
                break;
            case EnemyLife.freeze:
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                break;
            case EnemyLife.dead:
                if (this.gameObject.GetComponent<BuffDealer>() != null)
                {
                    this.gameObject.GetComponent<BuffDealer>().enabled = false;
                }
                canShoot = false;
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                break;
            default:
                break;
        }
    }



    private void ChooseDirection()
    {
        if (right != 0 && !isMoving)
        {
            MoveHorizontal(right, rb);
        }
        else if (forward != 0 && !isMoving)
        {
            MoveVertical(forward, rb);
        }
    }

    //private void RandomDirection()
    //{
    //    StartCoroutine(_randomDirection());
    //}
    private void RandomDirection()
    {
        CancelInvoke("RandomDirection");
        PathFind();
        this.rb.isKinematic = false;

        Direction selection = freeWays[Random.Range(0, freeWays.Count)];

        switch (selection)
        {
            case Direction.Up:
                forward = 1;
                right = 0;
                break;
            case Direction.Down:
                forward = -1;
                right = 0;
                break;
            case Direction.Left:
                forward = 0;
                right = -1;
                break;
            case Direction.Right:
                forward = 0;
                right = 1;
                break;
            default:
                break;
        }

        Invoke("RandomDirection", Random.Range(0.5f, 7));
    }

    private IEnumerator CollisionPause()
    {
        enemyLife = EnemyLife.alive;
        yield return new WaitForSeconds(0.1f);
        this.rb.isKinematic = false;
        RandomDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enemyLife == EnemyLife.alive)
        {
            if (collision.gameObject.layer == 14 || collision.gameObject.layer == 15)
            {
                return;
            }

            this.rb.isKinematic = true;
            string wall = collision.gameObject.tag;
            if (wall == "RedWall")
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(checkBricks());
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(CollisionPause());
                //RandomDirection();
            }
        }
       
        
    }

    private IEnumerator checkBricks()
    {
        enemyLife = EnemyLife.shootPosition;
        yield return new WaitForSeconds(1f);
        enemyLife = EnemyLife.alive;
        RandomDirection();
    }

    private void PathFind()
    {
        freeWays = new List<Direction>();

        if (!Physics.BoxCast(transform.position, new Vector3(1f, 0.4f, 0.1f), new Vector3(1, 0, 0), transform.rotation, 2f, blockingLayer))
            freeWays.Add(Direction.Right);
        if (!Physics.BoxCast(transform.position, new Vector3(1f, 0.4f, 0.1f), new Vector3(-1, 0, 0), transform.rotation, 2f, blockingLayer))
            freeWays.Add(Direction.Left);
        if (!Physics.BoxCast(transform.position, new Vector3(1f, 0.4f, 0.1f), new Vector3(0, 0, 1), transform.rotation, 2f, blockingLayer))
            freeWays.Add(Direction.Up);
        if (!Physics.BoxCast(transform.position, new Vector3(0.9f, 0.4f, 0.9f), new Vector3(0, 0, -1), transform.rotation, 2f, blockingLayer))
            freeWays.Add(Direction.Down);
    }

    private void Fire()
    {
        GameObject missile;
        
        if (Timer() == 0 && canShoot)
        {
            missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("MissileEnemy", target.transform.position, Quaternion.LookRotation(transform.forward));
            canShoot = false;
            timer = prepareTime();
            missile.GetComponent<EnemyBullet>().SetEnemyShooter(gameObject);

            var shotVFX = TotalSpawner.spawn.SpawnFromSpawner("ShotVFX", target.transform.position, Quaternion.LookRotation(transform.forward));
            shotVFX.GetComponent<TailVFX>().LifeOfTail();

           _soundManager.PlayShortAudio(_soundManager.enemyShoot(), _soundWeapon, 0.15f, true);
        }
    }

    private void FireImmediately()
    {
        GameObject missile;
        missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("MissileEnemy", target.transform.position, Quaternion.LookRotation(transform.forward));
        missile.GetComponent<EnemyBullet>().SetEnemyShooter(gameObject);

        var shotVFX = TotalSpawner.spawn.SpawnFromSpawner("ShotVFX", target.transform.position, Quaternion.LookRotation(transform.forward));
        shotVFX.GetComponent<TailVFX>().LifeOfTail();

        _soundManager.PlayShortAudio(_soundManager.enemyShoot(), _soundWeapon, 0.15f, true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Respawn>() != null)
        {
            // other.GetComponent<Respawn>().canRespNow = false;
            other.GetComponent<Respawn>().AddTankAtPoint(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Respawn>() != null)
        {
            //other.GetComponent<Respawn>().canRespNow = true;
            other.GetComponent<Respawn>().DeleteTankFromPoint(this.gameObject);
        }
    }
}
