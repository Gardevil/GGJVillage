using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour {

    public float WidthBound=100;
    public float HeightBound=100;

    public float horizontalPercentageReaction = 0.05f;
    public float verticalPercentageReaction = 0.001f;

    public float horizontalSpeed = 20;///m/s
    public float verticalSpeed = 20;///m/s

    public float maxZoomIn = 3;
    public float maxZoomOut = 0.3f;
    public float zoomSpeed = 500000;

    float initialOrthoSize;

	// Use this for initialization
	void Start () {
        initialOrthoSize = GetComponent<Camera>().orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
        float xPerc = Input.mousePosition.x / Screen.width;
        float yPerc = Input.mousePosition.y / Screen.height;

        float x = transform.position.x;
        float z = transform.position.z;

        bool move = false;
        if (0 <= xPerc && xPerc < horizontalPercentageReaction)
        {
            x -= horizontalSpeed * Time.deltaTime;
            move = true;
        }
        else if (1 >= xPerc && xPerc > 1 - horizontalPercentageReaction)
        {
            x += horizontalSpeed * Time.deltaTime;
            move = true;
        }

        if (0 <= yPerc && yPerc < verticalPercentageReaction)
        {
            z -= verticalSpeed * Time.deltaTime;
            move = true;
        }
        else if (1 >= yPerc && yPerc > 1 - verticalPercentageReaction)
        {
            z += verticalSpeed * Time.deltaTime;
            move = true;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float orthoSize = GetComponent<Camera>().orthographicSize;
        if (scroll != 0)
        {
            orthoSize -= scroll * zoomSpeed * Time.deltaTime;
            GetComponent<Camera>().orthographicSize = Math.Max(initialOrthoSize * maxZoomOut, Math.Min(orthoSize, initialOrthoSize * maxZoomIn));
        }

        if (move)
        {
            x = Math.Max(-WidthBound, Math.Min(x, WidthBound));
            z = Math.Max(-HeightBound, Math.Min(z, HeightBound));
            transform.position = new Vector3(x, transform.position.y, z);
        }
        //Debug.Log(Input.mousePosition);
	}
}
