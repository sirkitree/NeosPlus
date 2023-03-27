using System;
using BaseX;
using FrooxEngine;
using NEOSPlus.Shaders;

[Category(new string[] { "Assets/Materials/SoftOcclusion" })]
public class SoftOcclusionCutout : SingleShaderMaterialProvider
{
    protected override Uri ShaderURL => ShaderInjection.softocclusion;

    public readonly AssetRef<ITexture2D> MainTex;
    [Range(0f, 1f, "0.00")]
    public readonly Sync<float> Cutout;
    [Range(0f, 1f, "0.00")]
    public readonly Sync<float> Softness;
    public readonly Sync<color> ShadowColor;
    [Range(0f, 1f, "0.00")]
    public readonly Sync<float> ShadowAmount;

    private static MaterialProperty _MainTex = new MaterialProperty("_MainTex");
    private static MaterialProperty _Cutout = new MaterialProperty("_Cutout");
    private static MaterialProperty _Softness = new MaterialProperty("_Softness");
    private static MaterialProperty _ShadowColor = new MaterialProperty("_ShadowColor");
    private static MaterialProperty _ShadowAmount = new MaterialProperty("_ShadowAmount");

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
        material.UpdateFloat(_Cutout, Cutout);
        material.UpdateFloat(_Softness, Softness);
        material.UpdateColor(_ShadowColor, ShadowColor);
        material.UpdateFloat(_ShadowAmount, ShadowAmount);

        if (!RenderQueue.GetWasChangedAndClear()) return;
        var renderQueue = RenderQueue.Value;
        if ((int)RenderQueue == -1) renderQueue = 2450;
        material.SetRenderQueue(renderQueue);
    }

    protected override void UpdateKeywords(ShaderKeywords keywords) { }
    protected override void OnAttach()
    {
        base.OnAttach();
        Cutout.Value = 0.5f;
        Softness.Value = 0.1f;
        ShadowColor.Value = new color(0, 0, 0, 0.8f);
        ShadowAmount.Value = 0.5f;
    }
}
