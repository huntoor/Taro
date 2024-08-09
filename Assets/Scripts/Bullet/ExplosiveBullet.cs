using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float radius = 2;
    [SerializeField] private float force = 20;

    [SerializeField] private LayerMask layerMask;
    private Collider2D[] affectedColliders;


    private void Awake()
    {
        explosionParticle.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    private void Explode()
    {
        float cameraShakeIntenisty = 3f;
        float cameraShakeTimer = 0.6f;
        CameraShake.Instance.ShakeCamera(cameraShakeIntenisty, cameraShakeTimer);
        explosionParticle.Play();

        affectedColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D affectedCollider in affectedColliders)
        {
            if (affectedCollider.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                Vector2 forceDirection = (rb.transform.position - transform.position).normalized;

                rb.MovePosition(rb.position + (force * forceDirection));
            }
        }
    }
}
