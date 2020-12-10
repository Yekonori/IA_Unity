using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class isEnnemyInShockWaveRadius : Conditional
	{
		public float shockwaveRadius = 2f;
		public SharedVector3 currentPosition;
		public SharedVector3 currentEnnemyPosition;

		public override TaskStatus OnUpdate()
		{
			float distance = (currentPosition.Value - currentEnnemyPosition.Value).magnitude;

			if (distance < shockwaveRadius)
            {
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
