using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public interface IVectorToPosition
    {
        Vector3 VectorToPostion(Vector3 startPosition, Vector3 lastPosition, Vector3 currentPosition, Vector3 moveVector);
    }
}