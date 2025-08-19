using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Studio.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    public float duration = 0.3f;

    [SerializeField]private Vignette _vignette;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            Debug.Log("inicie a cena"); 
            return;
        }
        StartCoroutine(FlashColorVignette()); 

    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;
        //out significa para passar referência direta, não criar
        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();
        FloatParameter f = new FloatParameter();
        float startIntensity = _vignette.intensity.value;

        float time = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            f.value = 0.5f;
            time += Time.deltaTime;
            _vignette.intensity.value = Mathf.Lerp(startIntensity, f, time / duration);
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            _vignette.intensity.value = Mathf.Lerp(f, startIntensity, time / duration);
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
    }
}
