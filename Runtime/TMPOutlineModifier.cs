using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPOutlineModifier : MonoBehaviour
{
    private List<Material> garbageMaterials = new();
    private TextMeshProUGUI textTarget;

    [SerializeField] private bool setOutlineColorOnAwake = true;
    [SerializeField] private bool changeFaceDilate = false;
    [SerializeField] private bool changeOutlineThickness = false;
    [SerializeField] private bool changeUnderlayColor = false;
    [SerializeField, ColorUsage(true, true)] private Color32 outlineColor;
    [SerializeField, ColorUsage(true, true)] private Color32 underlayColor;
    [SerializeField, Range(-1, 1)] private float faceDilate;
    [SerializeField, Range(0, 1)] private float thickness;

    private void Awake()
    {
        if (setOutlineColorOnAwake)
        {
            SetOutlineColor(outlineColor);
        }
        if (changeOutlineThickness)
        {
            SetOutlineWidth(0);
        }
        if (changeFaceDilate)
        {
            SetFaceDilate(0);
        }
        if (changeUnderlayColor)
        {
            SetUnderlayColor(underlayColor);
        }
    }

    private void Start()
    {
        if (changeOutlineThickness)
        {
            SetOutlineWidth(thickness);
        }
        if (changeFaceDilate)
        {
            SetFaceDilate(faceDilate);
        }
    }

    private void OnDestroy()
    {
        while (garbageMaterials.Count > 0)
        {
            Destroy(garbageMaterials[0]);
            garbageMaterials.RemoveAt(0);
        }
    }

    public void SetOutlineColor(Color32 newColor)
    {
        GetTextTarget();
        textTarget.fontMaterial.SetColor("_OutlineColor", newColor);
        textTarget.ForceMeshUpdate();
    }

    public void SetUnderlayColor(Color32 newColor)
    {
        GetTextTarget();
        textTarget.fontMaterial.SetColor("_UnderlayColor", newColor);
        textTarget.ForceMeshUpdate();
    }

    public void SetOutlineWidth(float thickness)
    {
        GetTextTarget();
        thickness = Mathf.Min(thickness, 1);
        textTarget.fontMaterial.SetFloat("_OutlineWidth", thickness);
        textTarget.ForceMeshUpdate();
    }

    public void SetFaceDilate(float dilate)
    {
        GetTextTarget();
        dilate = Mathf.Min(dilate, 1);
        textTarget.fontMaterial.SetFloat("_FaceDilate", dilate);
        textTarget.ForceMeshUpdate();
    }

    private void GetTextTarget()
    {
        if (textTarget == null)
        {
            textTarget = GetComponent<TextMeshProUGUI>();
            garbageMaterials.Add(textTarget.fontMaterial);
        }
    }
}
