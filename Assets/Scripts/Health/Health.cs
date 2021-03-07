using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float totalHealth = 1;

    private string WhoIsKiller;
    private string thisObject;
    private bool isShealded = false;
    private float currentHealth;
    //private Animator animator;
    private TankColor tankColor;
    private GameplayManager GPM;
    private SoundManager soundManager;
    private PlayersStats playersStats;
    private Death death;
    private LevelManager levelManager;

    public UnityAction OnDeath;
    public bool IsBigGun = false; // set "True", if you want destroy SteelWall anyway;
    public bool isSelfDestroy = false;

    public void SetTotalHealth(string name)
    {
        if (name == thisObject)
        {
            var health = playersStats.GetHealth(name);
            if (health != totalHealth)
                totalHealth = health;
            SetCurrentHealth(health);
        }
    }

    private void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }
    public void DoOnDeathAction()
    {
        OnDeath?.Invoke();
    }
    private void Awake()
    {
        currentHealth = totalHealth;
    }
    private void Start()
    {
        thisObject = this.gameObject.tag;

        if (GetComponent<TankColor>() != null)
        {
            tankColor = GetComponent<TankColor>();
        }

        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();

        soundManager = SoundManager.play;
        playersStats = PlayersStats.Instance;
        levelManager = LevelManager.Instance;

        if (thisObject == "Player_1" || thisObject == "Player_2" || thisObject == "Player_3" || thisObject == "Player_4")
        {
            playersStats.OnHealthChange += SetTotalHealth;
            SetTotalHealth(thisObject);
        }

    }

    private void OnDestroy()
    {
        if (thisObject == "Player_1" || thisObject == "Player_2" || thisObject == "Player_3" || thisObject == "Player_4")
            playersStats.OnHealthChange -= SetTotalHealth;
    }

    private bool mayKillerDestroySteel 
    {
        get
        { 
            return StarsBoost.CanDestroySteel(WhoIsKiller);
        } 
    }

    private void SetTextureToEnemy(float health)
    {
        if (tankColor != null)
        {
            tankColor.CheckTexture(health);
        }
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }

    public void SetKillerName(string whoIsShooter)
    {
        WhoIsKiller = whoIsShooter;
    }
    public string GetKillerName()
    {
        if (WhoIsKiller != null)
            return WhoIsKiller;
        else return null;
    }

    public void TakeDamage(float dmg)
    {
        //if (Conteiner.accuracyAchive.ContainsKey(WhoIsKiller))
        //{
        //    Conteiner.accuracyAchive[WhoIsKiller]++;
        //}

        if (thisObject == "SteelWall")
        {
            if (!mayKillerDestroySteel && !IsBigGun)
            {
                return;
            }
        }

        if (isShealded) return;

        currentHealth -= dmg;

        if (thisObject == "Player_1" || thisObject == "Player_2" || thisObject == "Player_3" || thisObject == "Player_4")
        {
            playersStats.SetHealth(thisObject, -1f);
        }

        if (currentHealth <= 0)
        {
            DoOnDeathAction();
        }
        SetTextureToEnemy(currentHealth);
        
    }
    public void SetMortality()
    {
        //currentHealth = totalHealth;
        isShealded = false;
    }
    public void SetImmortality()
    {
        //currentHealth = 1000;
        isShealded = true;
    }
}
