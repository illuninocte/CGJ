﻿/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class Character : MonoBehaviour
{
	public Checkpoint Checkpoint;

	public void Die()
	{
		this.transform.position = this.Checkpoint.transform.position;
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Character))]
[CanEditMultipleObjects]
public class CharacterEditor : Editor
{
#pragma warning disable 0219, 414
	private Character _sCharacter;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sCharacter = this.target as Character;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif