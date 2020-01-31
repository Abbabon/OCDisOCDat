using System;
using RSG;

public interface IPromiseTimerService
{
	IPromise WaitWhile(Func<TimeData, bool> predicate);

	IPromise WaitUntil(Func<TimeData, bool> predicate);

	/// <summary>
	/// Wait for x seconds.
	/// </summary>
	/// <param name="seconds">time in floats</param>
	/// <returns></returns>
	IPromise WaitFor(float seconds);

	/// <summary>
	/// Cancels a promise provided by the PromiseTimer.
	/// </summary>
	/// <param name="promiseToCancel">the Promise we wish to cancel.</param>
	/// <returns></returns>
	bool CancelPromise(IPromise promiseToCancel);

	/// <summary>
	/// Will wait for timeToWait Seconds and call the callbackWhileWaiting while it waits.
	/// </summary>
	/// <param name="timeToWait">the time this Promise waits in seconds</param>
	/// <param name="callbackWhileWaiting">A callback to be called while the timer is waiting </param>
	/// <returns></returns>
	IPromise WaitForAndUpdate(float timeToWait, Action<float> callbackWhileWaiting);

	/// <summary>
	/// Will wait for timeToWait timespan and call the callbackWhileWaiting while it waits.
	/// </summary>
	/// <param name="timeToWait">the time this Promise waits in Timespan format</param>
	/// <param name="callbackWhileWaiting">A callback to be called while the timer is waiting </param>
	/// <returns></returns>
	IPromise WaitForAndUpdate(TimeSpan timeToWait, Action<TimeSpan> callbackWhileWaiting);
}