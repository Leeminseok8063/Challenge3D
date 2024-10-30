using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public interface IHeal
{
    void Heal(float amount);
}

public class PlayerCondition : MonoBehaviour, IDamagable, IHeal
{
    public UICondition uICondition;
    Condition health    { get { return uICondition.health; } }
    Condition hunger    { get { return uICondition.hunger; } }
    Condition stamina   { get { return uICondition.stamina; } }
    Condition shiled    { get { return uICondition.shield; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if(health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        Debug.Log("GG");
    }

    public void TakeDamage(int damage)
    {
        if(shiled.curValue > 0)
        {
            shiled.Subtract(damage);
        }
        else
        {
            health.Subtract(damage);
        }

        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }

   
}
