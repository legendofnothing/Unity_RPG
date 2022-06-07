using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _spellCasting;

    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileManaCost;
    [SerializeField] private float _projectileDamage;

    private void Update() {
        _player.GetComponent<PlayerAttack>().spellToCast = _spellCasting;
        _player.GetComponent<PlayerAttack>().projectileSpeed = _projectileSpeed;
        _player.GetComponent<PlayerAttack>().projectileManaCost = _projectileManaCost;
        _player.GetComponent<PlayerAttack>().projectileDamage = _projectileDamage;
    }
}
