using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float maxDisplacement = 0.2f;
        [SerializeField] private float maxPositionX = 2f;
        private Vector2 _anchorPosition;
        [SerializeField] public static float currentSpeed = 17.5f;
        [SerializeField] public static float baseSpeed = 17.5f;

        private float count;

        private IEnumerator Start() {
            GUI.depth = 2;
            while (true) {
                count = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void OnGUI() {
            GUI.Label(new Rect(5, 40, 200, 75), "FPS: " + Mathf.Round(count));
        }

        private void Update() {
            if (!GameObject.Find("StartMenu").transform.GetChild(0).gameObject.activeSelf) {
                gameObject.GetComponent<Animator>().SetBool("Run", true);
                var inputX = GetInput();

                var displacementX = GetDisplacement(inputX);

                displacementX = SmoothOutDisplacement(displacementX);

                var newPosition = GetNewLocalPosition(displacementX);

                newPosition = GetLimitedLocalPosition(newPosition);

                transform.localPosition = newPosition;
            }
        }

        private Vector3 GetLimitedLocalPosition(Vector3 position) {
            position.x = Mathf.Clamp(position.x, -maxPositionX, maxPositionX);
            return position;
        }
        private Vector3 GetNewLocalPosition(float displacementX) {
            var lastPosition = transform.localPosition;
            var newPositionX = lastPosition.x + displacementX;
            var newPositionZ = lastPosition.z + currentSpeed * Time.deltaTime;
            var newPosition = new Vector3(newPositionX, lastPosition.y, newPositionZ);
            return newPosition;
        }
        private float GetInput() {
            var inputX = 0f;
            if (Input.GetMouseButtonDown(0)) {
                _anchorPosition = Input.mousePosition;
            } else if (Input.GetMouseButton(0)) {
                inputX = (Input.mousePosition.x - _anchorPosition.x);
                _anchorPosition = Input.mousePosition;
            }
            return inputX;
        }
        private float GetDisplacement(float inputX) {
            var displacementX = 0f;
            displacementX = inputX * Time.deltaTime;
            return displacementX;
        }
        private float SmoothOutDisplacement(float displacementX) {
            return Mathf.Clamp(displacementX, -maxDisplacement, maxDisplacement);
        }
    }
}

