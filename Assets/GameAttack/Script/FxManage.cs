using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FxManage : MonoBehaviour
{
    public ParticleSystem particleObj;
    public float durationParticle;

    public async void RunFx(UnityAction callback = null) { 
        gameObject.SetActive(true);
        particleObj.Play();
        StartCoroutine(WaitRunFx(callback));
    }

    public IEnumerator WaitRunFx(UnityAction callback = null)
    {
        yield return new WaitForSeconds(durationParticle);

        LeanPool.Despawn(this);
        callback?.Invoke();
    }
}
