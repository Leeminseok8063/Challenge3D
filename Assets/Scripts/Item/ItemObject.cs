using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract();

}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    public event Action<string> InitPrompt;

    public void Init()
    {
        string str = $"{data.displayName}\n{data.description}";
        InitPrompt?.Invoke(str);
    }
   
    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
