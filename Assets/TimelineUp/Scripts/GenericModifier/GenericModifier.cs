using UnityEngine;

public abstract class GenericModifier<T> : MonoBehaviour where T : BaseModifiable
{
    [SerializeField] PopulationManager _populationManager;

    public void ApplyAll()
    {
        foreach(var entity in _populationManager.ListEntityInCrowd)
        {
            T modifiable = entity.GetComponent<T>();
            Apply(modifiable);
        }
    }

    public abstract void Apply(T modifiable);
}