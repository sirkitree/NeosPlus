using System;
using BaseX;
using FrooxEngine;
using NEOSPlus.Shaders;

[Category(new string[] { "Assets/Materials/NeosPlus/Effects" })]
public class RainMaterial : SingleShaderMaterialProvider
{
    protected override Uri ShaderURL => ShaderInjection.Rain;

    public readonly AssetRef<ITexture2D> MainTex;
    public readonly Sync<float> Speed;
    public readonly Sync<float> Density;
    public readonly Sync<color> Color;

    private static MaterialProperty _MainTex = new MaterialProperty("_MainTex");
    private static MaterialProperty _Speed = new MaterialProperty("_Speed");
    private static MaterialProperty _Density = new MaterialProperty("_Density");
    private static MaterialProperty _Color = new MaterialProperty("_Color");

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
        material.UpdateFloat(_Speed, Speed);
        material.UpdateFloat(_Density, Density);
        material.UpdateColor(_Color, Color);

        if (!RenderQueue.GetWasChangedAndClear()) return;
        var renderQueue = RenderQueue.Value;
        if ((int)RenderQueue == -1) renderQueue = 3000;
        material.SetRenderQueue(renderQueue);
    }

    protected override void UpdateKeywords(ShaderKeywords keywords) { }

    protected override void OnAttach()
    {
        base.OnAttach();
        Color.Value = new color(1);
        Speed.Value = 1;
        Density.Value = 1;
    }
}
