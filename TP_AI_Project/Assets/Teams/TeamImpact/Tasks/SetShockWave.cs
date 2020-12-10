using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class SetShockWave : Action
	{
		TeamImpactController controller;

		public bool needToShockWave = true;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			controller.fireShockWAve = needToShockWave;
			return TaskStatus.Success;
		}
	}
}