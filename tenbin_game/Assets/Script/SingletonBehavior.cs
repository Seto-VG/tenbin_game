using System;
using UnityEngine;

public abstract class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                Type t = typeof(T);

                _instance = (T)FindFirstObjectByType(t);
                if (_instance == null) { Debug.Log("no game objects : " + t); }
            }
            return _instance;
        }
    }

    virtual protected void Awake()
    {
        CheckInstance();
    }

    virtual protected void OnDestroy() { if (instance == this) { _instance = null; } }

    protected bool CheckInstance()
    {
        if (_instance == null)
        {
            _instance = this as T;
            return true;
        }
        else if (instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}