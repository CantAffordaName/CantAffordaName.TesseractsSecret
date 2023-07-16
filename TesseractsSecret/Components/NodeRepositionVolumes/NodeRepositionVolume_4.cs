using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components.NodeRepositionVolumes
{
    internal class NodeRepositionVolume_4 : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject node = SearchUtilities.Find("Tesseract_Body/Sector/EntranceNode");

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
            {
                TesseractsSecret.Log("Player entered NodeRepositionVolume_4!");
                node.transform.localPosition = new Vector3(86.65f, 102.46f, 13.61f);
                node.transform.localRotation = Quaternion.Euler(286, 251, 180);
            }
        }

        public static NodeRepositionVolume_4 MakeNodeRepositionVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("NodeRepositionVolume_4");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var teleportVolume = volume.AddComponent<NodeRepositionVolume_4>();

            return teleportVolume;
        }
    }
}