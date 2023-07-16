using OWML.Common;
using OWML.ModHelper;
using UnityEngine;
using TesseractsSecret.Utilities.ModAPIs;
using TesseractsSecret.Components.NodeRepositionVolumes;
using TesseractsSecret.Components.TesseractChangeVolumes;
using TesseractsSecret.Components.CircleTriggerVolumes;
using NewHorizons.Utility;
using TesseractsSecret.Components;
//using NewHorizons.Utility;

namespace TesseractsSecret;

public class TesseractsSecret : ModBehaviour
{
    public static TesseractsSecret Instance;

    public static INewHorizons newHorizonsAPI;

    public GameObject tesseract;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Starting here, you'll have access to OWML's mod helper.
        ModHelper.Console.WriteLine($"My mod {nameof(TesseractsSecret)} is loaded!", MessageType.Success);

        // Get the New Horizons API and load configs
        var newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
        newHorizons.LoadConfigs(this);

        // Example of accessing game code.
        LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
        {
            if (loadScene != OWScene.SolarSystem) return;
            ModHelper.Console.WriteLine("Loaded into solar system! Tesseract's Secret test!", MessageType.Success);
        };

        newHorizons.GetStarSystemLoadedEvent().AddListener(OnStarSystemLoaded); ;
    }

    private void OnStarSystemLoaded(string systemName)
    {
        ModHelper.Console.WriteLine("Calling GetStarSys and OnStarSys, checking for Tesseract", MessageType.Success);

        var newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
        GameObject tesseractPlanet = newHorizons.GetPlanet("Tesseract");
        GameObject interiorDimension = GameObject.Find("InteriorDimension_Body");
        GameObject timberHearthPlanet = GameObject.Find("TimberHearth_Body");

        if (tesseractPlanet != null && interiorDimension != null)
        {
            ModHelper.Console.WriteLine("Tesseract was found!", MessageType.Success);

            //Volumes for moving Nodes
            NodeRepositionVolume_1.MakeNodeRepositionVolume(tesseractPlanet, new Vector3(-36.58f, 129.45f, 68.01f), 80f);
            NodeRepositionVolume_2.MakeNodeRepositionVolume(tesseractPlanet, new Vector3(-119.5f, -25.03f, -58.05f), 80f);
            NodeRepositionVolume_3.MakeNodeRepositionVolume(tesseractPlanet, new Vector3(97.06f, -60.32f, 98.56f), 80f);
            NodeRepositionVolume_4.MakeNodeRepositionVolume(tesseractPlanet, new Vector3(86.65f, 102.46f, 13.61f), 40f);

            //Volumes for manipulating Tesseract
            tesseract = SearchUtilities.Find("InteriorDimension_Body/Sector/TesseractCombinedShapes");
            TesseractRotation tesseractRotation = tesseract.AddComponent<TesseractRotation>();
            TesseractChangeVolume_1.MakeTesseractChangeVolume(interiorDimension, new Vector3(-147.1187f, 312.1214f, -1045.452f), new Vector3(3, 3, 3));
            TesseractChangeVolume_2.MakeTesseractChangeVolume(interiorDimension, new Vector3(-130.9283f, 318.4645f, -1045.718f), new Vector3(3, 3, 3));

            //Volumes for circle puzzle
            CircleTriggerVolume_1.MakeTriggerVolume(tesseractPlanet, new Vector3(0.0219698f, 131.6165f, 10.83248f), 2.5f);
            CircleTriggerVolume_2.MakeTriggerVolume(tesseractPlanet, new Vector3(10.79503f, 131.6165f, 0.01568449f), 2.5f);
            CircleTriggerVolume_3.MakeTriggerVolume(tesseractPlanet, new Vector3(0.01446295f, 131.6165f, -10.86572f), 2.5f);
            CircleTriggerVolume_4.MakeTriggerVolume(tesseractPlanet, new Vector3(-10.78989f, 131.6165f, -0.03431129f), 2.5f);

            //Hide Desert Planet
            GameObject desertPlanet = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Desert");
            desertPlanet.SetActive(false);
            GameObject sandSphere = SearchUtilities.Find("Tesseract_Body/Sector/Sand");
            sandSphere.SetActive(false);

            //Volume for hiding cloak
            CloakDestroyerVolume.MakeDestructionVolume(tesseractPlanet, new Vector3(0, 0, 0), 350);

            //Volume for hijacking signal
            SignalHijacker.MakeHijackerVolume(timberHearthPlanet, new Vector3(-27.96963f, -188.83f, 0.8572062f), 100f);
        }
        else
        {
            ModHelper.Console.WriteLine("Couldn't find Tesseract!", MessageType.Error);
        }
    }

    public static void Log(string msg) =>
        Instance.ModHelper.Console.WriteLine($"Info: {msg}", MessageType.Info);

    public static void LogError(string msg) =>
        Instance.ModHelper.Console.WriteLine($"Error: {msg}", MessageType.Error);
}