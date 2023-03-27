using System;
using BaseX;
using FrooxEngine;
using NEOSPlus.Shaders;
//added this so i can expand and make my own toon shader as all ones everyone currently use are a pain.
//basically to avoid using mtoon or xixe or poyomi make your own.
[Category(new string[] { "Assets/Materials/Toon" })]
public class NeosPlusToon : SingleShaderMaterialProvider
{
    protected override Uri ShaderURL => ShaderInjection.NeosPlusToon;

    public readonly AssetRef<ITexture2D> MainTex;
    public readonly AssetRef<ITexture2D> MatcapTex;
    public readonly Sync<color> Color;
    [Range(0.01f, 1f, "0.01")]
    public readonly Sync<float> Shininess;
    [Range(0.01f, 1f, "0.01")]
    public readonly Sync<float> MatcapIntensity;

    private static MaterialProperty _MainTex = new MaterialProperty("_MainTex");
    private static MaterialProperty _MatcapTex = new MaterialProperty("_MatcapTex");
    private static MaterialProperty _Color = new MaterialProperty("_Color");
    private static MaterialProperty _Shininess = new MaterialProperty("_Shininess");
    private static MaterialProperty _MatcapIntensity = new MaterialProperty("_MatcapIntensity");

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
        material.UpdateTexture(_MatcapTex, MatcapTex);
        material.UpdateColor(_Color, Color);
        material.UpdateFloat(_Shininess, Shininess);
        material.UpdateFloat(_MatcapIntensity, MatcapIntensity);

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
        Shininess.Value = 0.078f;
        MatcapIntensity.Value = 1f;
    }
}
