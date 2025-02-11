using UnityEngine;


public class AnimationModifier : GenericModifier<AnimationModifiable>
{
    [SerializeField, Tooltip("Name of the parameter in Animator.")] public string IdleAnimationName = "IdleLegacy";
    [SerializeField, Tooltip("Name of the parameter in Animator.")] public string RunAnimationName = "RunLegacy";
    [SerializeField, Tooltip("Name of the parameter in Animator.")] public string JumpAnimationName = "JumpLegacy";

    public string CurrentAnimationName { get; set; }

    public override void Apply(AnimationModifiable modifiable)
    {
        modifiable.Play(CurrentAnimationName);
    }
}
