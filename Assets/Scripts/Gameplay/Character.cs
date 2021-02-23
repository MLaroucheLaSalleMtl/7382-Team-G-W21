using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class to manage all the character stats
/// </summary>
public class Character : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float mana;
    public float stamina;
    public float damage;
    public float defense;

    private BasicStats _fortitude;
    private BasicStats _strength;
    private BasicStats _agility;
    private BasicStats _intelligence;

    private float _fortitudeP, _strengthP, _agilityP, _intelligenceP;

    private float _maxHealth;
    private float _maxStamina;
    private float _maxMana;

    public float MaxStamina { get { return _maxStamina; } }
    public float MaxHealth { get { return _maxHealth; } }

    private bool _isDead;

    [Header("Utilities")]
    [SerializeField] private HealthBar hpBar;

    [Header("Feedback")]
    public UnityEvent deadFeedback;

    /// <summary>
    /// Initializing character with scriptable object data
    /// </summary>
    /// <param name="basicStats"></param>
    /// <param name="level"></param>
    /// <param name="baseHP"></param>
    /// <param name="baseMana"></param>
    /// <param name="BaseEnergy"></param>
    /// <param name="baseDefense"></param>
    public virtual void Init(BasicStats[] basicStats, int level, float baseHP, float baseMana, float BaseEnergy, float baseDefense)
    {
        for (int i = 0; i < basicStats.Length; i++)
        {
            BasicStats stat = basicStats[i];
            switch (stat.stat)
            {
                case Stats.Strength:
                    _strengthP = stat.points;
                    break;
                case Stats.Agility:
                    _agilityP = stat.points;
                    break;
                case Stats.Intelligence:
                    _intelligenceP = stat.points;
                    break;
                case Stats.Fortitude:
                    _fortitudeP = stat.points;
                    break;
                default:
                    break;
            }
        }

        _isDead = false;

        this.health = Mathf.Round(baseHP * Mathf.Log(_fortitudeP * level));
        this._maxHealth = this.health;
        this.damage = (_strengthP * 2) + (_agilityP * 0.4f);
        this.mana = Mathf.Round(baseMana * Mathf.Log(_intelligenceP * level)); ;
        this._maxMana = this.mana;
        this.stamina = 100;
        this._maxStamina = this.stamina;
        this.defense = baseDefense;

        if (hpBar != null)
            hpBar.Init(this.health, this._maxHealth);
    }

    /// <summary>
    /// Function to receive damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        if (!_isDead)
        {
            health -= damage;
            if (hpBar != null)
                hpBar.UpdateBar(this.health);
        }

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
        float endDamage = damage / (entity.defense * 0.2f);
        Debug.Log(">>>" + entity.name + " receives the damage " + endDamage);
        entity.ReceiveDamage(endDamage);
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
