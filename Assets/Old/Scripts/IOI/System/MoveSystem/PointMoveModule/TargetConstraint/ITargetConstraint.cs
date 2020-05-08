using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public interface ITargetConstraint
    {
        /// <summary>
        /// 对目标点进行约束
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="startPosition"></param>
        /// <param name="originalTarget">原始目标点</param>
        /// <returns></returns>
        Vector3 TargetConstraint(Transform transform,Vector3 startPosition, Vector3 originalTarget);
    }
}