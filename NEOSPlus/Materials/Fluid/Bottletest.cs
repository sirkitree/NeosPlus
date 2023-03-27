using System;
using BaseX;
using FrooxEngine;
using NEOSPlus.Shaders;

[Category(new string[] { "Assets/Materials/Water" })]
public class Water : SingleShaderMaterialProvider
{
    protected override Uri ShaderURL => ShaderInjection.water;

    public readonly AssetRef<ITexture2D> MainTex;
    public readonly Sync<color> Color;
    [Range(0f, 1f, "0.01")]
    public readonly Sync<float> CuttingPlane;

    private static MaterialProperty _MainTex = new MaterialProperty("_MainTex");
    private static MaterialProperty _Color = new MaterialProperty("_Color");
    private static MaterialProperty _CuttingPlane = new MaterialProperty("_CuttingPlane");

    [DefaultValue(-1)]
    public readonly Sync<int> RenderQueue;
    private static PropertyState _propertyInitializationState;

    public override PropertyState PropertyInitializationState
    {
        get => _propertyInitializationState;
        protected set => _propertyInitializationState = value;
    }

    protected override void UpdateMaterial(Material material)
    {
        material.UpdateTexture(_MainTex, MainTex);
        material.UpdateColor(_Color, Color);
        material.UpdateFloat(_CuttingPlane, CuttingPlane);

        if (!RenderQueue.GetWasChangedAndClear()) return;
        var renderQueue = RenderQueue.Value;
        if ((int)RenderQueue == -1) renderQueue = 2000;
        material.SetRenderQueue(renderQueue);
    }

    protected override void UpdateKeywords(ShaderKeywords keywords) { }
    protected override void OnAttach()
    {
        base.OnAttach();
        Color.Value = new color(1);
        CuttingPlane.Value = 0.5f;
    }
}
