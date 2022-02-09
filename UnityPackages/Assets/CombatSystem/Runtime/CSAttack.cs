using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Drakon.CombatSystem
{
	public class CSAttack : MonoBehaviour
	{
		[SerializeField]
		private CSAttackStats stats;
		
		protected CancellationTokenSource attackToken;
		protected CancellationTokenSource comboToken;
		
		#region Accessors
		
		/// <summary>
		/// The attack stats to use for this attack
		/// </summary>
		public CSAttackStats Stats
		{
			get => stats;
			protected set => stats = value;
		}
		
		#endregion
		
		#region Events
		
		/// <summary>
		/// Called when the attack is started
		/// </summary>
		public event System.Action OnAttackStart;
		
		/// <summary>
		/// Called at the beginning of the attack's windup phase
		/// </summary>
		public event System.Action OnWindupStart;
		
		/// <summary>
		/// Called at the beginning of the attack's active phase
		/// </summary>
		public event System.Action OnActiveStart;
		
		/// <summary>
		/// Called at the beginning of the attack's cooldown phase
		/// </summary>
		public event System.Action OnCooldownStart;

		/// <summary>
		/// Called at the end of the attack's cooldown phase
		/// </summary>
		public event System.Action OnCooldownEnd;
		
		/// <summary>
		/// Called at the beginning of the attack's combo phase (between active start and combo end)
		/// </summary>
		public event System.Action OnComboStart;
		
		/// <summary>
		/// Called at the end of the attack's combo phase
		/// </summary>
		public event System.Action OnComboEnd;
		
		#endregion
		
		#region Constructor

		public CSAttack(CSAttackStats stats)
		{
			Stats = stats;
			attackToken = new CancellationTokenSource();
			comboToken = new CancellationTokenSource();
		}

		#endregion
		
		#region Attack Timers
		
		/// <summary>
		/// Starts the attack triggering the OnAttackStart and OnWindupStart events
		/// </summary>
		public virtual void Attack()
		{
			Task.Run(StartAttack, attackToken.Token);
		}

		/// <summary>
		/// Called when the attack starts, this handles the attack timers and triggers attack events
		/// </summary>
		protected virtual async void StartAttack()
		{
			OnAttackStart?.Invoke();
			OnWindupStart?.Invoke();

			await Task.Delay((int)(Stats.WindupTime * 1000));
			
			OnActiveStart?.Invoke();

			await Task.Delay((int)(Stats.AttackTime * 1000));

			OnCooldownStart?.Invoke();
			
			Task.Run(StartCombo, comboToken.Token);
			
			await Task.Delay((int)(Stats.CooldownTime * 1000));

			OnCooldownEnd?.Invoke();
		}

		/// <summary>
		/// Cancels the attack and combo async tasks
		/// </summary>
		public virtual void CancelAttack()
		{
			attackToken.Cancel();
			CancelCombo();
		}

		/// <summary>
		/// Starts the combo async task
		/// </summary>
		protected virtual async void StartCombo()
		{
			OnComboStart?.Invoke();

			await Task.Delay((int) (Stats.ComboTime * 1000));

			OnComboEnd?.Invoke();
		}

		/// <summary>
		/// Cancels the combo async task
		/// </summary>
		public virtual void CancelCombo()
		{
			comboToken.Cancel();
		}

		#endregion 
	}
}