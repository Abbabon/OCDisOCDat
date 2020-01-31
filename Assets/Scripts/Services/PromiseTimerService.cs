using System;
using RSG;
using UnityEngine;
using Zenject;

public class PromiseTimerService : IPromiseTimerService, ITickable
{
	private readonly IPromiseTimer _promiseTimer;

	public PromiseTimerService()
	{
		_promiseTimer = new RSG.PromiseTimer();
	}

	public void Tick()
	{
		_promiseTimer.Update(Time.deltaTime);
	}

	public IPromise WaitFor(float seconds)
	{
		return _promiseTimer.WaitFor(seconds);
	}

	public IPromise WaitUntil(Func<TimeData, bool> predicate)
	{
		return _promiseTimer.WaitUntil(predicate);
	}

	public IPromise WaitWhile(Func<TimeData, bool> predicate)
	{
		return _promiseTimer.WaitWhile(predicate);
	}

	public IPromise WaitForAndUpdate(float timeToWait, Action<float> callbackWhileWaiting)
	{
		if (callbackWhileWaiting == null)
		{
			return Promise.Rejected(new InvalidOperationException("no action given for updates"));
		}

		return _promiseTimer.WaitWhile(time =>
		{
			callbackWhileWaiting(time.elapsedTime);
			return time.elapsedTime <= timeToWait;
		});
	}

	public IPromise WaitForAndUpdate(TimeSpan timeToWait, Action<TimeSpan> callbackWhileWaiting)
	{
		if (callbackWhileWaiting == null)
		{
			return Promise.Rejected(new InvalidOperationException("no action given for updates"));
		}

		return _promiseTimer.WaitWhile(time =>
		{
			var fromSeconds = TimeSpan.FromSeconds(time.elapsedTime);
			callbackWhileWaiting(fromSeconds);
			return timeToWait >= fromSeconds;
		});
	}

	public bool CancelPromise(IPromise promiseToCancel)
	{
		return _promiseTimer.Cancel(promiseToCancel);
	}
}