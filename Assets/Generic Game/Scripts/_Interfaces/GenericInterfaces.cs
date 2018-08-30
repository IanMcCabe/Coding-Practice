using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericGame
{
	// Core Interfaces
	interface IInput
	{
		void GetInput();
		void TriggerAction(IAction iAction);
	}

	interface IAction
	{
		void TriggerEvent(IEvent iEvent);
	}

	interface IEvent
	{
		void TriggerObjective(IObjective iObjective);
	}

	interface IObjective
	{
		void CheckObjective();
	}

	// Input Interfaces
	class keyInput : IInput
	{
		public void GetInput()
		{
		}

		public void TriggerAction(IAction iAction)
		{
		}
	}

	class SwipeInput : IInput
	{
		public void GetInput()
		{
			throw new System.NotImplementedException();
		}

		public void TriggerAction(IAction iAction)
		{
			throw new System.NotImplementedException();
		}
	}

	interface IClickInput : IInput
	{
	}

	interface IJoyStickInput : IInput
	{
	}

	// Action Interfaces
	interface IAttack : IAction
	{
	}

	// Event Interfaces
	interface IDamage : IEvent
	{
	}

	// Objective Interfaces
	interface IDestroy : IObjective
	{
	}
}
