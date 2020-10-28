using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class DarkEffect : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] public Transform target;
        [SerializeField] public int radius;
        [SerializeField] public int EndRandius;
        [SerializeField] public float waitTime;
        [SerializeField] public int speed = 1;

        public Vector3 GetScreenPosition(Camera cam)
        {
             
                return cam.WorldToScreenPoint(target.position);
        }
    }

    //渐变像素数量
    public int _smoothLength = 20;

    //遮罩混合颜色
    public Color _darkColor = Color.black;

    //目标物体
    public List<Item> _items = new List<Item>();
    public Material _mainMaterial;
    protected Camera _mainCamera;
    Vector4[] _itemDatas;
    Item _tmpItem;
    Vector4 _tmpVt;
    Vector3 _tmpPos;
    int _tmpScreenHeight;

    private void Start()
    {
        //_mainMaterial = new Material(Shader.Find("Peter/DarkEffect"));
        _mainCamera = GetComponent<Camera>();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_itemDatas == null || _itemDatas.Length != _items.Count)
        {
            _itemDatas = new Vector4[_items.Count];
        }
//       Debug.Log(_itemDatas.Length +"_itemDatas.Length "+_items.Count+"_items.Count" );
        _tmpScreenHeight = Screen.height;
        for (int i = 0; i < _items.Count; i++)
        {
            _tmpItem = _items[i];
      
            _tmpPos = _tmpItem.GetScreenPosition(_mainCamera);
            _tmpVt.x = _tmpPos.x;
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                _tmpVt.y = (_tmpScreenHeight - _tmpPos.y);
            }
            else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
            {
                _tmpVt.y = _tmpPos.y;
            }
            _tmpVt.z = _tmpItem.radius;
            _tmpVt.w = 0;
            _itemDatas[i] = _tmpVt;
        }
    
        _mainMaterial.SetInt("_SmoothLength", _smoothLength);
        _mainMaterial.SetColor("_DarkColor", _darkColor);
        _mainMaterial.SetInt("_ItemCnt", _itemDatas.Length); 
        _mainMaterial.SetVectorArray("_Item", _itemDatas);
        Graphics.Blit(source, destination, _mainMaterial);
    
    }
}
 