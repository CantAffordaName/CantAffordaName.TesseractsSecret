using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components.NodeRepositionVolumes
{
    internal class NodeRepositionVolume_2 : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject node = SearchUtilities.Find("Tesseract_Body/Sector/EntranceNode");

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
            {
                TesseractsSecret.Log("Player entered NodeRepositionVolume_2!");
                node.transform.localPosition = new Vector3(-119.5f, -25.03f, -58.05f);
                node.transform.localRotation = Quaternion.Euler(0, 257, 355);
            }
        }

        public static NodeRepositionVolume_2 MakeNodeRepositionVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("NodeRepositionVolume_2");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var teleportVolume = volume.AddComponent<NodeRepositionVolume_2>();

            return teleportVolume;
        }
    }
}