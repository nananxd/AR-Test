using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpawnerInteractor : MonoBehaviour
{
    [SerializeField] private XRRayInteractor arInteractor;
    [SerializeField] private Spawner uiSpawner;
    [SerializeField] private Vector3 spawnPt,spawnNormal;
    [SerializeField] private bool isInPlacementMode;

    public Vector3 SpawnPt { get { return spawnPt; } }
    public Vector3 SpawnNormal { get { return spawnNormal; } }
    public bool IsInPlacementMode { get { return isInPlacementMode; } set { isInPlacementMode = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var isPointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(-1);
        if (!isPointerOverUI && arInteractor.TryGetCurrentARRaycastHit(out var arRaycastHit))
        {
            if (!(arRaycastHit.trackable is ARPlane arPlane))
                return;

            if (arPlane.alignment != PlaneAlignment.HorizontalUp)
                return;

            //m_ObjectSpawner.TrySpawnObject(arRaycastHit.pose.position, arPlane.normal);
            if (!isInPlacementMode)
            {
                spawnPt = arRaycastHit.pose.position;
                spawnNormal = arPlane.normal;
                uiSpawner.ShowUI(true);
            }
           
        }

        return;
    }

    public void SetPlacementMode(bool isInPlacement)
    {
        isInPlacementMode = isInPlacement;
    }
}
