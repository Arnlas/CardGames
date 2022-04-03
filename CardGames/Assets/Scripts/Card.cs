using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Card : IDisposable
{
    #region Private fields

    public UnityEvent CardRemoved = new UnityEvent();
    private Texture backTexture;
    protected Dictionary<string, object> data = new Dictionary<string, object>();

    #endregion

    public Card(Texture tex = null)
    {
        backTexture = tex;
    }

    public bool IsParameterPresent<T>(string key, out T value)
    {
        value = default(T);
        if (data == null) Debug.LogError("No data");
        if (data == null || !data.ContainsKey(key)) return false;

        value = (T) data[key];

        return true;
    }
    
    public T GetParameter<T>(string key)
    {
        return (T)data[key];
    }

    public void AddParameter<T>(string key, T value)
    {
        if (data == null)
            data = new Dictionary<string, object>();
        data.Add(key, (object) value);
    }

    public Texture GetTexture()
    {
        return backTexture;
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(backTexture);
        data.Clear();
    }
}