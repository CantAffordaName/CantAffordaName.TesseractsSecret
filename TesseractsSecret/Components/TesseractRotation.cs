using UnityEngine;

namespace TesseractsSecret.Components
{
    internal class TesseractRotation : MonoBehaviour
    {
        Quaternion currentRotation;
        Quaternion rotationIncrement;
        Quaternion newRotation;

        private void Update()
        {
            currentRotation = transform.rotation;
            rotationIncrement = Quaternion.Euler(8f * Time.deltaTime, 8f * Time.deltaTime, 0f);
            newRotation = rotationIncrement * currentRotation;
            transform.rotation = newRotation;
        }
    }
}