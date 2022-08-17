using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(FixedCameraAngle))]
public class FixedCameraAngleEditor : Editor
{
   public static GameObject backgroundObj;
   static RawImage backgroundImage;

   public override VisualElement CreateInspectorGUI()
   {
      if (EditorApplication.isPlaying) return base.CreateInspectorGUI();

      var cameraAngle = target as FixedCameraAngle;

      if (backgroundObj != null)
      {
         DestroyImmediate(backgroundObj);
      }

      var switcher = FindObjectOfType<FixedCameraSwitcher>();

      CinemachineBrain.SoloCamera = cameraAngle.GetComponent<CinemachineVirtualCamera>();

      switcher.ClearUnwantedDepthObjects(cameraAngle);

      backgroundObj = new GameObject("background");
      backgroundObj.transform.parent = switcher.backgroundContainer;
      backgroundImage = backgroundObj.AddComponent<RawImage>();
      backgroundImage.texture = cameraAngle.backgroundTexture;
      backgroundImage.transform.rotation = backgroundObj.transform.parent.rotation;
      backgroundImage.transform.localPosition = cameraAngle.backgroundOffset;
      backgroundImage.transform.localScale = cameraAngle.backgroundScale * cameraAngle.backgroundScaleFactor;
      backgroundImage.SetNativeSize();

      return base.CreateInspectorGUI();
   }

   public override void OnInspectorGUI()
   {
      if (EditorApplication.isPlaying)
      {
         base.OnInspectorGUI();
         return;
      }

      if (backgroundImage != null)
      {
         var cameraAngle = target as FixedCameraAngle;
         backgroundImage.transform.localPosition = cameraAngle.backgroundOffset;
         backgroundImage.transform.localScale = cameraAngle.backgroundScale * cameraAngle.backgroundScaleFactor;
      };

      base.OnInspectorGUI();
   }

   void OnDisable()
   {
      DestroyImmediate(backgroundObj);
   }
}
