using UnityEngine;

namespace Motion
{
    public class MotionAutoStep : MonoBehaviour
    {
#region Singleton
        private static MotionAutoStep _instance;

        private void Start()
        {
            if (_instance != null && _instance.GetInstanceID() != GetInstanceID())
            {
                var otherGameObject = _instance.gameObject;
                var hasOtherComponents = false;
                for (int i = 0, n = otherGameObject.GetComponentCount(); i < n; i++)
                {
                    if(otherGameObject.GetComponentAtIndex(i) is MotionAutoStep or Transform)continue;
                    hasOtherComponents = true;
                    break;
                }

                if (hasOtherComponents)
                {
                    Destroy(_instance);
                } else {
                    Destroy(otherGameObject);
                }
            }
            
            _instance = this;
            
            DontDestroyOnLoad(this);
        }
#endregion
        
        private void LateUpdate() => DoMotion.Step(Time.deltaTime);
    }
}