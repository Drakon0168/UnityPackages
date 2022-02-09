using System;
using System.Collections.Generic;
using Drakon.CombatSystem;
using UnityEngine;

namespace Drakon.CombatSystem
{
	public class CSWeapon : MonoBehaviour
	{
		[SerializeField]
		private int inputCount;
		[SerializeField]
		private List<CSAttack> attacks;
		private CSAttack currentAttack;
		private CSAttack idle;

		[SerializeReference]
		private Dictionary<CSAttack, List<CSAttack>> adjacencyList;

		#region Accessors

		public List<CSAttack> Attacks => attacks;

		public CSAttack CurrentAttack => currentAttack;

		public CSAttack Idle => idle;
		
		public int InputCount => inputCount;

		#endregion

		public void Awake()
		{
			currentAttack = attacks[0];
		}
	}
}