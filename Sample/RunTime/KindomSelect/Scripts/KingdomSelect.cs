using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Cr7Sund.TweenTimeLine
{
    public class KingdomSelect : MonoBehaviour
    {
        public List<Kingdom> kingdoms = new List<Kingdom>();
        private List<GameObject> kingdomBtns = new List<GameObject>();

        [Space]

        [Header("Public References")]
        public GameObject kingdomPointPrefab;
        public GameObject kingdomButtonPrefab;
        public Transform modelTransform;
        public Transform kingdomButtonsContainer;
        public RectTransform followTarget;

        public Camera mainCamera;

        [Space]
        public Vector2 visualOffset;

        [Header("Tween Settings")]
        public float lookDuration;
        public Ease lookEase;

        public void Show()
        {
            var sequence = Sequence.Create();
            foreach (Kingdom k in kingdoms)
            {
                sequence.Chain(SpawnKingdomPoint(k));
            }

            if (kingdoms.Count > 0)
            {
                LookAtKingdom(kingdoms[0]);
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(kingdomButtonsContainer.GetChild(0).gameObject);
            }
        }

        public void Hide()
        {
            foreach (var kingdom in kingdoms)
            {
                Destroy(kingdom.visualPoint.parent.gameObject);
            }
            foreach (var kingdom in kingdomBtns)
            {
                Destroy(kingdom);
            }
            kingdomBtns.Clear();
        }

        private void LookAtKingdom(Kingdom k)
        {
            Transform cameraParent = mainCamera.transform.parent;
            Transform cameraPivot = cameraParent.parent;
            //dynamic -animation
            Vector3 endCameraRotation = new Vector3(k.y, 0, 0);
            Vector3 endCameraPivotRotation = new Vector3(0, -k.x, 0);
            var sequence = Sequence.Create().Group(
            PrimeTween.Tween.LocalRotation(
                cameraParent, endCameraRotation, lookDuration, lookEase))
            .Group(
            PrimeTween.Tween.LocalRotation(
                cameraPivot, endCameraPivotRotation, lookDuration, lookEase));

            sequence.OnComplete(followTarget, (target) =>
            {
                if (!followTarget.gameObject.activeInHierarchy)
                {
                    followTarget.gameObject.SetActive(true);
                }
                var screenPos = mainCamera.WorldToScreenPoint(k.visualPoint.position);
                target.position = screenPos;
            });
        }

        private Tween SpawnKingdomPoint(Kingdom k)
        {
            GameObject kingdom = Instantiate(kingdomPointPrefab, modelTransform);
            kingdom.transform.localEulerAngles = new Vector3(k.y + visualOffset.y, -k.x - visualOffset.x, 0);
            k.visualPoint = kingdom.transform.GetChild(0);

            SpawnKingdomButton(k);

            return Tween.LocalPositionY(kingdom.transform, kingdom.transform.position.y - 20f, kingdom.transform.position.y, 0.2f);
        }

        private void SpawnKingdomButton(Kingdom k)
        {
            Kingdom kingdom = k;
            Button kingdomButton = Instantiate(kingdomButtonPrefab, kingdomButtonsContainer).GetComponent<Button>();
            kingdomBtns.Add(kingdomButton.gameObject);
            kingdomButton.onClick.AddListener(() =>
            {
                LookAtKingdom(kingdom);
            });

            kingdomButton.transform.GetChild(0).GetComponentInChildren<TMPro.TMP_Text>().text = k.name;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;

            //only draw if there is at least one stage
            if (kingdoms.Count > 0)
            {
                for (int i = 0; i < kingdoms.Count; i++)
                {
                    //creat two empty objects
                    GameObject point = new GameObject("point");
                    GameObject parent = new GameObject("parent");
                    //move the point object to the front of the world sphere
                    point.transform.position += -new Vector3(0, 0, .5f);
                    //parent the point to the "parent" object in the center
                    point.transform.parent = parent.transform;
                    //set the visual offset
                    parent.transform.eulerAngles = new Vector3(visualOffset.y, -visualOffset.x, 0);

                    if (!Application.isPlaying)
                    {
                        Gizmos.DrawWireSphere(point.transform.position, 0.02f);
                    }

                    //spint the parent object based on the stage coordinates
                    parent.transform.eulerAngles += new Vector3(kingdoms[i].y, -kingdoms[i].x, 0);
                    //draw a gizmo sphere // handle label in the point object's position
                    Gizmos.DrawSphere(point.transform.position, 0.07f);
                    //destroy all
                    DestroyImmediate(point);
                    DestroyImmediate(parent);
                }
            }
#endif
        }
    }

    [System.Serializable]
    public class Kingdom
    {
        public string name;

        [Range(-180, 180)]
        public float x;
        [Range(-89, 89)]
        public float y;

        [HideInInspector]
        public Transform visualPoint;

        // [HideInInspector]
        // public LinePath linePath;
    }
}