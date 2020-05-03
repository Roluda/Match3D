using NinjaCactus.Gamelogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Interface {
    public class Rotator : MonoBehaviour {

        [SerializeField]
        GameObject targetObject;
        [SerializeField, Range(0, 1000)]
        float rotationSpeedMouse;
        [SerializeField, Range(0, 100)]
        float rotationSpeedMobile;
        [SerializeField, Range(0, 100)]
        float barrelSpeedMobile = 50;

        public void NewTarget(Matchspace target) {
            targetObject = target.gameObject;
        }

        public void NewTarget(GameObject target) {
            targetObject = target.gameObject;
        }

        // Update is called once per frame
        void Update() {
            if (!targetObject) {
                return;
            }

#if UNITY_STANDALONE || UNITY_EDITOR
                if (Input.GetKey(KeyCode.Mouse1)) {
                    float rotX = Input.GetAxis("Mouse X") * rotationSpeedMouse * Mathf.Deg2Rad;
                    float rotY = Input.GetAxis("Mouse Y") * rotationSpeedMouse * Mathf.Deg2Rad;

                    targetObject.transform.Rotate(Vector3.up, -rotX, Space.World);
                    targetObject.transform.Rotate(Vector3.right, rotY, Space.World);
                }
#endif

#if UNITY_ANDROID
            if (Input.touchCount == 1) {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Moved) {
                    float rotX = touch.deltaPosition.x * rotationSpeedMobile * Mathf.Deg2Rad;
                    float rotY = touch.deltaPosition.y * rotationSpeedMobile * Mathf.Deg2Rad;

                    targetObject.transform.Rotate(Vector3.up, -rotX, Space.World);
                    targetObject.transform.Rotate(Vector3.right, rotY, Space.World);
                }
            }

            if (Input.touchCount == 2) {
                Touch touchOne = Input.GetTouch(0);
                Touch touchTwo = Input.GetTouch(1);
                if (touchOne.phase == TouchPhase.Moved && touchTwo.phase == TouchPhase.Moved) {
                    Vector3 oldAxis = (touchOne.position - touchOne.deltaPosition) - (touchTwo.position - touchTwo.deltaPosition);
                    Vector3 newAxis = touchOne.position - touchTwo.position;
                    float angle = Vector3.SignedAngle(oldAxis, newAxis, Vector3.forward) * Mathf.Deg2Rad;
                    targetObject.transform.Rotate(Vector3.forward, angle * barrelSpeedMobile, Space.World);
                }
            }
#endif
        }
    }
}
