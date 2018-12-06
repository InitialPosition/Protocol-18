using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour {

	// assign default values to the global controls
	public static KeyCode move_forward = KeyCode.W;
	public static KeyCode move_backward = KeyCode.S;
	public static KeyCode move_left = KeyCode.A;
	public static KeyCode move_right = KeyCode.D;
	public static KeyCode move_jump = KeyCode.Space;

	public static void setNewControls(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode jump) {
		move_left = left;
		move_right = right;
		move_forward = up;
		move_backward = down;
		move_jump = jump;
	}
}
