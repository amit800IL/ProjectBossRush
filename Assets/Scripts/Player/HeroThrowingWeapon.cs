using System.Collections;
using UnityEngine;

public class HeroThrowingWeapon : MonoBehaviour
{
    [SerializeField] private Rigidbody weaponRigidBody;
    [SerializeField] private float throwForce;
    [SerializeField] private float jumpHeight = 2f;
    private void Start()
    {
        StartCoroutine(ThrowWeapon());
    }

    private IEnumerator ThrowWeapon()
    {
        Vector3 jumpForce = new Vector3(0, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y)), 0);

        weaponRigidBody.AddForce(jumpForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        Vector3 throwDirection = new Vector3(0, 0, -1f);

        weaponRigidBody.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(0.5f);

        weaponRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(5f);

        weaponRigidBody.gameObject.SetActive(false);
    }
}
