using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Drakon.CombatSystem
{
	public class CSAttack
	{
		private CancellationTokenSource comboTimer;
		
		#region Accessors
		
		/// <summary>
		/// The attack stats to use for this attack
		/// </summary>
		public CSAttackStats Stats
		{
			get;
			private set;
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
			comboTimer = new CancellationTokenSource();
		}

		#endregion
		
		#region Attack Timers
		
		public async void Attack()
		{
			OnAttackStart?.Invoke();
			OnWindupStart?.Invoke();

			await Task.Delay((int)(Stats.WindupTime * 1000));
			
			OnActiveStart?.Invoke();

			Task.Run(async () =>
			{
				OnComboStart?.Invoke();

				await Task.Delay((int) (Stats.ComboTime * 1000));

				OnComboEnd?.Invoke();
			}, comboTimer.Token);
			
			await Task.Delay((int)(Stats.AttackTime * 1000));

			OnCooldownStart?.Invoke();
			
			await Task.Delay((int)(Stats.CooldownTime * 1000));

			OnCooldownEnd?.Invoke();
		}

		public void CancelCombo()
		{
			comboTimer.Cancel();
		}

		#endregion 
	}
}