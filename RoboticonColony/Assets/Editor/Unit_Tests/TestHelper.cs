using UnityEngine;
using System;
using System.Collections;
using NUnit.Framework;

/// <summary>
/// A simple class which contains helper methods for testing. Such as expected exceptions, since Unity's implementation of NUnit does not contain Assert.Throws
/// </summary>
public class TestHelper{

    /// <summary>
    /// Returns true if a given method throws an exception of a given type
    /// </summary>
    /// <param name="method">A lambda expression of the method to call</param>
    /// <param name="expected">The expected exception type</param>
    /// <returns>If the exception was thrown or not</returns>
	public static bool Throws(Action method, Type expected)
    {
        //Throw an exception if the passed in expected type is not a type of exception
        if (!expected.IsSubclassOf(typeof(Exception)))
            throw new ArgumentException("Not a type of exception!", "expected");

        //Attempt to transfer 2 money from inv1 to inv2, even though inv1 only has 1 money, which should throw an error
        try
        {
            //Execute the method
            method();
        }
        catch (Exception e)
        {
            //The expected exception was obtained, so return true
            if (e.GetType() == expected)
                return true;
        }

        return false;
    }

    [Test]
    public void ThrowExpected()
    {
        //Expect an ArgumentException to be thrown from passing the value of 4 into the IsFive method
        bool exceptionThrown = Throws(() => IsFive(4), typeof(ArgumentException));
        Assert.True(exceptionThrown);
    }

    [Test]
    public void ThrowNotExpected()
    {
        //Expect the IsFive method to finish without throwing any exceptions when passing it the value of 5
        bool exceptionThrown = Throws(() => IsFive(5), typeof(ArgumentException));
        Assert.False(exceptionThrown);
    }

    [Test]
    public void ThrowNonExceptionTypeUsed()
    {
        try
        {
            //Try to tell the throw method to expect an exception of type int, which isnt an exception type. This should throw an exception
            Throws(() => IsFive(4), typeof(int));
            Assert.Fail();
        }
        catch(Exception e)
        {
            if (e.GetType() != typeof(ArgumentException))
                Assert.Fail();
        }
    }

    /// <summary>
    /// Simple method used for Throw testing. Throws an ArgumentException error if the passed in number is not 5
    /// </summary>
    /// <param name="x">The number to check</param>
    private void IsFive(int x)
    {
        if (x != 5)
            throw new ArgumentException();
    }
}
