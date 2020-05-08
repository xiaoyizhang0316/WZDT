using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public interface IVectorConstraint
    {
        Vector3 VectorConstraint(Vector3 startPosition, Vector3 lastPosition, Vector3 currentPosition,Vector3 moveVector);
    }
}
