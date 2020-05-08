using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
namespace IOIntensiveFramework.Move
{
    public class MoveManager : MonoSingletonDontDestroy<MoveManager>
    {
        public VectorMoveCore CreateVectorMoveCore(Transform transform, IVectorConstraint vectorConstraint, IVectorToPosition vectorToPosition, IPositionConstraint positionConstraint)
        {
            VectorMoveCore vectorMoveCore = new VectorMoveCore(transform, vectorConstraint, vectorToPosition, positionConstraint);
            return vectorMoveCore;
        }
    }
}