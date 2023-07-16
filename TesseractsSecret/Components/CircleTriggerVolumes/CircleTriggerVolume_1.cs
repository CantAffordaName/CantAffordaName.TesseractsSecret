using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components.CircleTriggerVolumes
{
    internal class CircleTriggerVolume_1 : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public bool canBeTriggered = false;
        public bool hasBeenTriggered = false;

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody && canBeTriggered)
            {
                TesseractsSecret.Log("Player entered CircleTriggerVolume_1!");
                hasBeenTriggered = true;

                GameObject circleTriggerVolume2 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_2");
                GameObject circleTriggerVolume3 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_3");
                GameObject circleTriggerVolume4 = SearchUtilities.Find("Tesseract_Body/CircleTriggerVolume_4");

                if (circleTriggerVolume2.GetComponent<CircleTriggerVolume_2>().hasBeenTriggered && circleTriggerVolume3.GetComponent<CircleTriggerVolume_3>().hasBeenTriggered && circleTriggerVolume4.GetComponent<CircleTriggerVolume_4>().hasBeenTriggered)
                {
                    DisableMonolith();
                }
            }
        }

        private void DisableMonolith()
        {
            GameObject monolith = SearchUtilities.Find("Tesseract_Body/Sector/OuterPlanet_Grass/TesseractOuterPlanetv2/Monolith");
            monolith.SetActive(false);
        }

        public static CircleTriggerVolume_1 MakeTriggerVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("CircleTriggerVolume_1");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var triggerVolume = volume.AddComponent<CircleTriggerVolume_1>();

            return triggerVolume;
        }
    }
}
