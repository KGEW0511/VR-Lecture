using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Mob : MonoBehaviour
{
    public float destroyDelay = 1f;

    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    private bool isDestroyed = false;

    private void Start()
    {
        Invoke(nameof(Destroy), 3f);

        OnCreated?.Invoke();
    }
    public void Destroy()
    {
        if (isDestroyed)
            return;
        isDestroyed = true;

        Destroy(gameObject, destroyDelay);

        OnDestroyed?.Invoke();
    }
}
