//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Shooting : MonoBehaviour
    {
        public SteamVR_Action_Boolean shootAction;

        public Hand hand;

        public bool shot;
       


        private void OnEnable()
        {
            shot = false;
            if (hand == null)
                hand = this.GetComponent<Hand>();
           
            if (shootAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No shoot action assigned", this);
                return;
            }

            shootAction.AddOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (shootAction != null)
                shootAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnPlantActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                //Plant();
                shot = true;
            }
        }

        public void Plant()
        {
            StartCoroutine(DoPlant());
        }

        private IEnumerator DoPlant()
        {
            yield return null;
            /*Vector3 plantPosition;

            RaycastHit hitInfo;
            bool hit = Physics.Raycast(hand.transform.position, Vector3.down, out hitInfo);
            if (hit)
            {
                plantPosition = hitInfo.point + (Vector3.up * 0.05f);
            }
            else
            {
                plantPosition = hand.transform.position;
                plantPosition.y = Player.instance.transform.position.y;
            }

            GameObject planting = GameObject.Instantiate<GameObject>(prefabToPlant);
            planting.transform.position = plantPosition;
            planting.transform.rotation = Quaternion.Euler(0, Random.value * 360f, 0);

            planting.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));

            Rigidbody rigidbody = planting.GetComponent<Rigidbody>();
            if (rigidbody != null)
                rigidbody.isKinematic = true;



            Vector3 initialScale = Vector3.one * 0.01f;
            Vector3 targetScale = Vector3.one * (1 + (Random.value * 0.25f));

            float startTime = Time.time;
            float overTime = 0.5f;
            float endTime = startTime + overTime;

            while (Time.time < endTime)
            {
                planting.transform.localScale = Vector3.Slerp(initialScale, targetScale, (Time.time - startTime) / overTime);
                yield return null;
            }


            if (rigidbody != null)
                rigidbody.isKinematic = false;
                */
        }
    }
}