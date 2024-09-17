using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class DracovidMeteorScript : MonoBehaviour
{
    [SerializeField] private float shotDelay = 0.33f;
    [SerializeField] private Vector3 shotSourceOffset;
    [SerializeField] private VisualEffect[] meteors = new VisualEffect[3];

    private Transform[] targets = new Transform[3];
    private bool IsTargetSet = false;

    private void OnEnable()
    {
        CalibrateTargets();
        StartCoroutine(LaunchMeteors());
    }

    public void SetTargets(Transform[] targets)
    {
        this.targets = targets;
    }

    private void CalibrateTargets()
    {
        for (int i = 0; i < 3; i++)
        {
            meteors[i].SetVector3("DownFrom", targets[i].position + shotSourceOffset);
        }
    }

    public IEnumerator LaunchMeteors()
    {
        meteors[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(shotDelay);
        meteors[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(shotDelay);
        meteors[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        for (int i = 0; i < 3; i++)
        {
            meteors[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

}
