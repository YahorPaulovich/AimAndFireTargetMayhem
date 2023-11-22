using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _timeoutDelay = 3f;

    private IObjectPool<Projectile> _objectPool;

    public IObjectPool<Projectile> SetObjectPool { set => _objectPool = value; }

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        _objectPool.Release(this);
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(_timeoutDelay));
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}
