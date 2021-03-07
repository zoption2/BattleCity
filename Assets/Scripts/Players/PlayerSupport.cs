using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
public class PlayerSupport : MonoBehaviour, I_PlayerSpecific
{
    private GameObject targetPlayer = null;
    private LayerMask playerMask;
    private Transform target;
    private string _thisPlayer;
    private string _otherPlayer;
    private UI_BuffsDisplay buffsDisplay;
    private UI_BuffsDisplay otherBuffsDisplay;
    private PlayersStats playersStats;
    private VFXTotalSpawner vFXSpawner;
    private OtherPlayerSkillsControl mySkillControl;
    private OtherPlayerSkillsControl otherSkillControl;
    private LineRenderer lineRenderer;
    private Material material;
    private StateOfCircle state;

    private Coroutine attackSecondSkill;
    private Coroutine defendSecondSkill;
    private Coroutine attackThirdSkill;
    private Coroutine defendThirdSkill;

    private int my_stars;
    private int target_stars;
    private int resolution = 10;

   

    private void Start()
    {
        target = gameObject.transform.GetChild(1);
        playerMask = LayerMask.GetMask("Player");
        _thisPlayer = this.gameObject.tag;
        buffsDisplay = GetComponent<UI_BuffsDisplay>();
        playersStats = PlayersStats.Instance;
        vFXSpawner = VFXTotalSpawner.Instance;
        mySkillControl = GetComponent<OtherPlayerSkillsControl>();
        material = Resources.Load("Materials/LightArc") as Material;
        state = StateOfCircle.attackCircle;
        buffsDisplay.SetStateOfCircle(state);

        playersStats.OnTargetProjectilesUp += DudlicateProjectiles;
    }

    private void DudlicateProjectiles(string target)
    {
        if (targetPlayer != null && _otherPlayer == target)
        {
            playersStats.SetProjectiles(_thisPlayer, 1);
        }
    }

    private void SetLineRenderer(LineRenderer line)
    {
        line.material = material;
        line.textureMode = LineTextureMode.RepeatPerSegment;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        line.positionCount = 0;
    }

    public void FirstSkill(string playerName)
    {
        //if (targetPlayer == null && MasterController.totalPlayersInGame > 1)
        if (targetPlayer == null && MasterController.activePlayers.Count > 2)
            {
            RaycastHit hit;
            if (Physics.Raycast(target.position, target.forward, out hit, 50f, playerMask))
            {
                targetPlayer = hit.transform.gameObject;
                _otherPlayer = targetPlayer.tag;
                otherBuffsDisplay = targetPlayer.GetComponent<UI_BuffsDisplay>();
                otherSkillControl = targetPlayer.GetComponent<OtherPlayerSkillsControl>();
                //buffsDisplay.ShowCircle();
                otherBuffsDisplay.SetStateOfCircle(state);

                //otherBuffsDisplay.OnDisableTank += ShowMyConnectingCircle;

            }

            GameObject missile;
            missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("ConnectingBeam", target.transform.position, Quaternion.LookRotation(transform.forward));
            var _missile = missile.GetComponent<ConnectinBeam>();
            _missile.SetShooter(this.gameObject);
            _missile.DoRayShoot();
        }
        else
        {
            if (MasterController.playerBoosters[playerName] > 0)
            {
                //state = buffsDisplay.stateOfCircle;
                StartFirstSkill();

                playersStats.SetProjectiles(playerName, -1);
            }
        }
    }

