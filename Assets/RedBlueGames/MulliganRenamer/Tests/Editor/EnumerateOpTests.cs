﻿namespace RedBlueGames.MulliganRenamer
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using NUnit.Framework;

    public class EnumerateOpTests
    {
        [Test]
        public void Rename_NullTarget_AddsCount()
        {
            // Arrange
            string name = null;
            var enumerateOp = new EnumerateOperation();
            enumerateOp.CountFormat = "0";
            enumerateOp.StartingCount = 0;

            var expected = new RenameResult() { new Diff("0", DiffOperation.Insertion) };

            // Act
            var result = enumerateOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RenameFormat_NoFormat_DoesNothing()
        {
            // Arrange
            var name = "Char_Hero";
            var enumerateOp = new EnumerateOperation();
            enumerateOp.CountFormat = string.Empty;

            var expected = new RenameResult() { new Diff(name, DiffOperation.Equal) };

            // Act
            var result = enumerateOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RenameFormat_SingleDigitFormat_AddsCount()
        {
            // Arrange
            var name = "Char_Hero";
            var enumerateOp = new EnumerateOperation();
            enumerateOp.CountFormat = "0";
            enumerateOp.StartingCount = 0;

            var expected = new RenameResult()
            {
                new Diff(name, DiffOperation.Equal),
                new Diff("0", DiffOperation.Insertion)
            };

            // Act
            var result = enumerateOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RenameCount_CountSeveralItems_CountsUp()
        {
            // Arrange
            var names = new string[]
            {
                "BlockA",
                "BlockB",
                "BlockC",
                "BlockD",
                "BlockE",
            };
            var enumerateOp = new EnumerateOperation();
            enumerateOp.CountFormat = "0";
            enumerateOp.StartingCount = 1;

            var expectedRenameResults = new RenameResult[]
            {
                new RenameResult() { new Diff("BlockA", DiffOperation.Equal), new Diff("1", DiffOperation.Insertion) },
                new RenameResult() { new Diff("BlockB", DiffOperation.Equal), new Diff("2", DiffOperation.Insertion) },
                new RenameResult() { new Diff("BlockC", DiffOperation.Equal), new Diff("3", DiffOperation.Insertion) },
                new RenameResult() { new Diff("BlockD", DiffOperation.Equal), new Diff("4", DiffOperation.Insertion) },
                new RenameResult() { new Diff("BlockE", DiffOperation.Equal), new Diff("5", DiffOperation.Insertion) },
            };

            // Act
            var results = new List<RenameResult>(names.Length);
            for (int i = 0; i < names.Length; ++i)
            {
                results.Add(enumerateOp.Rename(names[i], i));
            }

            // Assert
            Assert.AreEqual(
                expectedRenameResults.Length,
                results.Count,
                "Expected Results and results should have the same number of entries but didn't.");
            for (int i = 0; i < results.Count; ++i)
            {
                var expected = expectedRenameResults[i];
                Assert.AreEqual(expected, results[i]);
            }
        }

        [Test]
        public void RenameStartingCount_StartFromNonZero_AddsCorrectCount()
        {
            // Arrange
            var name = "Char_Hero";
            var enumerateOp = new EnumerateOperation();
            enumerateOp.CountFormat = "0";
            enumerateOp.StartingCount = -1;


            var expected = new RenameResult()
            {
                new Diff("Char_Hero", DiffOperation.Equal),
                new Diff("-1", DiffOperation.Insertion)
            };

            // Act
            var result = enumerateOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RenameStartingCount_InvalidFormat_IsIgnored()
        {
            // Arrange
            var name = "Char_Hero";
            var enumerateOp = new EnumerateOperation();
            enumerateOp.CountFormat = "s";
            enumerateOp.StartingCount = 100;

            var expected = new RenameResult() { new Diff(name, DiffOperation.Equal) };

            // Act
            var result = enumerateOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}