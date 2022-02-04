using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformSegment : MonoBehaviour
{
    public void Bounce(float fourse, Vector3 centere, float radius)
    {
        if(TryGetComponent(out Rigidbody rigidBody))
        {
            rigidBody.isKinematic = false;
            rigidBody.AddExplosionForce(fourse, centere, radius);
            StartCoroutine(DestroySegment());
        }
    }

    private IEnumerator DestroySegment()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
