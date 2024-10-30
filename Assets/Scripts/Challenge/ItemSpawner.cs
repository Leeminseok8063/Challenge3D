using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Challenge
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField]List<ItemData> itemDatas;
        public void SpawnItem(Vector3 pos, int id)
        {
            ItemObject temp = new GameObject().AddComponent<ItemObject>();
            temp.data = itemDatas[id];
            temp.gameObject.name = temp.data.name;
            temp.gameObject.layer = 7;
            temp.transform.localScale *= 3;

            temp.AddComponent<Rigidbody>();
            BoxCollider box = temp.AddComponent<BoxCollider>();
            box.size = temp.data.itemMesh.GetComponent<BoxCollider>().size;

            Instantiate(temp.data.itemMesh, temp.transform);
            temp.data.itemMesh.gameObject.layer = 0;
        }
    }
}
