using PigVisionMobile.Core.Behaviors;

namespace PigVisionMobile.UnitTests
{
	public class MockEventToCommandBehavior : EventToCommandBehavior
	{
		public void RaiseEvent(params object[] args)
		{
			_handler.DynamicInvoke(args);
		}
	}
}
