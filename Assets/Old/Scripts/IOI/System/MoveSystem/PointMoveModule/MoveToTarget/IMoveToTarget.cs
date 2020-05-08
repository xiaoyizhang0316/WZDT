using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public interface IMoveToTarget
    {
        void MoveToTarget(Transform transform,Vector3 startPosition,Vector3 lastConstraintTarget, Vector3 constraintTarget,Action complete);
        void StopMove(Transform transform, Vector3 startPosition,Vector3 lastConstraintTarget, Vector3 constraintTarget, Action complete);
    }
}