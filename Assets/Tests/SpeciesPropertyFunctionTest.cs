using NUnit.Framework;
using Species;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Tests
{
    public class SpeciesPropertyFunctionTest
    {
        [Test]
        public void calculates_height_correctly()
        {
            // Arrange
            var expected = new Vector3(1, 1, 1);
            // Act
            var actual = SpeciesPropertyFunctions.GetHeight(1);
            // Assert
            Assert.AreEqual(expected.Y, actual.y, 0.01);
        }

        [Test]
        public void calculates_height_not_correctly()
        {
            // Arrange
            var expected = new Vector3(1, 1, 1);
            // Act
            var actual = SpeciesPropertyFunctions.GetHeight(0);
            // Assert
            Assert.AreNotEqual(expected.Y, actual.y);
        }

        [Test]
        public void calculates_green_color_correctly()
        {
            // Arrange
            var expected = new Color32(0, 100, 0, 100);
            // Act
            var actual = SpeciesPropertyFunctions.GetColorBySpeed(1f);
            // Assert
            Assert.AreEqual(expected.r,actual.r);
        }
        
        [Test]
        public void calculates_red_color_correctly()
        {
            // Arrange
            var expected = new Color32(229, 100, 0, 100);
            // Act
            var actual = SpeciesPropertyFunctions.GetColorBySpeed(0.1f);
            // Assert
            Assert.AreEqual(expected.r,actual.r);
        }
    }
}