using UnityEngine;
using System.Collections;

public class GestosCamara : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var pan = new TKPanRecognizer();		
		// when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
//		if( Application.platform == RuntimePlatform.IPhonePlayer )
//			pan.minimumNumberOfTouches = 2;
		
		pan.gestureRecognizedEvent += ( r ) =>
		{
			float panX = pan.deltaTranslation.x;
			float panY = pan.deltaTranslation.y;
			Vector3 camPos = Camera.mainCamera.transform.position;
			float newCamPosX = Mathf.Clamp(camPos.x + panX, -75, -30);
			float newCamPosY = Mathf.Clamp(camPos.y + panY, -75, -30);
			if((newCamPosX != -75 || newCamPosX != -35) && ( newCamPosY != -75 || newCamPosY != -35 ))
				Camera.mainCamera.transform.position -= new Vector3(panX, panY) / 25;
				
			//Camera.mainCamera.transform.position = new Vector3(newCamPosX, camPos.y, newCamPosZ);
			Debug.Log( "pan recognizer fired: " + r );
		};
		
		// continuous gestures have a complete event so that we know when they are done recognizing
		pan.gestureCompleteEvent += r =>
		{
			Debug.Log( "pan gesture complete" );
		};
		TouchKit.addGestureRecognizer( pan );
		
		
		
		
		var pinch = new TKPinchRecognizer();
		pinch.gestureRecognizedEvent += ( r ) =>
		{
			Camera.mainCamera.orthographicSize = Mathf.Clamp(Camera.mainCamera.orthographicSize + pinch.deltaScale, 9, 25);
//			Camera.mainCamera.fieldOfView = Mathf.Clamp(Camera.mainCamera.fieldOfView + pinch.deltaScale * 50, 20, 75);
//			Debug.Log (pinch.deltaScale);
//			Debug.Log( "pinch recognizer fired: " + r );
		};
		TouchKit.addGestureRecognizer( pinch );

	}
}
