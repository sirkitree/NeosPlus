using System;
using BaseX;
using FrooxEngine;
using NEOSPlus.Shaders;
//added due to https://github.com/Neos-Metaverse/NeosPublic/issues/1338 Unlit Displacement this code does not currently work or function. - xlinka
[Category(new string[] { "Assets/Materials/UnlitDisplacement" })]
public class UnlitDisplacement : SingleShaderMaterialProvider
{
    protected override Uri ShaderURL => ShaderInjection.UnlitDisplacement;

    public readonly AssetRef<ITexture2D> MainTex;
    [Range(0f, 1f, "0.00")]
    public readonly Sync<float> Displacement;
    [Range(0.01f, 10f, "0.00")]
    public readonly Sync<float> Speed;
    public readonly Sync<color> Color;

    private static MaterialProperty _MainTex = new MaterialProperty("_MainTex");
    private static MaterialProperty _Displacement = new MaterialProperty("_Displacement");
    private static MaterialProperty _Speed = new MaterialProperty("_Speed");
    private static MaterialProperty _Color = new MaterialProperty("_Color");

   private static PropertyState _propertyInitializationState;

    public override PropertyState PropertyInitializationState
    {
        get => _propertyInitializationState;
        protected set => _propertyInitializationState = value;
    }

    protected override void UpdateMaterial(Material material)
    {
        material.UpdateTexture(_MainTex, MainTex);
        material.UpdateFloat(_Displacement, Displacement);
        material.UpdateFloat(_Speed, Speed);
        material.UpdateColor(_Color, Color);
    }
    protected override void UpdateKeywords(ShaderKeywords keywords) { }

    protected override void OnAttach()
    {
        base.OnAttach();
        Color.Value = new color(1f);
        Displacement.Value = 0.1f;
        Speed.Value = 1f;
    }
}
