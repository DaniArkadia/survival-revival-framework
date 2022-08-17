using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class FixedCameraAngle : MonoBehaviour
{
   public static event Action<FixedCameraAngle> OnFixedCameraChanged;
   public string stateName;
   public List<DepthObject> depthObjects;

   public Texture backgroundTexture;
   public Vector3 backgroundOffset;
   public Vector3 backgroundScale = Vector3.one;
   public float backgroundScaleFactor = 1f;

   [ContextMenu("SwitchToCameraView")]
   public void AttemptToSwitch()
   {
      OnFixedCameraChanged?.Invoke(this);
   }
}
