using UnityEngine;

public class SplitRocketEffect : MonoBehaviour
{
    public ParticleSystem smokeTrail; // roketin b�rakt��� duman efekti

    private void Start()
    {
        smokeTrail.Play(); // duman efektini ba�lat
    }

    private void OnDestroy()
    {
        smokeTrail.Stop(); // duman efektini durdur
    }
}