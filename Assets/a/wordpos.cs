using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wordpos : MonoBehaviour
{
    
        public RectTransform rectTransA;
        public RectTransform rectTransB;

        public Transform transA;
        public Transform transB;

        private Vector3 PointA
        {
            get { return WorldPosToSceen(transA.position); }
        }
        private Vector3 PointB
        {
            get { return WorldPosToSceen(transB.position); }
        }

        public Image img;
        public float lineWidth = 0.5f;

        private RectTransform ImageRectTrans
        {
            get { return img.GetComponent<RectTransform>(); }
        }

        void Update()
        {
            // DrawLineByWorldTrans();
            DrawLineByUITrans();
        }

        Vector3 WorldPosToSceen(Vector3 pos)
        {
            return Camera.main.WorldToScreenPoint(pos);
        }

        void DrawLineByWorldTrans()
        {
            DrawLine(PointA, PointB);
        }

        void DrawLineByUITrans()
        {
            DrawLine(rectTransA.position, rectTransB.position);
        }

        void DrawLine(Vector3 posA, Vector3 posB)
        {
            Vector3 differenceVector = posB - posA;

            // 设置线的长度（Width）
            ImageRectTrans.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
            // 从左向右划线
            ImageRectTrans.pivot = new Vector2(0, 0.5f);
            // 设置线的起始点
            ImageRectTrans.position = posA;
            // 设置线相对于水平向右方向的角度
            float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
            ImageRectTrans.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
     
}
