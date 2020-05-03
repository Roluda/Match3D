using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NinjaCactus.Interface {
    [RequireComponent(typeof(Renderer))]
    public class UI_Cube : MonoBehaviour {
        public bool isActive = true;

        [SerializeField]
        float size;
        [SerializeField]
        Vector2 normalizedPosition;
        [SerializeField]
        float distanceFromCamera;
        [SerializeField]
        float growSpeed;
        [SerializeField]
        Vector3 rotationSpeed;
        [SerializeField]
        Texture icon;

        [SerializeField]
        UnityEvent onClick = default;


        Renderer render;

        public void Deactivate() {
            isActive = false;
        }

        public void Activate() {
            isActive = true;
        }

        private void Awake() {
            render = GetComponent<Renderer>();
        }
        // Update is called once per frame
        private void Start() {
            render.material.SetTexture("_Icon", icon);
        }

        private void Update() {
            UpdatePosition();
            UpdateSize();
            UpdateRotation();
        }

        private void OnMouseDown() {
            if (isActive) {
                onClick.Invoke();
            }
        }

        void UpdateRotation() {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        void UpdateSize() {
            if (isActive) {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one*size, Time.deltaTime * growSpeed);
            } else {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero*size, Time.deltaTime * growSpeed);
            }
        }

        void UpdatePosition() {
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width * normalizedPosition.x,
                Screen.height * normalizedPosition.y,
                distanceFromCamera));
            transform.position = new Vector3(position.x, position.y, 0);
        }
    }
}
