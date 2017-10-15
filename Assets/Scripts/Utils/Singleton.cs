﻿using UnityEngine;


public class Singleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }
    }

    public static void Destroy()
    {
        _instance = null;
    }

}
