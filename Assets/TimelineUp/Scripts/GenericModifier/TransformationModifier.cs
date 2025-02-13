using System;

public class TransformationModifier : GenericModifier<TransformationModifiable>
{
    public override void Apply(TransformationModifiable modifiable)
    {
        //modifiable.ListEntitySprites = GameplayManager.Instance.TimelineEraSO.entitySprites;
    }
}