using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeControl : MonoBehaviour
{
    private GameplayManager GPM;
    private Volume _volume;
    private ColorAdjustments colorAdjustments;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        GPM.OnLevelFailed += ScreenAberation;
        GPM.OnLevelFailed += ScreenSaturation;
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out colorAdjustments);
        _volume.profile.TryGet(out chromaticAberration);
        
    }

    private void ScreenSaturation()
    {
        StartCoroutine(screenSaturation());
    }
    private IEnumerator screenSaturation()
    {
       // _volume.profile.TryGetSettings(out color);
       
        
        float colorDrop = 0;
        while (colorDrop >  - 100f)
        {
            colorDrop -= Time.deltaTime * 25;
            colorAdjustments.saturation.value = Mathf.Clamp(colorDrop, -100, 0);
            yield return null;
        }
    }

    private void ScreenAberation()
    {
        StartCoroutine(screenAberation());
    }
    private IEnumerator screenAberation()
    {
        float aberate = 0f;

        while (aberate < 0.5f)
        {
            aberate += Time.deltaTime * 5;
            chromaticAberration.intensity.value = Mathf.Clamp(aberate, 0f, 0.5f);
            yield return null;
        }
        while (aberate > 0)
        {
            aberate -= Time.deltaTime;
            chromaticAberration.intensity.value = Mathf.Clamp(aberate, 0f, 0.5f);
            yield return null;
        }
    }

}
