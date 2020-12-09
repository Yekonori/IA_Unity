using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
//
using BehaviorDesigner.Runtime;
using System.Linq;

namespace TeamImpact {

	public class TeamImpactController : BaseSpaceShipController
	{
		BehaviorTree behaviorTree;
		public BehaviorTree BehaviorTree
        {
			get { return behaviorTree; }
        }

		List<WayPoint> waypoints;
		List<WayPoint> myWaypoints;
		List<WayPoint> ennemyWaypoints;
		List<WayPoint> waypointsNotOwn;
		[SerializeField] WayPoint closestWP = null;
		public WayPoint ClosestWP
        {
			get { return closestWP; }
        }

		public SharedBool hasWaypointToConquest = true;

		[HideInInspector]
		public bool isChassingEnemy = false;

		//Mine
		public float safeDistanceToMine = 3f;
		public SharedGameObjectList listMines = null;

		// Variables to modify
		public float thrust = 0.5f;
		public float targetOrient = 0f;
		public bool shoot = false;
		public bool dropMine = false;
		public bool fireShockWAve = false;

		private void Start()
        {
			behaviorTree = GetComponent<BehaviorTree>();
        }

        public override void Initialize(SpaceShip spaceship, GameData data)
		{
			
		}

		public override InputData UpdateInput(SpaceShip spaceship, GameData data)
		{
			waypoints = data.WayPoints;
			myWaypoints = waypoints.FindAll((wp) => wp.Owner == spaceship.Owner);
			ennemyWaypoints = waypoints.FindAll((wp) => wp.Owner != spaceship.Owner && wp.Owner != -1); //waypoints[0].Owner == -1 si pas contest
			waypointsNotOwn = waypoints.Except(myWaypoints).ToList();

            float minDistance = 1000f;

			// try to go to closest wp not own
			foreach (WayPoint wp in waypointsNotOwn)
            {
				float distance = (spaceship.transform.position - wp.transform.position).magnitude;
				if (distance < minDistance)
                {
					minDistance = distance;
					closestWP = wp;
				}
            }

			if (waypointsNotOwn.Count > 0)
            {
				hasWaypointToConquest.Value = true;
			}
			else
            {
				hasWaypointToConquest.Value = false;
			}

			//behaviorTree.SetVariableValue("closestWP", closestWP.transform.position);
			// My spaceship 
			behaviorTree.SetVariableValue("currentPosition", spaceship.transform.position);
			behaviorTree.SetVariableValue("velocity", spaceship.Velocity);
			behaviorTree.SetVariableValue("capturedWP", myWaypoints.Count);
			behaviorTree.SetVariableValue("notCapturedWP", waypointsNotOwn.Count);

			// Energy
			behaviorTree.SetVariableValue("energy", spaceship.Energy);

			// Ennemy
			behaviorTree.SetVariableValue("ennemyNbWP", ennemyWaypoints.Count);
			behaviorTree.SetVariableValue("ennemyPosition", data.SpaceShips[1 - spaceship.Owner].transform.position);
			behaviorTree.SetVariableValue("isEnnemyStun", data.SpaceShips[1 - spaceship.Owner].IsStun());

			// Temps restant
			behaviorTree.SetVariableValue("timeLeft", data.timeLeft);

			// Mines
			bool isMineDangerous = false;
			List<GameObject> listDangerousMines = new List<GameObject>();
			foreach (Mine mine in data.Mines)
            {
				float distanceToMine = (mine.transform.position - spaceship.transform.position).magnitude;
				if (distanceToMine < mine.ExplosionRadius + safeDistanceToMine)
                {
					isMineDangerous = true;
					listDangerousMines.Add(mine.gameObject);
				}
            }
			behaviorTree.SetVariableValue("isMineDangerous", isMineDangerous);
			listMines.SetValue(listDangerousMines);

			return new InputData(thrust, targetOrient, shoot, dropMine, fireShockWAve);
		}
    }
}
