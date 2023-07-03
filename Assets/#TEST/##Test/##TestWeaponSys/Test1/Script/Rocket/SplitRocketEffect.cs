using UnityEngine;

public class SplitRocketEffect : MonoBehaviour
{
    public ParticleSystem smokeTrail; // roketin býraktýðý duman efekti

    private void Start()
    {
        smokeTrail.Play(); // duman efektini baþlat
    }

    private void OnDestroy()
    {
        smokeTrail.Stop(); // duman efektini durdur
    }
}