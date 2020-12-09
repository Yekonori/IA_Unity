using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class SetShoot : Action
	{
		TeamImpactController controller;

		public bool needToShoot = true;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			controller.shoot = needToShoot;

			return TaskStatus.Success;
		}
	}
}