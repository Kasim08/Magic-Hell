using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnLevelEnd.AddListener(Dispose);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnLevelEnd.RemoveListener(Dispose);
    }
    public abstract void Collect();
    public abstract void Use();

    public virtual void Dispose()
    {
        Destroy(gameObject);
    }
}
