namespace SuperMassive.Tests.Unit
{
    using System;
    using System.Globalization;
    using System.IO;
    using NUnit.Framework;
    using Properties;

    [TestFixture]
    public class GuardTest
    {
        [Test]
        public void Guard_ArgumentNotNull_ValidArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(new { Value = "value" }, "argumentName"));

            // value types shouldn't throw when boxed
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull("argumentValue", "argumentName"));
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(int.MaxValue, "argumentName"));
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(long.MaxValue, "argumentName"));
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(float.MaxValue, "argumentName"));
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(float.PositiveInfinity, "argumentName"));
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(double.MaxValue, "argumentName"));
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(DateTime.UtcNow, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNull_EmptyArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull(string.Empty, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNull_NullArgument_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(null, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNull_WrappedException_ValidArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNull<TestException>(new { Value = "value" }, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNull_WrappedException_NullArgument_Throws()
        {
            Assert.Throws<TestException>(() => Guard.ArgumentNotNull<TestException>(null, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WithNullArgument_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WithEmptyArgument_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty(string.Empty, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WithWhitespaceArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNullOrEmpty(" ", "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WithFilledArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNullOrEmpty(Randomizer.GetRandomString(), "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WrapException_WithNullArgument_Throws()
        {
            var exception = Assert.Throws<TestException>(() => Guard.ArgumentNotNullOrEmpty<TestException>(null, "argumentName"));
            Assert.That(exception.InnerException, Is.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WrapException_WithEmptyArgument_Throws()
        {
            var exception = Assert.Throws<TestException>(() => Guard.ArgumentNotNullOrEmpty<TestException>(string.Empty, "argumentName"));
            Assert.That(exception.InnerException, Is.TypeOf<ArgumentException>());
        }

        [Test]
        public void Guard_ArgumentNotNullOrEmpty_WrapException_WithValidArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNullOrEmpty<TestException>(Randomizer.GetRandomString(), "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WithNullString_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrWhiteSpace(null, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WithEmptyString_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrWhiteSpace(string.Empty, "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WithWhiteSpace_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrWhiteSpace(" ", "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WithMultipleWhiteSpace_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrWhiteSpace("    ", "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WithValidArguments_DoesNotThrow()
        {
            var argumentName = Randomizer.GetRandomString();
            var argumentValue = Randomizer.GetRandomString();

            Assert.DoesNotThrow(() => Guard.ArgumentNotNullOrWhiteSpace(argumentValue, argumentName));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WrapException_WithNullArgument_Throws()
        {
            var exception = Assert.Throws<TestException>(() => Guard.ArgumentNotNullOrWhiteSpace<TestException>(null, "argumentName"));
            Assert.That(exception.InnerException, Is.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WrapException_WithEmptyArgument_Throws()
        {
            var exception = Assert.Throws<TestException>(() => Guard.ArgumentNotNullOrWhiteSpace<TestException>(string.Empty, "argumentName"));
            Assert.That(exception.InnerException, Is.TypeOf<ArgumentException>());
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WrapException_WithWhiteSpace_Throws()
        {
            var exception = Assert.Throws<TestException>(() => Guard.ArgumentNotNullOrWhiteSpace<TestException>(" ", "argumentName"));
            Assert.That(exception.InnerException, Is.TypeOf<ArgumentException>());
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WrapException_WithMultipleWhiteSpace_Throws()
        {
            Assert.Throws<TestException>(() => Guard.ArgumentNotNullOrWhiteSpace<TestException>("   ", "argumentName"));
        }

        [Test]
        public void Guard_ArgumentNotNullOrWhiteSpace_WrapException_WithValidArgument_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.ArgumentNotNullOrWhiteSpace<TestException>(Randomizer.GetRandomString(), "argumentName"));
        }

        [Test]
        public void Guard_Requires_UnfulfillablePrecondition_NoArguments_Throws()
        {
            var messageFormat = Resources.GENERIC_GUARD_FAILURE_PRECONDITION_WITHFORMAT;

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()));
            });

            var message = string.Format(
                CultureInfo.CurrentCulture,
                messageFormat,
                null,
                null);

            Assert.That(exception.Message.StartsWith(message));
        }

        [Test]
        public void Guard_Requires_WrapException_ValidPrecondition_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.Requires<TestException>(() => { return true; }), "preconditionName", "argumentName");
        }

        [Test]
        public void Guard_Requires_WrapException_NullPrecondition_Throws()
        {
            Assert.Throws<TestException>(() => Guard.Requires<TestException>(null, "preconditionName", "argumentName"));
        }

        [Test]
        public void Guard_Requires_WrapException_NullPreconditionName_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.Requires<TestException>(() => { return true; }, argumentName: "argumentName"));
        }

        [Test]
        public void Guard_Requires_WrapException_UnfulfillablePrecondition_Throws()
        {
            Assert.Throws<TestException>(() => Guard.Requires<TestException>(() => { return false; }, "preconditionName", "argumentName"));
        }

        [Test]
        public void Guard_Requires_WrapException_NullArgumentName_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.Requires<TestException>(() => { return true; }, preconditionName: "preconditionName"));
        }

        [Test]
        public void Guard_IsInstanceOfType_WithNullType_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.IsInstanceOfType(null, "argumentValue", "argumentName"));
        }

        [Test]
        public void Guard_IsInstanceOfType_WithNullArgumentValue_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.IsInstanceOfType(typeof(string), null, "argumentName"));
        }

        [Test]
        public void Guard_IsInstanceOfType_WithNullArgumentName_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.IsInstanceOfType(typeof(string), "argumentValue", null));
        }

        [Test]
        public void Guard_IsInstanceOfType_WithMatchingType_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.IsInstanceOfType(typeof(string), "value", "argumentName"));
        }

        [Test]
        public void Guard_IsInstanceOfType_WithNotMatchingType_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.IsInstanceOfType(typeof(int), "value", "argumentName"));
        }

        [Test]
        public void Guard_Requires_UnfulfillablePrecondition_WithPreconditionName_WithArgumentName_Throws()
        {
            var argumentName = Randomizer.GetRandomString();
            var precondition = Randomizer.GetRandomString();
            var messageFormat = Resources.GENERIC_GUARD_FAILURE_PRECONDITION_WITHFORMAT;

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()), precondition, argumentName);
            });

            var message = string.Format(
                CultureInfo.CurrentCulture,
                messageFormat,
                precondition,
                argumentName);

            Assert.That(exception.Message.StartsWith(message));
        }

        [Test]
        public void Guard_Requires_UnfulfillablePrecondition_WithArgumentName_Throws()
        {
            var argumentName = Randomizer.GetRandomString();
            var messageFormat = Resources.GENERIC_GUARD_FAILURE_PRECONDITION_WITHFORMAT;

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()), argumentName: argumentName);
            });

            var message = string.Format(
                CultureInfo.CurrentCulture,
                messageFormat,
                null,
                argumentName);

            Assert.That(exception.Message.StartsWith(message));
        }

        [Test]
        public void Guard_Requires_UnfulfillablePrecondition_WithPreconditionName_Throws()
        {
            var preconditionName = Randomizer.GetRandomString();
            var messageFormat = Resources.GENERIC_GUARD_FAILURE_PRECONDITION_WITHFORMAT;

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()), preconditionName: preconditionName);
            });

            var message = string.Format(
                CultureInfo.CurrentCulture,
                messageFormat,
                preconditionName,
                null);

            Assert.That(exception.Message.StartsWith(message));
        }

        [Test]
        public void Guard_InstanceIsAssignable_WithNullTargetType_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.InstanceIsAssignable(null, new AssignableParent(), "argumentName"));
        }

        [Test]
        public void Guard_InstanceIsAssignable_WithNullInstance_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.InstanceIsAssignable(typeof(IAssignable), null, "argumentName"));

            Assert.Throws<ArgumentNullException>(() => Guard.InstanceIsAssignable(typeof(AssignableParent), null, "argumentName"));

            Assert.Throws<ArgumentNullException>(() => Guard.InstanceIsAssignable(typeof(AssignableChild), null, "argumentName"));
        }

        [Test]
        public void Guard_InstanceIsAssignable_Unassignable_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.InstanceIsAssignable(typeof(IAssignable), new Unassignable(), "argumentName"));

            Assert.Throws<ArgumentException>(() => Guard.InstanceIsAssignable(typeof(AssignableParent), new Unassignable(), "argumentName"));

            Assert.Throws<ArgumentException>(() => Guard.InstanceIsAssignable(typeof(AssignableChild), new Unassignable(), "argumentName"));
        }

        [Test]
        public void Guard_InstanceIsAssignable_Assignable_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.InstanceIsAssignable(typeof(IAssignable), new AssignableParent(), "instance"));

            Assert.DoesNotThrow(() => Guard.InstanceIsAssignable(typeof(IAssignable), new AssignableChild(), "instance"));

            Assert.DoesNotThrow(() => Guard.InstanceIsAssignable(typeof(AssignableParent), new AssignableChild(), "instance"));
        }

        [Test]
        public void Guard_InstanceIsAssignable_WrapException_Unassignable_Throws()
        {
            Assert.Throws<TestException>(() => Guard.InstanceIsAssignable<TestException>(typeof(IAssignable), new Unassignable(), "argumentName"));

            Assert.Throws<TestException>(() => Guard.InstanceIsAssignable<TestException>(typeof(AssignableParent), new Unassignable(), "argumentName"));

            Assert.Throws<TestException>(() => Guard.InstanceIsAssignable<TestException>(typeof(AssignableChild), new Unassignable(), "argumentName"));
        }

        [Test]
        public void Guard_TypeIsAssignable_WithNullTargetType_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.TypeIsAssignable(null, typeof(object), "argumentName"));
        }

        [Test]
        public void Guard_TypeIsAssignable_WithNullValueType_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.TypeIsAssignable(typeof(IAssignable), null, "argumentName"));
        }

        [Test]
        public void Guard_TypeIsAssignable_Unassignable_Throws()
        {
            Assert.Throws<ArgumentException>(() => Guard.TypeIsAssignable(typeof(IAssignable), typeof(Unassignable), "argumentName"));
        }

        [Test]
        public void Guard_TypeIsAssignable_Assignable_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.TypeIsAssignable(typeof(IAssignable), typeof(AssignableParent), "argumentName"));

            Assert.DoesNotThrow(() => Guard.TypeIsAssignable(typeof(IAssignable), typeof(AssignableChild), "argumentName"));

            Assert.DoesNotThrow(() => Guard.TypeIsAssignable(typeof(AssignableParent), typeof(AssignableChild), "argumentName"));
        }

        [Test]
        public void Guard_TypeIsAssignable_WrapException_WithNullTargetType_Throws()
        {
            var ex = Assert.Throws<TestException>(() => Guard.TypeIsAssignable<TestException>(null, typeof(object), "argumentName"));
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Guard_TypeIsAssignable_WrapException_WithNullValueType_Throws()
        {
            var ex = Assert.Throws<TestException>(() => Guard.TypeIsAssignable<TestException>(typeof(IAssignable), null, "argumentName"));
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Guard_TypeIsAssignable_WrapException_Unassignable_Throws()
        {
            var ex = Assert.Throws<TestException>(() => Guard.TypeIsAssignable<TestException>(typeof(IAssignable), typeof(Unassignable), "argumentName"));
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
        }

        [Test]
        public void Guard_TypeIsAssignable_WrapException_Assignable_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.TypeIsAssignable<TestException>(typeof(IAssignable), typeof(AssignableParent), "argumentName"));

            Assert.DoesNotThrow(() => Guard.TypeIsAssignable<TestException>(typeof(IAssignable), typeof(AssignableChild), "argumentName"));

            Assert.DoesNotThrow(() => Guard.TypeIsAssignable<TestException>(typeof(AssignableParent), typeof(AssignableChild), "argumentName"));
        }

        private class TestException : Exception
        {
            public TestException() : base()
            {
            }

            public TestException(string message) : base(message)
            {
            }

            public TestException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

        private class AssignableParent : IAssignable
        {
        }

        private class AssignableChild : AssignableParent, IAssignable
        {
        }

        private class Unassignable
        {
        }

        private interface IAssignable
        {
        }
    }
}
