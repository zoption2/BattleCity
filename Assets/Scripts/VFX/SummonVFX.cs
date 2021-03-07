using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonVFX : MonoBehaviour
{
    [SerializeField] private string OnEnableEffectName;
    [SerializeField] private string OnDisableEffectName;
    [SerializeField] private float TimeToWork = 1;
    [SerializeField] bool needFollow;

    private VFXTotalSpawner holder;
    private Transform parent;
    private void Awake()
    {
        holder = VFXTotalSpawner.Instance;
        if (needFollow)
        {
            parent = transform;
        }
    }
    private void OnEnable()
    {
        if(OnEnableEffectName != null)
        holder?.PlayEffect(OnEnableEffectName, transform.position, Quaternion.identity, TimeToWork, parent);
    }
    private void OnDisable()
    {
        if(OnDisableEffectName != null)
        holder?.PlayEffect(OnDisableEffectName, transform.position, Quaternion.identity, TimeToWork, parent);
    }


}
