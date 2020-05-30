using System;
using System.Linq;
using NUnit.Framework;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="Measure"/>
    /// /// </summary>
    public partial class MeasureTests
    {
        /// <summary>
        /// Ensures that <see cref="Measure.Duration(IMetric)"/> throws an
        /// instance of <see cref="ArgumentNullException"/> when <c>null</c>
        /// is passed as a parameter, and that the exception's
        /// <see cref="ArgumentException.ParamName"/> propery was the expected
        /// parameter name.
        /// </summary>
        [Test]
        public void MeasureDurationNoTagsThrowsForNullParameter()
        {
            var method = typeof(Measure).GetMethod("Duration",
                new[]{ typeof(IMetric) })!;
            var parameter = method.GetParameters().First();

            var ex = Assert.Throws<ArgumentNullException>(
                () => Measure.Duration(null!));

            Assert.AreEqual(parameter.Name, ex.ParamName);
        }

        /// <summary>
        /// Ensures that <see cref="Measure.Duration(IMetric)"/> returns an
        /// instance of <see cref="Measure.DurationMeasure"/>
        /// </summary>
        [Test]
        public void MeasureDurationNoTagsReturnsDurationMeasure()
        {
            _ = Measure.Duration(new EnabledNonThrowingMetric());

            // If the method returned successfully, then it can only be an
            // instance of DurationMeasure, since DurationMeasure is a struct.
            Assert.Pass();
        }

        /// <summary>
        /// Ensures that <see cref="Measure.Duration(IMetric)"/> returns an
        /// object which can be disposed.
        /// </summary>
        [Test]
        public void MeasureDurationNoTagsCanBeDisposed()
        {
            using var x = Measure.Duration(new EnabledNonThrowingMetric());

            // This code will fail to compile if the returned value cannot be
            // dispoed (i.e. not IDisposable or does not have a Dispose method)
            Assert.Pass();
        }

        /// <summary>
        /// Ensures that <see cref="Measure.Duration{TTags}(IMetric, TTags)"/>
        /// throws an instance of <see cref="ArgumentNullException"/> when
        /// <c>null</c> is passed as a parameter, and that the exception's
        /// <see cref="ArgumentException.ParamName"/> propery was the expected
        /// parameter name.
        /// </summary>
        [Test]
        public void MeasureDurationWithTagsThrowsForNullParameter()
        {
            var method = typeof(Measure).GetMethod("Duration",
                new[]{ typeof(IMetric) })!;
            var parameter = method.GetParameters().First();

            var ex = Assert.Throws<ArgumentNullException>(
                () => Measure.Duration(null!, new object()));

            Assert.AreEqual(parameter.Name, ex.ParamName);
        }

        /// <summary>
        /// Ensures that <see cref="Measure.Duration{TTags}(IMetric,TTags)"/>
        /// returns an instance of <see cref="Measure.DurationMeasure{TTags}"/>
        /// </summary>
        [Test]
        public void MeasureDurationWithTagsReturnsDurationMeasure()
        {
            _ = Measure.Duration(new EnabledNonThrowingMetric(), new object());

            // If the method returned successfully, then it can only be an
            // instance of DurationMeasure, since DurationMeasure is a struct.
            Assert.Pass();
        }

        /// <summary>
        /// Ensures that <see cref="Measure.Duration{TTags}(IMetric,TTags)"/>
        /// returns an object which can be disposed.
        /// </summary>
        [Test]
        public void MeasureDurationWithTagsCanBeDisposed()
        {
            using var x = Measure.Duration(new EnabledNonThrowingMetric(),
                new object());

            // This code will fail to compile if the returned value cannot be
            // dispoed (i.e. not IDisposable or does not have a Dispose method)
            Assert.Pass();
        }
    }
}
