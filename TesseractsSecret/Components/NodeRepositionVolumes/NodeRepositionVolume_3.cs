using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace TesseractsSecret.Components.NodeRepositionVolumes
{
    internal class NodeRepositionVolume_3 : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter = new();

        public GameObject node = SearchUtilities.Find("Tesseract_Body/Sector/EntranceNode");

        public virtual void Start()
        {
            //This works.
            TesseractsSecret.Log("This was called inside Node Repo 3 at the start!");
        }


        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
            {
                TesseractsSecret.Log("Player entered NodeRepositionVolume_3!");
                node.transform.localPosition = new Vector3(97.06f, -60.32f, 98.56f);
                node.transform.localRotation = Quaternion.Euler(355, 345, 129);
            }
        }

        public static NodeRepositionVolume_3 MakeNodeRepositionVolume(GameObject planet, Vector3 pos, float radius)
        {
            var volume = new GameObject("NodeRepositionVolume_3");
            volume.transform.parent = planet.transform;
            volume.transform.localPosition = pos;
            volume.layer = LayerMask.NameToLayer("BasicEffectVolume");

            var sphere = volume.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = radius;

            var teleportVolume = volume.AddComponent<NodeRepositionVolume_3>();

            return teleportVolume;
        }
    }
}