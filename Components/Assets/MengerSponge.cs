using BaseX;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrooxEngine
{

	public class MengerSponge : MeshXShape
	{
		private List<int> indices = new List<int>();
		public int depth;

		public MengerSponge(MeshX mesh, int depth) : base(mesh)
		{
			this.depth = depth;
			float3 p1 = new float3(0, 0, 0);
			float3 p2 = new float3(1, 1, 1);
			var count = MathX.FloorToInt(MathX.Pow(20, depth));
			UniLog.Log("Set the vertex count to " + count);
			mesh.SetVertexCount(count);
			generateMengerSponge(p1, p2, depth, ref mesh);
			mesh.RecalculateNormals();
			mesh.RecalculateTangents();
		}

		private void generateMengerSponge(float3 pStart, float3 pEnd, int depth, ref MeshX mesh)
		{
			if (depth == 0)
			{
				addCube(pStart, pEnd, ref mesh);
				return;
			}
			depth--;
			float length = pEnd.x - pStart.x;

			float3 endOffset = float3.One * length / 3.0f;

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						if ((x == 1 && y == 1) || (x == 1 && z == 1) || (y == 1 && z == 1))
						{
							continue;
						}
						float3 newStart = pStart + float3.Right * length * (x / 3.0f) + float3.Forward * length * (z / 3.0f) + float3.Up * length * (y / 3.0f);
						generateMengerSponge(newStart, newStart + endOffset, depth, ref mesh);
					}
				}
			}
		}

		
		private void addCube(float3 pStart, float3 pEnd, ref MeshX mesh)
		{
			float length = pEnd.x - pStart.x;
			float3 p1 = pStart;
			float3 p2 = pStart + float3.Right * length;
			float3 p3 = pStart + float3.Forward * length + float3.Right * length;
			float3 p4 = pStart + float3.Forward * length;

			float3 p5 = pEnd - float3.Forward * length - float3.Right * length;
			float3 p6 = pEnd - float3.Forward * length;
			float3 p7 = pEnd;
			float3 p8 = pEnd - float3.Right * length;

			addQuad(p1, p4, p3, p2, ref mesh);
			addQuad(p7, p6, p2, p3, ref mesh);
			addQuad(p7, p3, p4, p8, ref mesh);
			addQuad(p1, p5, p8, p4, ref mesh);
			addQuad(p1, p2, p6, p5, ref mesh);
			addQuad(p7, p8, p5, p6, ref mesh);
			return;
		}

		private void addQuad(float3 p1, float3 p2, float3 p3, float3 p4, ref MeshX mesh)
		{
			// 1
			int p1Index = mesh.VertexCount;
			indices.Add(mesh.VertexCount);
			mesh.AddVertex(p1);

			int x = mesh.VertexCount;
			indices.Add(mesh.VertexCount);
			mesh.AddVertex(p2);

			int y = mesh.VertexCount;
			int p3Index = mesh.VertexCount;
			indices.Add(mesh.VertexCount);
			mesh.AddVertex(p3);

			mesh.AddTriangle(x, y, mesh.VertexCount);

			// 2
			indices.Add(p1Index);
			indices.Add(p3Index);
			indices.Add(mesh.VertexCount);
			mesh.AddTriangle(p1Index, p3Index, mesh.VertexCount);
			mesh.AddVertex(p4);
		}

		public override void Update()
		{
			Mesh.RecalculateNormals(AllTriangles);
			Mesh.RecalculateTangents(AllTriangles);
		}
	}
}


