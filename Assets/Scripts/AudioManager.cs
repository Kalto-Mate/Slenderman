using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleInstance;
    private void Awake() { if (singleInstance == null) singleInstance = this; }
    //---------------------------------------------------------------------------

    public AudioSource nieve;           float nieveMaxVolume = 1;
    public AudioSource look;            float stareMaxVolume = 1f;
    public AudioSource aprehension;     float aprehensionMaxVolume = 1f;    bool aprehensionInProgress = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //EXPONER VOLUMEN DE LOS AUDIO MIXERS


    //CONTROL AUDIO SOURCES

    void SetAudiosourceVolume(AudioSource target, float volume)
    {
        target.volume = volume;
    }

    //EFECTOS DE ESTÁTICA
    public void UpdateStaticEffect(float gradient)
    {
        float newVol = nieveMaxVolume * gradient;
        SetAudiosourceVolume(nieve, newVol);
    }

    public void UpdateStareEffect(float gradient)
    {
        float newVol = stareMaxVolume * gradient;
        SetAudiosourceVolume(look, newVol);
    }

    //APREHENSION

    public void AttemptAprehension(int chance)
    {
        int random = Random.Range(0, 100);
        if (random <= chance    &&      !aprehensionInProgress) StartCoroutine(AprehensionEvent());
        
    }
    IEnumerator AprehensionEvent()
    {
        aprehensionInProgress = true;
        while (aprehension.volume < aprehensionMaxVolume)
        {
            aprehension.volume = aprehension.volume + 0.05f;
            yield return null;
        }

        SetAudiosourceVolume(aprehension, aprehensionMaxVolume);
        float time = Random.Range(3f, 10f);
        yield return new WaitForSeconds(time);

        while (aprehension.volume > 0)
        {
            aprehension.volume = aprehension.volume - 0.001f;
            yield return null;
        }
        aprehensionInProgress = false;

    }
}
