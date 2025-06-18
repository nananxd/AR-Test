using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GLTFast;
//using Siccity.GLTFUtility;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using TMPro;

public class TestLoad : MonoBehaviour
{
    public TextMeshProUGUI debugTxt;
    string path = "https://github.com/KhronosGroup/glTF-Sample-Models/blob/main/2.0/Avocado/glTF/Avocado.gltf";
    string prefabAddress = "Box";
    //Start is called before the first frame update
    async void Start()
    {
        //ImportGLTF(path);
        #region
        var gltf = new GLTFast.GltfImport();

        // Create a settings object and configure it accordingly
        var settings = new ImportSettings
        {
            GenerateMipMaps = true,
            AnisotropicFilterLevel = 3,
            NodeNameMethod = NameImportMethod.OriginalUnique
        };
        // Load the glTF and pass along the settings
        var success = await gltf.Load("https://github.com/KhronosGroup/glTF-Sample-Models/blob/main/2.0/BarramundiFish/glTF-Binary/BarramundiFish.glb", settings);
        //var success = await gltf.Load("https://github.com/KhronosGroup/glTF-Sample-Models/raw/refs/heads/main/2.0/Avocado/glTF-Binary/Avocado.glb",settings);

        if (success)
        {
            var gameObject = new GameObject("glTF");
            await gltf.InstantiateMainSceneAsync(gameObject.transform);
            //gameObject.AddComponent<>
        }
        else
        {
            Debug.LogError("Loading glTF failed!");
        }
        #endregion
    }

    //private void Start()
    //{

    //}

    public void OnModelSelect(string address)
    {
        Addressables.LoadAssetAsync<GameObject>(address).Completed += OnModelLoaded;
    }

    void OnModelLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            debugTxt.text = $".{handle.Status}";
            Transform trans = Camera.main.transform;
            var model =  Instantiate(handle.Result);
            model.transform.position = trans.position + trans.forward * 1f;
            model.transform.rotation = Quaternion.LookRotation(-trans.forward);
        }
        else
        {
            Debug.LogError("Failed to load Addressable prefab.");
            debugTxt.text = $"Failed to load Addressable prefab.{handle.Status}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ImportGLTF(string filepath)
    {
        //GameObject result = Importer.LoadFromFile(filepath);
        //Debug.Log("LoadSuccess");
    }
}
