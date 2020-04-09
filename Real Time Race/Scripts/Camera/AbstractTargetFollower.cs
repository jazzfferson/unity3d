using UnityEngine;

public abstract class AbstractTargetFollower : MonoBehaviour
{
    public enum UpdateType                                  // The available methods of updating are:
    {                                              // Let the script decide how to update
        FixedUpdate,                                        // Update in FixedUpdate (for tracking rigidbodies).
        LateUpdate,                                         // Update in LateUpdate. (for tracking objects that are moved in Update)
    }
    
    [SerializeField] protected Transform target;              		// The target object to follow		// Whether the rig should automatically target the player.
    [SerializeField] private UpdateType updateType;         		// stores the selected update type


	void FixedUpdate() {

        // we update from here if updatetype is set to Fixed, or in auto mode,
		// if the target has a rigidbody, and isn't kinematic.
		if (updateType == UpdateType.FixedUpdate) {

			FollowTarget(Time.deltaTime);
		}
	}

	void LateUpdate() {

		// we update from here if updatetype is set to Late, or in auto mode,
		// if the target does not have a rigidbody, or - does have a rigidbody but is set to kinematic.
		if (updateType == UpdateType.LateUpdate) {
			FollowTarget(Time.deltaTime);
		}
	}
	
	protected abstract void FollowTarget(float deltaTime);
}