    public void SecondSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 1)
        {
            //state = buffsDisplay.stateOfCircle;
            switch (state)
            {
                case StateOfCircle.attackCircle:
                    StartSecondAttackSkill();
                    break;
                case StateOfCircle.defenseCircle:
                    StartSecondDefenseSkill();
                    break;
            }

            playersStats.SetProjectiles(playerName, -2);
        }
    }

    public void ThirdSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 2)
        {
            //state = buffsDisplay.stateOfCircle;
            switch (state)
            {
                case StateOfCircle.attackCircle:
                    StartThirdAttackSkill();
                    break;
                case StateOfCircle.defenseCircle:
                    StartThirdDefenseSkill();
                    break;
            }

            playersStats.SetProjectiles(playerName, -3);
        }
    }
    #region FirstSkill
    private void StartFirstSkill()
    {
        var secondPlayer = targetPlayer;
        switch (secondPlayer)
        {
            case null:
                switch (state)
                {
                    case StateOfCircle.attackCircle:
                        buffsDisplay.SetStateOfCircle(StateOfCircle.defenseCircle);
                        state = StateOfCircle.defenseCircle;
                        break;
                    case StateOfCircle.defenseCircle:
                        buffsDisplay.SetStateOfCircle(StateOfCircle.attackCircle);
                        state = StateOfCircle.attackCircle;
                        break;
                }
                break;
            default:
                switch (state)
                {
                    case StateOfCircle.attackCircle:
                        buffsDisplay.SetStateOfCircle(StateOfCircle.defenseCircle);
                        otherBuffsDisplay.SetStateOfCircle(StateOfCircle.defenseCircle);
                        state = StateOfCircle.defenseCircle;
                        break;
                    case StateOfCircle.defenseCircle:
                        buffsDisplay.SetStateOfCircle(StateOfCircle.attackCircle);
                        otherBuffsDisplay.SetStateOfCircle(StateOfCircle.attackCircle);
                        state = StateOfCircle.attackCircle;
                        break;
                }
                break;
        }
    }

    #endregion
    #region SecondAttackSkill
    private void StartSecondAttackSkill()
    {
        if (!MasterController.stageClear)
        {
            secondAttackSkill();
            //if (attackSecondSkill != null)
            //{
            //    StopCoroutine(attackSecondSkill);
            //}
            //attackSecondSkill = StartCoroutine(secondAttackSkill());
        }
    }

    private void secondAttackSkill()
    {
        var secondPlayer = targetPlayer;
        switch (secondPlayer)
        {
            case null:
                mySkillControl.UseSupportStars();
                break;

            default:
                mySkillControl.UseSupportStars();
                otherSkillControl.UseSupportStars();
                break;
        }
    }

    #endregion
    #region SecondDefenseSkill
 private void StartSecondDefenseSkill()
    {
        var secondPlayer = targetPlayer;
        switch (secondPlayer)
        {
            case null:
                EnergyShield energyShield = GetComponent<EnergyShield>();
                energyShield.CustomImmortality(5f);
                break;
            default:
                EnergyShield energyShield_1 = GetComponent<EnergyShield>();
                energyShield_1.CustomImmortality(5f);
                EnergyShield energyShield_2 = targetPlayer.GetComponent<EnergyShield>();
                energyShield_2.CustomImmortality(5f);
                break;
        }
    }
    #endregion
    #region ThirdAttackSkill
    private void StartThirdAttackSkill()
    {
        var secondPlayer = targetPlayer;
        switch (secondPlayer)
        {
            case null:
                StartCoroutine(BuildArc(gameObject));
                break;
            default:
                StartCoroutine(BuildArc(gameObject));
                StartCoroutine(BuildArc(secondPlayer));
                break;
        }
    }
    #endregion

    #region ThirdDefenseSkill

    private void StartThirdDefenseSkill()
    {
        var secondPlayer = targetPlayer;
        switch (secondPlayer)
        {
            case null:
                playersStats.SetHealth(_thisPlayer, 1);
                break;
            default:
                playersStats.SetHealth(_thisPlayer, 1);
                playersStats.SetHealth(secondPlayer.tag, 1);
                break;
        }
    }
    #endregion
   
    

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Buffs>() != null)
        {
            if (targetPlayer == null)
            {
                collider.gameObject.GetComponent<Buffs>().BuffMe(gameObject);
            }
            else
            {
                collider.gameObject.GetComponent<Buffs>().BuffMe(gameObject, targetPlayer);
            }
        }
    }

    //private void ShowMyConnectingCircle()
    //{
    //    buffsDisplay.HideCircle();
    //    otherBuffsDisplay.OnDisableTank -= ShowMyConnectingCircle;
    //}

       


    private GameObject ChooseEnemy(Transform startPlayer)
    {
        LevelManager levelManager = LevelManager.Instance;
        GameObject enemy;
        Dictionary<float, GameObject> distances = new Dictionary<float, GameObject>();
        float[] allDist = new float[levelManager.GetAllEnemies().Count];
        int i = 0;

        foreach (var item in levelManager.GetAllEnemies() )
        {
            float distance = Vector3.Distance(startPlayer.position, item.transform.position);
            distances.Add(distance, item);
            allDist[i] = distance;
            i++;
        }

        var shottestDist = Mathf.Min(allDist);
        GameObject myTarget = distances[shottestDist];
        return myTarget;
    }
    private IEnumerator BuildArc(GameObject origin)
    {
        var newGO = new GameObject();
        newGO.transform.parent = origin.transform;
        newGO.transform.localPosition = new Vector3(0, 0, 0);
        LineRenderer lineRenderer = newGO.AddComponent<LineRenderer>();
        SetLineRenderer(lineRenderer);
        var thisTank = origin;
        var otherTank = ChooseEnemy(origin.transform);
        float timer = 0.2f;
        while (timer > 0)
        {
            if (otherTank != null)
            {
                RenderArc(thisTank, otherTank, lineRenderer);
                timer -= Time.deltaTime;
                yield return null;
            }
            else if (otherTank == null && newGO != null)
            {
                Destroy(newGO);
                timer = 0;
                yield break;
            }
            else
            {
                timer = 0;
                yield break;
            }

        }

        VFXTotalSpawner.Instance.PlayEffect("HitBlast", new Vector3(otherTank.transform.position.x, otherTank.transform.position.y + 1, otherTank.transform.position.z), Quaternion.identity, 0.7f);

        Health health = otherTank.GetComponent<Health>();
        health.SetKillerName(_thisPlayer);
        health.TakeDamage(2f);

        timer = 0.1f;
        while (timer > 0)
        {
            if (otherTank != null)
            {
                RenderArc(thisTank, otherTank, lineRenderer);
                timer -= Time.deltaTime;
                yield return null;
            }
            else if (otherTank == null && newGO != null)
            {
                Destroy(newGO);
                timer = 0;
                yield break;
            }
            else
            {
                timer = 0;
                yield break;
            } 

        }

        if (newGO != null)
        {
            Destroy(newGO);
        }
            
        //lineRenderer.positionCount = 0;
    }
    private void RenderArc(GameObject myTank, GameObject enemyTank, LineRenderer line)
    {
        float timer = 1f;
        line.positionCount = resolution + 1;
        //var enemy = ChooseEnemy(transform);
        line.SetPositions(SampleParabola(myTank, enemyTank));

    }
    private Vector3[] SampleParabola(GameObject start, GameObject finish)
    {
        Vector3[] coords = new Vector3[resolution + 1];

        Vector3 myTank = new Vector3(start.transform.position.x, start.transform.position.y + 1, start.transform.position.z);
        Vector3 enemyTank = new Vector3(finish.transform.position.x, finish.transform.position.y + 1, finish.transform.position.z);
        Vector3 centr = (enemyTank + myTank) * 0.5f;

        for (int i = 0; i < coords.Length; i++)
        {
            float fracComplete = (float)i / (float)coords.Length;
            //var diametr = Mathf.Clamp((Vector3.Magnitude(enemyTank - myTank)), 12f, 23f);
            var diametr = Vector3.Magnitude(enemyTank - myTank);
            var pointPos = Vector3.Lerp(myTank, enemyTank, fracComplete);
            var distToCos = Vector3.Magnitude(enemyTank - pointPos);
            var sin = 1 - ((float)distToCos / (float)diametr);
            var cos = (distToCos / (float)diametr);
            var drawPoint = new Vector3(pointPos.x, pointPos.y + (diametr * sin * cos), pointPos.z);
            coords[i] = drawPoint;
        }
        return coords;
    }

    //private IEnumerator BuildLightArc(GameObject origin)
    //{
    //    var thisTank = origin;
    //    var otherTank = ChooseEnemy(transform);
    //    float pointsCount = resolution + 1;
    //    //lineRenderer.positionCount = (int)pointsCount;

    //    for (int i = 0; i < pointsCount; i++)
    //    {
    //        float fracComplete = (float)i / (float)pointsCount;
    //        var point = CalculateArc(thisTank.transform.position, otherTank.transform.position, fracComplete);
    //        lineRenderer.positionCount = i + 1;
    //        lineRenderer.SetPosition(i, point);
    //        yield return new WaitForEndOfFrame();
    //    }

    //    StartCoroutine(BuildArc(thisTank));
    //}

    private Vector3 CalculateArc(Vector3 start, Vector3 end, float time)
    {
        var radius = Mathf.Clamp((Vector3.Magnitude(end - start)), 12f, 23f);
        var pointPos = Vector3.Lerp(start, end, time);
        var distToCos = Vector3.Magnitude(end - pointPos);
        var sin = (float)1 - ((float)distToCos / (float)radius);
        var cos = ((float)distToCos / (float)radius);
        var drawPoint = new Vector3(pointPos.x, pointPos.y + (radius * sin * cos), pointPos.z);

        return drawPoint;
    }

    private void OnDisable()
    {
        otherBuffsDisplay?.HideCircle();
        playersStats.OnTargetProjectilesUp -= DudlicateProjectiles;

        if (attackSecondSkill != null)
        {
            StopCoroutine(attackSecondSkill);
        }
    }

}
