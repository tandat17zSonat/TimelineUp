using UnityEngine;

namespace HyperCasualRunner.Locomotion
{
    /// <summary>
    /// Base class for making custom movement constrains. You can derive this and override the methods to create your own custom behaviour.
    /// </summary>
    public abstract class MovementConstrainerBase : ScriptableObject
    {
        public virtual void Initialize(GameObject runnerGameObject)
        {

        }

        public virtual void OnDestroying()
        {

        }

        public abstract Vector3 GetConstrainedPosition(Vector3 position);
    }
}
