using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class Spawner : MonoBehaviour
{
    public GameObject ui,placementBtn;
    [SerializeField] private SpawnerInteractor spawnInteractor;
    [SerializeField] private List<GameObject> modelToSpawn;

    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    
    public void ShowUI(bool isActive)
    {
        ui.SetActive(isActive);
       
    }

    public void SpawnModel(int index)
    {
        var model = Instantiate(modelToSpawn[index]);

        model.transform.position = spawnInteractor.SpawnPt;

        var facePosition = mainCam.transform.position;
        var forward = facePosition - spawnInteractor.SpawnPt;
        BurstMathUtility.ProjectOnPlane(forward, spawnInteractor.SpawnNormal, out var projectedForward);
        model.transform.rotation = Quaternion.LookRotation(projectedForward, spawnInteractor.SpawnNormal);
        ShowUI(false);
        spawnInteractor.SetPlacementMode(true);
        placementBtn.gameObject.SetActive(true);
    }
}
