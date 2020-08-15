﻿using Stats;
using UnityEngine;
namespace SpawnerSystem.ScriptableObjects {
    public class Enemy : SpawnableSO,  ICharacterStat, ICharacterBase
    {
        [SerializeField] string _name;
        [SerializeField] uint _level;
        [SerializeField] int _baseHealth;
        [SerializeField] int _baseMana;
        [SerializeField] Gender _gender;
        [SerializeField] EnemyCharacter Stats;

        public string Name { get { return _name; } }
        public Gender gender { get { return _gender; } }
        public uint Level { get { return _level; } }
        public int BaseHealth { get { return _baseHealth; } }
        public int BaseMana { get { return _baseMana; } }
        // add logic for determine max health and mana 
        // Will Just use base Health for Max health until we add character system to project
        public override GameObject Spawn(Vector3 Position)
        {
         GameObject spawn =   Instantiate(GO, Position + SpawnOffset, Quaternion.identity);
            spawn.AddComponent<EnemyCharacter>();
            return spawn;

        }
    }
}