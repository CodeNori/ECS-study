﻿
using UnityEngine;
using Unity.Entities;
using CharacterStats;


namespace IAUS.ECS2.Charaacter
{
    public class CharacterAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [Header("Character Stats")]
        [Range(0, 999)]
        public int CurHealth;
        [Range(0, 999)]
        public int CurMana;
        [Range(0, 999)]
        public int MaxHealth;
        [Range(0, 999)]
        public int MaxMana;


        public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<EnemyCharacter>(entity);
            dstManager.AddComponent<ECS.Component.Movement>(entity);
            dstManager.AddComponent<Unity.Transforms.CopyTransformFromGameObject>(entity);
            var data = new Stats() { CurHealth = CurHealth, CurMana = CurMana, MaxHealth = MaxHealth, MaxMana = MaxMana };
            dstManager.AddComponentData(entity, data);
            dstManager.AddComponent<NPC>(entity);
        }
    }

    public struct NPC : IComponentData
    {

        public Entity HomeEntity { get; set; }

    }
}
