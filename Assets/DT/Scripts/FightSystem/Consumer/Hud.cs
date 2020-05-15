using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using System;

[Serializable]
public class Hud :  MonoBehaviour
{
    private TextMesh font;
    private MeshFilter filter;
    private Vector3[] vertices;
    private Color[] colors;
    private int[] triangles;
    float x = 0.25f, y = 0.05f, z = 0f;
    private string _txt = "";
    private float _hud = 0f;
    public Material _material;
    private ConsumeSign targetConsumer;

    public void Init(ConsumeSign sign)
    {
        _txt = sign.consumeData.consumerName;
        _hud = 0.5f;
        targetConsumer = sign;
        CreateBlood();
    }

    private void CreateBlood()
    {
        //hud = new GameObject("hud");
        //hud.transform.DOLookAt(Camera.main.transform.position,0f);
        //hud.transform.SetParent(targetConsumer.transform);
        //hud.transform.localPosition = Vector3.zero + new Vector3(0, 2f, 0);
        //hud.transform.position = new Vector3(0, 1, 0);
        filter = gameObject.AddComponent<MeshFilter>();
        var rend = gameObject.AddComponent<MeshRenderer>();

        vertices = new Vector3[8];
        colors = new Color[8];
        triangles = new int[12];

        vertices[0].Set(-x, y, z);
        vertices[2].Set(-x, -y, z);
        vertices[5].Set(x, y, z);
        vertices[7].Set(x, -y, z);
        UpdateHud(_hud);

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                colors[4 * j + i] = j == 1 ? Color.red : Color.grey;
            }
        }

        int currentCount = 0;
        for (int i = 0; i < 8; i = i + 4)
        {
            triangles[currentCount++] = i;
            triangles[currentCount++] = i + 3;
            triangles[currentCount++] = i + 1;

            triangles[currentCount++] = i;
            triangles[currentCount++] = i + 2;
            triangles[currentCount++] = i + 3;
        }

        filter.mesh.vertices = vertices;
        filter.mesh.triangles = triangles;
        filter.mesh.colors = colors;
        rend.material = _material;
        rend.shadowCastingMode = ShadowCastingMode.Off;
        rend.receiveShadows = false;
        rend.lightProbeUsage = LightProbeUsage.Off;
        rend.reflectionProbeUsage = ReflectionProbeUsage.Off;
    }


    private void CreateText()
    {
        //GameObject go = new GameObject("Name");
        //go.transform.SetParent(hud.transform);
        //go.transform.rotation = Quaternion.identity;
        //go.transform.localScale = 0.1f * Vector3.one;
        //go.transform.localPosition = new Vector3(0, 0.5f, 0);
        //font = go.AddComponent<TextMesh>();
        //font.text = _txt;
        //font.fontSize = 36;
        //font.alignment = TextAlignment.Center;
        //font.anchor = TextAnchor.MiddleCenter;
        //font.color = Color.black;
    }


    public void UpdateInfo(string txt)
    {
        font.text = txt;
    }

    public void UpdateHud(float v)
    {
        float val = Mathf.Lerp(-x, x, 1 - v);
        vertices[1].Set(val, y, z);
        vertices[3].Set(val, -y, z);
        vertices[4] = vertices[1];
        vertices[6] = vertices[3];
        filter.mesh.vertices = vertices;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}