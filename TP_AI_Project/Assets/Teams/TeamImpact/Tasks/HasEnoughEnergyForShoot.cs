using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class HasEnoughEnergyForShoot : Conditional
	{
		TeamImpactController controller;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			SharedFloat energy = (SharedFloat)controller.BehaviorTree.GetVariable("energy");

			if (energy.Value >= 0.2f)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}
