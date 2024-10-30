using Assets.Scripts;
using Assets.Scripts.Challenge;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader instance;
    public static SceneLoader Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject().AddComponent<SceneLoader>();
                instance.gameObject.name = "SceneLoader";
            }

            return instance;
        }

        
    }

    public GameObject UIObject;
    public UIModule uiModule;
    public ItemSpawner itemSpawner;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        uiModule = Instantiate(UIObject).GetComponent<UIModule>();
        itemSpawner = GetComponent<ItemSpawner>();
    }

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            itemSpawner.SpawnItem(new Vector3(10, 4.1500001f, -8.44999981f), 0);
        }
    }

    void Update()
    {
        
    }
}
