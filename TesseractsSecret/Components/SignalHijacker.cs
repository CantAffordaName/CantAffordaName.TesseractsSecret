using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components
{
    internal class SignalHijacker : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject escapePod1Signal = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_EscapePodCrashSite/Sector_CrashFragment/Interactables_CrashFragment/VisibleFrom_EscapePodCrashSite/DistressBeaconTransmitter/Signal_EscapePod");
        public GameObject escapePod2Signal = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Sector_SouthHemisphere/Sector_EscapePod/Interactables_EscapePod/EscapePod_VisibleFrom_Far/DistressBeaconTransmitter/Signal_EscapePod");
        public GameObject escapePod3Signal = SearchUtilities.Find("DarkBramble_Body/Sector_DB/Interactables_DB/EntranceWarp_ToHub/Signal_EscapePod");
        public GameObject tesseractEscapePod = SearchUtilities.Find("Tesseract_Body/Sector/EscapePod_Socket");

        public bool isHijacked = false;

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
            {
                TesseractsSecret.Log("Player triggered Signal Hijacker!");
                isHijacked = true;
            }
        }

        private void Update()
        {
            if(isHijacked)
            {
                escapePod1Signal.transform.position = tesseractEscapePod.transform.position;
                escapePod2Signal.transform.position = tesseractEscapePod.transform.position;
                escapePod3Signal.transform.position = tesseractEscapePod.transform.position;
            }
        }

        public static SignalHijacker MakeHijackerVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("SignalHijacker");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var hijackVolume = volume.AddComponent<SignalHijacker>();

            return hijackVolume;
        }
    }
}
