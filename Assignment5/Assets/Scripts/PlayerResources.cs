using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    // Sliders 
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _manaSlider;
    // Player Stats
    [SerializeField] private float _attack1Mana = 10;
    [SerializeField] private float _attack2Mana = 20;
    [SerializeField] private float _sprintMana = 0.05f;
    [SerializeField] private float _manaRegen = 0.05f;
    [SerializeField] private float _jumpMana = 20f;
    // Player scripts
    private PlayerAudio _playerAudio;
    // Components
    private Animator _animator;
    void Start()
    {
        // Player scripts
        _playerAudio = GetComponent<PlayerAudio>();
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _manaSlider.value += _manaRegen;
    }
    // Getters
    public float GetMana()
    {
        return _manaSlider.value;
    }
    public float GetHealth()
    {
        return _healthSlider.value;
    }
    public float GetAttack1Mana()
    {
        return _attack1Mana;
    }
    public float GetAttack2Mana()
    {
        return _attack2Mana;
    }
    public float GetSprintMana()
    {
        return _sprintMana;
    }
    public float GetJumpMana()
    {
        return _jumpMana;
    }
    // Setters
    public void AdjustHitPoints(float amount)
    {
        _healthSlider.value += amount;
    }
    public void AdjustMana(float amount)
    {
        _manaSlider.value += amount;
    }
    // Other
    public IEnumerator DamagePlayer(int damage, float interval)
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            _playerAudio.Play("hurt");
            _animator.SetTrigger("hurt");
            _healthSlider.value -= damage;
            Invoke(nameof(ChangeColor), 0.167f);
            if (_healthSlider.value <= float.Epsilon)
            {
                KillPlayer();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }
    private void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void KillPlayer()
    {
        _playerAudio.Play("death");
        _animator.SetBool("alive", false);
        Debug.Log("Dead");
    }
}
