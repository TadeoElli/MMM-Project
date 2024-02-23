using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Observer<T> {
    [SerializeField] T value;
    [SerializeField] UnityEvent<T> onValueChanged;

    public T Value{
        get => value;
        set => Set(value);
    }

    public Observer(T value, UnityAction<T> callback = null){
        this.value = value;
        onValueChanged = new UnityEvent<T>();
        if(callback != null) onValueChanged.AddListener(callback);
    }

    public void Set(T value){
        if(Equals(this.value, value)) return;
        this.value = value;
        Invoke();
    }

    public void Invoke(){
        Debug.Log($"Invoking{onValueChanged.GetPersistentEventCount()} listeners");
        onValueChanged.Invoke(value);
    }

    public void AddListener(UnityAction<T> callback){
        if(callback == null) return;
        if(onValueChanged == null) onValueChanged = new UnityEvent<T>();

        onValueChanged.AddListener(callback);
    }

    public void RemoveListener(UnityAction<T> callback){
        if(callback == null) return;
        if(onValueChanged == null) onValueChanged = new UnityEvent<T>();

        onValueChanged.RemoveListener(callback);
    }
    
}

