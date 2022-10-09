using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerCore _core;
    [SerializeField] float _health = 1000;
    [SerializeField] float _maxHealth = 1000;
    Transform _healthFill;
    Transform _healthGhost;
    [SerializeField] float _fillDuration = 0.7f;
    [SerializeField] float _ghostDuration = 2f;

    [SerializeField] float _deathDuration = 2f;
    [SerializeField] float _deathLength = 5f;
    [SerializeField] float _deathHeight = 5f;

    bool _isKilled = false;
    void Start()
    {
        _health = _maxHealth;
        _core = GetComponent<PlayerCore>();
        _healthFill = Singleton.Instance.Game.HealthFill;
        _healthGhost = Singleton.Instance.Game.HealthGhost;

        _healthFill.localScale = Vector2.one; //For when the game is restarted
        _healthGhost.localScale = Vector2.one;
    }
    public void Damage(float damage, Vector3 direction)
    {
        float startHealth = _health/_maxHealth;
        _health = _health-damage;
        float targetHealth = _health/_maxHealth;

        if(_health <= 0)
        {
            _health = 0;
            OnDeath(direction);
            StartCoroutine(DamagedBar(startHealth, 0));

            if(!_isKilled)
            {
                _isKilled = true;
                Singleton.Instance.Game.PlayKillParticle(transform.position);
                Singleton.Instance.Game.CollectiblesManager.DropCoins(Random.Range(5, 12), transform.position);
            }
        }
        else
        {
            Singleton.Instance.Audio.
            StartCoroutine(DamagedBar(startHealth, targetHealth));
        }
    }

    ushort key;
    IEnumerator DamagedBar(float startHealth, float targetHealth)
    {
        float tFill = 0;
        float tGhost = 0;
        key++;
        ushort requirement = key;
        while((tFill <= 1 || tGhost <= 1) && key == requirement)
        {
            tFill = Mathf.Clamp(tFill + Time.deltaTime/_fillDuration, 0, 1);
            tGhost = Mathf.Clamp(tGhost + Time.deltaTime/_ghostDuration, 0, 1);

            _healthFill.localScale = new Vector2(
                Mathf.Lerp(startHealth, targetHealth, Ease.OutQuart(tFill))
            , _healthFill.localScale.y);
            
            _healthGhost.localScale = new Vector2(
                Mathf.Lerp(startHealth, targetHealth, Ease.InQuart(tGhost))
            , _healthGhost.localScale.y);
            
            yield return null;
        }
        if(requirement == key)
        {
            _healthFill.localScale = new Vector2(targetHealth, _healthFill.localScale.y);
            _healthGhost.localScale = new Vector2(targetHealth, _healthGhost.localScale.y);
        }
    }

    void OnDeath(Vector2 direction) => StartCoroutine(DeathAnimation(direction));
    ushort keyDeath;
    IEnumerator DeathAnimation(Vector2 direction)
    {
        //Don't hurtbox so people can juggle
        Transform animator = transform.GetChild(0);
        float t = 0;
        Vector2 startPosition = _core.Locomotion.Rb.position;
        Vector2 targetPosition = _core.Locomotion.Rb.position + direction * _deathLength;
        keyDeath++;
        ushort requirement = keyDeath;
        while(t <= 1 && keyDeath == requirement)
        {
            t += Time.deltaTime/_deathDuration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);
            animator.localPosition = new Vector2(
                0,
                NormalizedParabole(t) * _deathHeight
            );
            yield return null;
        }
        if(keyDeath == requirement)
        {
            //This makes it so that the enemy will only die when finished juggling.
            //Juggling mechanic might be fun.
            Singleton.Instance.Game.PlayDeathParticle(transform.position + Vector3.up);
            
            Camera.main.transform.SetParent(null);
            gameObject.SetActive(false);
        }
        
    }
    float NormalizedParabole(float x) => -4*(0.5f - x)*(0.5f - x) + 1;
}
