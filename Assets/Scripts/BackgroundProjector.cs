using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundProjector : MonoBehaviour
{
   public GameObject Model;
   public Quaternion rotation;
   public Vector3 scale;
   public Vector3 position;

   void OnEnable()
   {
      EditorApplication.update += UpdateTexture;
   }
   void OnDisable()
   {
      EditorApplication.update -= UpdateTexture;
   }

   [ContextMenu("projectTexture")]
   public void UpdateTexture()
   {

      Matrix4x4 vp = Camera.main.projectionMatrix * Camera.main.cameraToWorldMatrix;
      vp.SetTRS(vp.GetPosition() + position, vp.rotation * rotation, vp.lossyScale + scale);

      MeshFilter[] mesheFilters = Model.GetComponentsInChildren<MeshFilter>();
      foreach (MeshFilter meshFilter in mesheFilters)
      {
         int size = meshFilter.sharedMesh.vertices.Length;
         Vector2[] uvs = new Vector2[size];
         for (int i = 0; i < size; i++)
         {
            uvs[i] = vertexToUVPosition(vp, meshFilter, i);
         }
         meshFilter.sharedMesh.SetUVs(0, uvs);
      }
   }

   private Vector2 vertexToUVPosition(Matrix4x4 vp, MeshFilter meshFilter, int index)
   {
      Vector3 vertex = meshFilter.sharedMesh.vertices[index];
      Vector4 worldPos = new Vector4(vertex.x, vertex.y, vertex.z, 1f);
      Vector4 clipPos = vp * worldPos;
      clipPos.Scale(new Vector3(1, .5f, 1));
      return clipPos;
   }
}
