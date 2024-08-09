using System.Collections;
using UnityEngine;

public class HeroThrowingWeapon : MonoBehaviour
{
    [SerializeField] private Rigidbody weaponRigidBody;
    [SerializeField] private float throwSpeed;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private void Start()
    {
        originalRotation = weaponRigidBody.transform.rotation;
        originalPosition = weaponRigidBody.transform.position;
        StartCoroutine(ThrowWeapon());
    }

    private IEnumerator ThrowWeapon()
    {
        Vector3 throwDirection = new Vector3(0, 0, -0.5f);
        weaponRigidBody.velocity = throwDirection * throwSpeed;

        yield return new WaitForSeconds(5f);

        //float elapsedTime = 0f;
        //float timerMax = 5f;

        //while (elapsedTime < timerMax)
        //{
        //    elapsedTime += Time.deltaTime;

        //    //float progress = elapsedTime / timerMax;

        //    //float angle = 360 * progress;

        //    //weaponRigidBody.rotation = originalRotation * Quaternion.Euler(angle, 0, 0);

        //    yield return null;
        //}

        weaponRigidBody.gameObject.SetActive(false);
    }
}
