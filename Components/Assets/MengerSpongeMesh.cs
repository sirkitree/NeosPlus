using BaseX;

namespace FrooxEngine
{
	[Category(new string[] { "Assets/Procedural Meshes" })]
	public class MengerSpongeMesh : ProceduralMesh
	{
		[Range(1, 9)]
		public readonly Sync<int> depth;
		private MengerSponge Sponge;
		private int _depth;

		protected override void OnAwake()
		{
			base.OnAwake();
			depth.Value = 1;
		}

		protected override void PrepareAssetUpdateData()
		{
			_depth = depth.Value;
		}

		protected override void ClearMeshData()
		{
			Sponge = null;
		}

		protected override void UpdateMeshData(MeshX meshx)
		{
			bool value = false;
			if (Sponge == null || Sponge.depth != _depth)
			{
				Sponge?.Remove();
				Sponge = new MengerSponge(meshx, _depth);
				value = true;
			}
			Sponge.depth = depth.Value;
			Sponge.Update();
			uploadHint[MeshUploadHint.Flag.Geometry] = value;
		}
	}
}