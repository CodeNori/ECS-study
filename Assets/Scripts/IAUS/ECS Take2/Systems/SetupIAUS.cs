﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;


namespace IAUS.ECS2
{
    [UpdateBefore(typeof(ConsiderationSystem))]
    public class SetupIAUS : JobComponentSystem
    {

        EntityCommandBufferSystem entityCommandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            entityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }


        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {

            JobHandle patrolAdd = new PatrolAdd()
            {
                health = GetComponentDataFromEntity<HealthConsideration>(true),
                TimeC = GetComponentDataFromEntity<TimerConsideration>(true),
                Distance = GetComponentDataFromEntity<DistanceToConsideration>(true),
                entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer()
            }.Schedule(this, inputDeps);

            JobHandle waitAdd = new WaitAdd()
            {
                health = GetComponentDataFromEntity<HealthConsideration>(true),
                TimeC = GetComponentDataFromEntity<TimerConsideration>(true),
                Distance = GetComponentDataFromEntity<DistanceToConsideration>(true),
                entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer()
            }.Schedule(this, patrolAdd);


            return waitAdd;
        }
        [BurstCompile]
        public struct PatrolAdd : IJobForEachWithEntity_EBCC<StateBuffer, Patrol, CreateAIBufferTag>
        {

            [ReadOnly] [NativeDisableParallelForRestriction] public ComponentDataFromEntity<HealthConsideration> health;
            [ReadOnly] [NativeDisableParallelForRestriction] public ComponentDataFromEntity<TimerConsideration> TimeC;// possible removing as it is not valid
            [ReadOnly] [NativeDisableParallelForRestriction] public ComponentDataFromEntity<DistanceToConsideration> Distance;

            [NativeDisableParallelForRestriction] public EntityCommandBuffer entityCommandBuffer;

            public void Execute(Entity entity, int Tindex, DynamicBuffer<StateBuffer> stateBuffer, [ReadOnly] ref Patrol c1, [ReadOnly]ref CreateAIBufferTag c2)
            {
                bool add = true;
                for (int index = 0; index < stateBuffer.Length; index++)
                {
                    if (stateBuffer[index].StateName == AIStates.Patrol)
                    { add = false; }
                }

                if (add)
                {
                    stateBuffer.Add(new StateBuffer()
                    {
                        StateName = AIStates.Patrol,
                        Status = ActionStatus.Idle
                    });
                    if (!health.Exists(entity))
                    {
                        entityCommandBuffer.AddComponent<HealthConsideration>(entity);

                    }
                    if (!TimeC.Exists(entity))
                    {
                        entityCommandBuffer.AddComponent<TimerConsideration>(entity);

                    }
                    if (!Distance.Exists(entity))
                    {
                        entityCommandBuffer.AddComponent<DistanceToConsideration>(entity);

                    }
                }
            }
        }


        [BurstCompile]
        public struct WaitAdd : IJobForEachWithEntity_EBCC<StateBuffer, WaitTime, CreateAIBufferTag>
        {

            [ReadOnly] [NativeDisableParallelForRestriction] public ComponentDataFromEntity<HealthConsideration> health;
            [ReadOnly] [NativeDisableParallelForRestriction] public ComponentDataFromEntity<TimerConsideration> TimeC;// possible removing as it is not valid
            [ReadOnly] [NativeDisableParallelForRestriction] public ComponentDataFromEntity<DistanceToConsideration> Distance;

            [NativeDisableParallelForRestriction] public EntityCommandBuffer entityCommandBuffer;

            public void Execute(Entity entity, int Tindex, DynamicBuffer<StateBuffer> stateBuffer, [ReadOnly] ref WaitTime c1, [ReadOnly]ref CreateAIBufferTag c2)
            {
                bool add = true;
                for (int index = 0; index < stateBuffer.Length; index++)
                {
                    if (stateBuffer[index].StateName == AIStates.Wait)
                    { add = false; }
                }

                if (add)
                {
                    stateBuffer.Add(new StateBuffer()
                    {
                        StateName = AIStates.Wait,
                        Status = ActionStatus.Idle
                    });
                    if (!health.Exists(entity))
                    {
                        entityCommandBuffer.AddComponent<HealthConsideration>(entity);

                    }
                    if (!TimeC.Exists(entity))
                    {
                        entityCommandBuffer.AddComponent<TimerConsideration>(entity);

                    }
                    if (!Distance.Exists(entity))
                    {
                        entityCommandBuffer.AddComponent<DistanceToConsideration>(entity);

                    }
                }
            }
        }
    }
}
