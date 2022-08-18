using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FixedCameraSwitcher : Singleton
{
   public RectTransform backgroundContainer;
   
   FixedCameraAngle currentAngle;
   Animator cameraState;

   RawImage spawnedBackground;

   public void ClearUnwantedDepthObjects(FixedCameraAngle exclude = null)
   {
      var depthObjects = FindObjectsOfType<DepthObject>(true).ToList();

      if (exclude != null)
      {
         depthObjects.ForEach((o) => 
         {
            if (exclude.depthObjects.Contains(o))
            {
               o.gameObject.SetActive(true);
            } 
            else
            {
               o.gameObject.SetActive(false);
            }
         });
      }
   }
   
   void Awake()
   {
      InitializeSingleton();
      cameraState = GetComponent<Animator>();

      // Disable all depth objects
      ClearUnwantedDepthObjects();
   }


   void Start()
   {
      OnRequestCameraChange(FindObjectOfType<FixedCameraAngle>());
   }

   void OnEnable() => FixedCameraAngle.OnFixedCameraChanged += OnRequestCameraChange;
   void OnDisable() => FixedCameraAngle.OnFixedCameraChanged -= OnRequestCameraChange;

   void OnRequestCameraChange(FixedCameraAngle cameraAngle)
   {
      if (currentAngle != null)
      {
         // hide the previous angle's depth objects.
         foreach (var depthObject in currentAngle.depthObjects)
         {
            depthObject.gameObject.SetActive(false);
            depthObject.LookAtCamera(cameraAngle.transform);
         }
      }
      currentAngle = cameraAngle;
      cameraState.Play(cameraAngle.stateName);

      foreach (Transform t in backgroundContainer)
      {
         Destroy(t.gameObject);
      }

      // Spawn background image
      var spawnedLayer = new GameObject(currentAngle.backgroundTexture.name);
      spawnedLayer.transform.position = backgroundContainer.transform.position;
      spawnedLayer.transform.rotation = backgroundContainer.transform.rotation;
      spawnedLayer.transform.parent = backgroundContainer;
      
      var image = spawnedLayer.AddComponent<RawImage>();
      spawnedBackground = image;
      spawnedBackground.texture = currentAngle.backgroundTexture;
      spawnedBackground.SetNativeSize();

      foreach (var depthObject in currentAngle.depthObjects)
      {
         depthObject.gameObject.SetActive(true);
      }
   }

   void Update()
   {
      if (spawnedBackground == null) return;

      spawnedBackground.rectTransform.localScale = currentAngle.backgroundScale * currentAngle.backgroundScaleFactor;
      spawnedBackground.rectTransform.localPosition = currentAngle.backgroundOffset;
   }
}
