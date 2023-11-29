using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskImpact : MonoBehaviour
{
    [SerializeField] ThrowSubtance throwSubtance;
    [SerializeField] ParticleSystem particlesBrokenGlass;
    [SerializeField] ParticleSystem particlesSmoke;
    
    
    private void Start() {
        var main = particlesBrokenGlass.main;
        particlesSmoke.Stop();
        main.maxParticles = 0;
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        var main = particlesBrokenGlass.main;
        var mainSmoke = particlesSmoke.main;
        if(other.transform.tag.Equals("Flask"))
        {
            throwSubtance.FlaskCrash();
            main.maxParticles += 20;
            switch (throwSubtance.GetIndex())
            {
                case 0:
                    mainSmoke.startColor = new Color(1,0,0,.5f);
                    break;
                case 1:
                    mainSmoke.startColor = new Color(0,0,1,.5f);
                    break;
                case 2:
                    mainSmoke.startColor = new Color(0,1,0,.5f);
                    break;
                default:
                    mainSmoke.startColor = new Color(1,1,1,.5f);
                    break;
            }
            particlesSmoke.Play();
            StartCoroutine(DeleteExtraParticles());
        }
    }

    private IEnumerator DeleteExtraParticles()
    {
        var main = particlesBrokenGlass.main;
        yield return new WaitForSeconds(9.9f);
        main.maxParticles -= 20;
    }

}
