using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class HasEnoughEnergyForShockWave : Conditional
	{
		TeamImpactController controller;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			SharedFloat energy = (SharedFloat)controller.BehaviorTree.GetVariable("energy");

			if (energy.Value >= 0.4f)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}
