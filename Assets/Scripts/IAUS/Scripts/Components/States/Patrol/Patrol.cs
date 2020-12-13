﻿using UnityEngine;
using Unity.Entities;

namespace IAUS.ECS2.Component
{
    [GenerateAuthoringComponent]
    public struct Patrol : IBaseStateScorer
    {

        public float NumberOfWayPoints;
        public ConsiderationScoringData DistanceToPoint;
        public ConsiderationScoringData HealthRatio;
        public bool Complete => Status == ActionStatus.Running;
        public float TotalScore { get { return _totalScore; } set { _totalScore = value; } }
        public ActionStatus Status { get { return _status; } set { _status = value; } }
        public float ResetTimer { get { return _resetTimer; } set { _resetTimer = value; } }
        public float ResetTime { get { return _resetTime; } set { _resetTime = value; } }
        public float distanceToPoint;
        public float StartingDistance;
        public float DistanceRatio => distanceToPoint / StartingDistance;
        public Waypoint CurWaypoint;

        public float mod { get { return 1.0f - (1.0f / 2.0f); } }
        [HideInInspector] public bool UpdatePatrolPoints;
        [SerializeField] public ActionStatus _status;
        [SerializeField] public float _resetTimer;
        [SerializeField] float _resetTime;
        [SerializeField] float _totalScore;
    }

    public struct PatrolActionTag : IComponentData { }
}