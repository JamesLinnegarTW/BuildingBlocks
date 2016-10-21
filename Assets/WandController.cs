using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WandController : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private GameObject pickUp;

	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();

	private InteractableItem closestItem;
	private InteractableItem interactingItem;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame 
	void Update () {
		if(controller == null) {
			Debug.Log ("Controller not initialised");
			return;
		}


		if (controller.GetPressDown (triggerButton)) {
			Debug.Log ("Trigger button was just pressed");
			float minDistance = float.MaxValue;
			float distance;
			foreach (InteractableItem item in objectsHoveringOver) {
				distance = (item.transform.position - transform.position).sqrMagnitude; // squared because the difference in vectors can be a negative
			

				if (distance < minDistance) {
					minDistance = distance;
					closestItem = item;

				}
			}
			interactingItem = closestItem;
			closestItem = null;

			if (interactingItem) {
				if (interactingItem.IsInteracting()) {
					interactingItem.EndInteraction (this);
				}
				interactingItem.BeginInteraction (this);
			}
		}

		if (controller.GetPressUp (triggerButton) && interactingItem != null) {
			Debug.Log ("Trigger button was just Released");
			interactingItem.EndInteraction (this);

		}


	}

	private void OnTriggerEnter(Collider collider){
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if(collidedItem != null){ // is the item interactable?
			objectsHoveringOver.Add(collidedItem);
		}
	}

	private void OnTriggerExit(Collider collider){
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if(collidedItem != null){ // is the item interactable?
			objectsHoveringOver.Remove(collidedItem);
		}
	}
}
