
using UnityEngine;

/* A baseclass that can be used for any MonoBehaviour that needs to act as a singleton. */
public abstract class Singleton : MonoBehaviour
{
   public static Singleton Instance { get; private set; }

   // Sets the singleton, this method should be called in awake.
   public void InitializeSingleton()
   {
      if (Instance != null)
      {
         throw new System.Exception("Multiple instances of Singleton type: " + this.GetType().ToString() + " detected.");
      }
      Instance = this;
   }
}