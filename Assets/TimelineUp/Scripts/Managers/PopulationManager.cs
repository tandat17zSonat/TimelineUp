using System;
using System.Collections;
using System.Collections.Generic;
using DarkTonic.PoolBoss;
using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [Header("")]
    [SerializeField] Transform entityPrefab;
    [SerializeField] Transform container;
    [SerializeField] Transform crowdOrganizingPoint;

    [Header("Organize")]
    [SerializeField] float organizeDurationInSeconds = 1f;
    [SerializeField] float entityMoveSpeed = 2f;

    private List<PopulatedEntity> _listEntityInCrowd;
    private List<PopulatedEntity> _listEntityOutsideCrowd;
    public List<PopulatedEntity> ListEntityInCrowd { get { return _listEntityInCrowd; } }
    public List<PopulatedEntity> ListEntityOutsideCrowd { get { return _listEntityOutsideCrowd; } }

    private bool _canMovePopulatedEntities;
    private WaitForSeconds _organizeWait;
    private Transform _moveTarget;
    private bool _shouldRotate;

    private Coroutine _organizeCoroutine;

    public Action PopulationChanged { get; set; }
    public float OrganizeDurationInSeconds { get { return organizeDurationInSeconds; } }


    private void Awake()
    {
        _canMovePopulatedEntities = true;
        _organizeWait = new WaitForSeconds(organizeDurationInSeconds);

        _moveTarget = crowdOrganizingPoint;

        _listEntityInCrowd = new List<PopulatedEntity>();
        _listEntityOutsideCrowd = new List<PopulatedEntity>();
    }

    public PopulatedEntity Spawn(int level, bool inCrowd = true)
    {
        var parent = inCrowd ? container : null;
        var spawned = PoolBoss.Spawn(entityPrefab, Vector3.zero, Quaternion.identity, parent);

        spawned.GetComponent<CapsuleCollider>().enabled = inCrowd ? true : false;

        float rndX = UnityEngine.Random.Range(-0.5f, 0.5f);
        float rndZ = UnityEngine.Random.Range(-0.5f, 0.5f);
        spawned.transform.localPosition = new Vector3(rndX, 0, rndZ);

        var entity = spawned.GetComponent<PopulatedEntity>();
        entity.Initialize(this);
        entity.SetInfo(level);
        entity.Appear();
        return entity;
    }

    public void AddToCrowd(PopulatedEntity entity)
    {
        _listEntityInCrowd.Add(entity);

        StartOrganizing();
        PopulationChanged?.Invoke();
    }

    public void AddToOutsideCrowd(PopulatedEntity entity)
    {
        _listEntityOutsideCrowd.Add(entity);
    }

    public void Remove(PopulatedEntity entity)
    {
        PoolBoss.Despawn(entity.transform);
    }
    public void RemoveEntityFromCrowd(PopulatedEntity entity)
    {
        _listEntityInCrowd.Remove(entity);
        entity.Disappear();
        PoolBoss.Despawn(entity.transform);
    }


    public void Unload()
    {
        foreach (var entity in _listEntityInCrowd)
        {
            entity.Disappear();
            PoolBoss.Despawn(entity.transform);
        }

        _listEntityInCrowd.Clear();
    }

    // -----Các function để sắp xếp lại đám đông----------------------------------------------------------------------------------
    public void StartOrganizing()
    {
        KillOrganizingCalls();
        _organizeCoroutine = StartCoroutine(Co_Organize());
    }

    private IEnumerator Co_Organize()
    {
        ResetEntityRotations();
        Move(crowdOrganizingPoint);

        yield return _organizeWait;

        StopEntitiesMovement();
    }

    void StopEntitiesMovement()
    {
        _canMovePopulatedEntities = false;
        foreach (PopulatedEntity entity in _listEntityInCrowd)
        {
            entity.DisablePhysicsInteraction();
        }
    }

    void Move(Transform target)
    {
        _moveTarget = target;
        _canMovePopulatedEntities = true;
    }

    void ResetEntityRotations()
    {
        _shouldRotate = false;
        foreach (PopulatedEntity entity in _listEntityInCrowd)
        {
            entity.ResetVisualRotation();
        }
    }

    private void OnDestroy()
    {
        KillOrganizingCalls();
    }

    private void KillOrganizingCalls()
    {
        if (_organizeCoroutine != null) StopCoroutine(_organizeCoroutine);
    }

    /// <summary>
    /// Để tổ chức lại đám đông khi cho các entity đứng sát nhau khi có thêm entity mới hoặc bị xóa
    /// </summary>
    private void FixedUpdate()
    {
        if (!_canMovePopulatedEntities)
        {
            return;
        }

        // Cho các warrior đừng gần lại nhau
        foreach (PopulatedEntity entity in _listEntityInCrowd)
        {
            entity.Move(_moveTarget, entityMoveSpeed, _shouldRotate);
        }
    }
}
