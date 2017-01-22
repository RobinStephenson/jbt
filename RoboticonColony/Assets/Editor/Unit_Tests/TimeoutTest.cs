using NUnit.Framework;
using System;

public class TimeoutTest {

    // no tests for time dependant behaviour as most of the behaviour is trivial and
    // ... we want our tests to execute quickly.

	[Test]
	public void SecondsRemainingUnstartedTest()
    {
        int StartSeconds = 10;
        Timeout timeout = new Timeout(StartSeconds);
        Assert.AreEqual(timeout.SecondsRemaining, StartSeconds);
    }

    [Test]
    public void InvalidConstructionTest()
    {
        // Trying to create a timeout with less than 1 second should throw an error
        Assert.True(TestHelper.Throws(() => new Timeout(0), typeof(ArgumentOutOfRangeException)));
        Assert.True(TestHelper.Throws(() => new Timeout(-1), typeof(ArgumentOutOfRangeException)));
    }

    [Test]
    public void FinishedUnstartedTest()
    {
        // Finished should return false if the timeout was not started
        Timeout timeout = new Timeout(10);
        Assert.False(timeout.Finished);
    }
}
