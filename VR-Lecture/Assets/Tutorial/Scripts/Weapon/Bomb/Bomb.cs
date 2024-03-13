using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bomb : MonoBehaviour
{
    public enum State
    {
        Idle,
        Drop,
    }

    public float explosionRadius;
    public LayerMask explosionHittableMask;

    public float recycleDelay = 1f;

    public UnityEvent OnExplision;
    public UnityEvent OnRecycle;

    private State state;

    public void Drop()
    {
        state = State.Drop;
    }

    public void Throw()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0f, 150f, 300f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == State.Idle)
            return;

        Explosion();
    }

    private void Explosion()
    {
        var overlaps = Physics.OverlapSphere(transform.position, explosionRadius, explosionHittableMask, QueryTriggerInteraction.Collide);
        foreach (var overlap in overlaps)
        {
            var hitObject = overlap.GetComponent<Hittable>();
            hitObject?.Hit();
        }

        OnExplision?.Invoke();
        Invoke(nameof(Recycle), recycleDelay);
    }

    public void Recycle()
    {
        state = State.Idle;

        OnRecycle?.Invoke();
    }
}
