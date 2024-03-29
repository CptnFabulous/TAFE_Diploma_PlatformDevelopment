﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    public LayerMask touchMask;
    private List<GameObject> _touchList = new List<GameObject>();
    private GameObject[] _oldTouch;
    private RaycastHit _hit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        // Use Mouse
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            _oldTouch = new GameObject[_touchList.Count];
            _touchList.CopyTo(_oldTouch);
            _touchList.Clear();

            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, touchMask))
            {
                GameObject recipient = _hit.transform.gameObject;
                _touchList.Add(recipient);

                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", _hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchDown");
                }
                if (Input.GetMouseButton(0))
                {
                    recipient.SendMessage("OnTouchStay", _hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchStay");
                }
                if (Input.GetMouseButtonUp(0))
                {
                    recipient.SendMessage("OnTouchUp", _hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchUp");
                }
            }
            foreach (GameObject item in _oldTouch)
            {
                if (!_touchList.Contains(item))
                {
                    item.SendMessage("OnTouchExit", _hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchExit");
                }
            }
        }
        #endif

        if (Input.touchCount > 0)
        {
            _oldTouch = new GameObject[_touchList.Count];
            _touchList.CopyTo(_oldTouch);
            _touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit, touchMask))
                {
                    GameObject recipient = _hit.transform.gameObject;
                    _touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", _hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchDown");
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", _hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchExit");
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", _hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchUp");
                    }
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        recipient.SendMessage("OnTouchStay", _hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchStay");
                    }

                }
            }
            foreach (GameObject item in _oldTouch)
            {
                if (!_touchList.Contains(item))
                {
                    item.SendMessage("OnTouchExit", _hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchExit");
                }
            }
        }
    }
}
