using NewHorizons.Utility;
using System.Collections;
using TesseractsSecret.Components.CircleTriggerVolumes;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components.TesseractChangeVolumes
{
    internal class TesseractChangeVolume_1 : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject tesseract = SearchUtilities.Find("InteriorDimension_Body/Sector/TesseractCombinedShapes");
        public GameObject grassPlanet = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Grass/TesseractOuterPlanetv2");
        public GameObject desertPlanet = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Desert");
        public GameObject sandSphere = SearchUtilities.Find("Tesseract_Body/Sector/Sand");

        public SkinnedMeshRenderer meshRenderer;
        public float animationDuration = 5f;
        public float rotationSpeed = 8f;
        //public Color32 targetColor = new Color32(143, 235, 255, 255);
        public Color32 targetColor = new Color32(255, 247, 203, 255);

        public Material material;
        public Color32 initialColor;

        public bool hasBeenTriggered = false;

        public virtual void Start()
        {
            meshRenderer = tesseract.GetComponent<SkinnedMeshRenderer>();
            meshRenderer.SetBlendShapeWeight(0, 0f);
            material = meshRenderer.material;
            initialColor = material.color;
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody && !hasBeenTriggered)
            {
                GameObject circleTriggerVolume1 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_1");
                GameObject circleTriggerVolume2 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_2");
                GameObject circleTriggerVolume3 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_3");
                GameObject circleTriggerVolume4 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_4");

                GameObject nodeDoor_1 = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Grass/TeleportNodev2 (1)/NodeDoor");
                GameObject nodeDoor_2 = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Grass/TeleportNodev2 (2)/NodeDoor");
                GameObject nodeDoor_3 = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Grass/TeleportNodev2 (3)/NodeDoor");

                GameObject otherChangeVolume = SearchUtilities.Find("InteriorDimension_Body/TesseractChangeVolume_2");
                otherChangeVolume.GetComponent<TesseractChangeVolume_2>().hasBeenTriggered = false;
                TesseractsSecret.Log("Player entered TesseractChangeVolume_1!");
                StartCoroutine(AnimateBlendShape());
                hasBeenTriggered = true;

                //Switch from Grass to Desert
                grassPlanet.SetActive(false);
                desertPlanet.SetActive(true);
                sandSphere.SetActive(true);

                circleTriggerVolume1.GetComponent<CircleTriggerVolume_1>().canBeTriggered = true;
                circleTriggerVolume2.GetComponent<CircleTriggerVolume_2>().canBeTriggered = true;
                circleTriggerVolume3.GetComponent<CircleTriggerVolume_3>().canBeTriggered = true;
                circleTriggerVolume4.GetComponent<CircleTriggerVolume_4>().canBeTriggered = true;

                nodeDoor_1.SetActive(false);
                nodeDoor_2.SetActive(false);
                nodeDoor_3.SetActive(true);
            }
        }

        private IEnumerator AnimateBlendShape()
        {
            //Lerp the blend shape weight from 0 to 100
            float startTime = Time.time;
            meshRenderer.SetBlendShapeWeight(0, 0);
            float startWeight = meshRenderer.GetBlendShapeWeight(0);
            float targetWeight = 100f;

            //Lerp the material color from initial color to target color
            Color32 startColor = initialColor;

            while (Time.time < startTime + animationDuration)
            {
                float elapsedTime = Time.time - startTime;
                float lerpProgress = elapsedTime / animationDuration;

                //Lerp the blend shape weight
                float blendWeight = Mathf.Lerp(startWeight, targetWeight, lerpProgress);
                meshRenderer.SetBlendShapeWeight(0, blendWeight);

                //Lerp the material color
                Color32 lerpedColor = Color32.Lerp(startColor, targetColor, lerpProgress);
                material.color = lerpedColor;

                yield return null;
            }

            //Ensure the target weight and color are set precisely
            meshRenderer.SetBlendShapeWeight(0, targetWeight);
            material.color = targetColor;
        }

        public static TesseractChangeVolume_1 MakeTesseractChangeVolume(GameObject planet, Vector3 pos, Vector3 size)
        {
            var volume = new GameObject("TesseractChangeVolume_1");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var box = volume.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.size = size;

            var changeVolume = volume.AddComponent<TesseractChangeVolume_1>();

            return changeVolume;
        }
    }
}