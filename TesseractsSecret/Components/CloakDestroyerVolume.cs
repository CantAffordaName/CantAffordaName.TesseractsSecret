using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components
{
    internal class CloakDestroyerVolume : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject cloakField = SearchUtilities.Find("Tesseract_Body/Sector/CloakingField");

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
            {
                TesseractsSecret.Log("Player entered CloakDestroyerVolume!");
                cloakField.SetActive(false);
            }
        }

        public static CloakDestroyerVolume MakeDestructionVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("CloakDestroyerVolume");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var destructionVolume = volume.AddComponent<CloakDestroyerVolume>();

            return destructionVolume;
        }
    }
}
