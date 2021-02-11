using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class to manage all the character stats
/// </summary>
public class Character : MonoBehaviour
{
    public float health;
    public float mana;
    public float energy;
    public float damage;

    private BasicStats _fortitude;
    private BasicStats _strength;
    private BasicStats _agility;
    private BasicStats _intelligence;

    private float _maxHealth;

    private bool _isDead;

    public UnityEvent deadFeedback;

    /// <summary>
    /// Init function to set the basic stats
    /// </summary>    
    /// <param name="stats">Fortitude, Strength, Agility, Intelligence</param>
    public virtual void Init(params float[] stats)
    {
        try
        {
            _fortitude = new BasicStats(Stats.Fortitude, stats[0]);
            _strength = new BasicStats(Stats.Strength, stats[1]);
            _agility = new BasicStats(Stats.Agility, stats[2]);
            _intelligence = new BasicStats(Stats.Intelligence, stats[3]);
        }
        catch
        {
            _fortitude = new BasicStats(Stats.Fortitude, 10);
            _strength = new BasicStats(Stats.Strength, 10);
            _agility = new BasicStats(Stats.Agility, 10);
            _intelligence = new BasicStats(Stats.Intelligence, 10);
        }

        _isDead = false;

        // TODO::Calculate damage, mana, energy and health correctly
        this.health = 10;
        _maxHealth = health;
    }

    /// <summary>
    /// Function to receive damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        if (!_isDead)
            health -= damage;

        if (health <= 0)
        {
            _isDead = true;
            TriggerDeath();
        }
    }

    /// <summary>
    /// Function to do damage
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="damage"></param>
    public void DoDamage(Character entity)
    {
        Debug.Log(">>>" + entity.name + " receives the damage");
        entity.ReceiveDamage(damage);
    }

    /// <summary>
    /// Function to trigger death phase
    /// </summary>
    public void TriggerDeath()
    {
        // TODO::Implement correctly the dead feeback
        //deadFeedback.Invoke();
        Destroy(gameObject);
    }
}
