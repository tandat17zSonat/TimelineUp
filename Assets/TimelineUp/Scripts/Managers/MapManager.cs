using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] int maxObject = 20;
    [SerializeField] Transform parent;

    [SerializeField, ReadOnly] private Transform _mapPrefab;
    [SerializeField, ReadOnly] private float _mapWidth;
    private List<Transform> _maps = new List<Transform>();

    public Transform MapPrefab { get { return _mapPrefab; } }

    public void SetPrefab(Transform mapPrefab, float mapWidth)
    {
        _mapPrefab = mapPrefab;
        _mapWidth = mapWidth;
    }

    public void BuildMap()
    {
        Clear();

        for (int i = 0; i < maxObject; i++)
        {
            var mapObj = Instantiate(_mapPrefab, parent);
            mapObj.transform.position = new Vector3(0, 0, 1) * i * _mapWidth;
            _maps.Add(mapObj);
        }
    }

    private void Clear()
    {
        foreach (Transform mapObj in parent)
        {
            Destroy(mapObj.gameObject);
        }
        _maps.Clear();
    }

}
