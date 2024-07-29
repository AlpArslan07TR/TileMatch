using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static T Instance
    {
        get
        {
            if(_instance != null)
            {
                return _instance;
            }
            return _instance = FindAnyObjectByType<T>();
        }
    }
    private static T _instance;


    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
        }

        if(_instance != this)
        {
            Debug.LogError("Duplicate Singleton found.Destroying this instance");
            Destroy(this);
        }
    }

}
