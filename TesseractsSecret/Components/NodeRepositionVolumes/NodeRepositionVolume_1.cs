using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components.NodeRepositionVolumes
{
    internal class NodeRepositionVolume_1 : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject node = SearchUtilities.Find("Tesseract_Body/Sector/EntranceNode");

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
            {
                TesseractsSecret.Log("Player entered NodeRepositionVolume_1!");
                node.transform.localPosition = new Vector3(-36.58f, 129.45f, 68.01f);
                node.transform.localRotation = Quaternion.Euler(355, 0, 0);
            }
        }

        public static NodeRepositionVolume_1 MakeNodeRepositionVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("NodeRepositionVolume_1");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var teleportVolume = volume.AddComponent<NodeRepositionVolume_1>();

            return teleportVolume;
        }
    }
}