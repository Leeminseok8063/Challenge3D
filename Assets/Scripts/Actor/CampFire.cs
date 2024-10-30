using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;
    bool isDamaged = false;
    List<IDamagable> things = new List<IDamagable>();

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("DealDamage", 0, damageRate);
    }

    private void Update()
    {
        if(!isDamaged) StartCoroutine(DealDamage());
    }

    // Update is called once per frame
    IEnumerator DealDamage()
    {
        isDamaged = true;
        yield return new WaitForSeconds(damageRate);
        for(int i = 0; i < things.Count; i++)
        {
            things[i].TakeDamage(damage);
        }
        isDamaged= false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }
    }
}
