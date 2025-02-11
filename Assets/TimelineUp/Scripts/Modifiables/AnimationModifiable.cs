using UnityEngine;

public class AnimationModifiable : BaseModifiable
{
    [SerializeField] TransformationModifiable _transformationModifiable;

    Animation _activeAnimation;
    string _playingAnimationName;

    public void Awake()
    {
        _transformationModifiable.Transformed += Transformator_Transformed;
    }

    void OnDestroy()
    {
        _transformationModifiable.Transformed -= Transformator_Transformed;
    }

    void Transformator_Transformed(GameObject obj)
    {
        _activeAnimation = obj.GetComponent<Animation>();

        //if (_playingAnimationName != null)
        //{
        //    Play(_playingAnimationName);
        //}
    }

    public override void Initialize(int level)
    {

    }

    public void Play(string animationName)
    {
        _activeAnimation.Play(animationName);
        _playingAnimationName = animationName;
    }
}