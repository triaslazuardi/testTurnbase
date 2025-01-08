using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FxManage : MonoBehaviour
{
    public ParticleSystem particleObj;
    public float durationParticle;

    public async void RunFx() { 
        gameObject.SetActive(true);
        particleObj.Play();
        StartCoroutine(WaitRunFx());
    }

    public IEnumerator WaitRunFx()
    {
        yield return new WaitForSeconds(durationParticle);

        LeanPool.Despawn(this);
    }
}
