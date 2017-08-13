﻿namespace RedBlueGames.MulliganRenamer
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using NUnit.Framework;

    public class AddStringOpTests
    {
        [Test]
        public void AddPrefix_NullTarget_Adds()
        {
            // Arrange
            string name = null;
            var addStringOp = new AddStringOperation();
            addStringOp.Prefix = "Pre";

            var expected = new RenameResult() { new Diff("Pre", DiffOperation.Insertion) };

            // Act
            var result = addStringOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddPrefix_Empty_DoesNothing()
        {
            // Arrange
            var name = "Char_Hero_Spawn";
            var addStringOp = new AddStringOperation();
            addStringOp.Prefix = string.Empty;

            var expected = new RenameResult() { new Diff(name, DiffOperation.Equal) };

            // Act
            var result = addStringOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddPrefix_ValidPrefix_IsAdded()
        {
            // Arrange
            var name = "Hero_Spawn";
            var addStringOp = new AddStringOperation();
            addStringOp.Prefix = "Char_";

            var expected = new RenameResult()
            {
                new Diff("Char_", DiffOperation.Insertion),
                new Diff("Hero_Spawn", DiffOperation.Equal)
            };

            // Act
            var result = addStringOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddSuffix_Empty_DoesNothing()
        {
            // Arrange
            var name = "Char_Hero_Spawn";
            var addStringOp = new AddStringOperation();
            addStringOp.Suffix = string.Empty;

            var expected = new RenameResult() { new Diff("Char_Hero_Spawn", DiffOperation.Equal) };

            // Act
            var result = addStringOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddSuffix_ValidSuffix_IsAdded()
        {
            // Arrange
            var name = "Char_Hero";
            var addStringOp = new AddStringOperation();
            addStringOp.Suffix = "_Spawn";

            var expected = new RenameResult()
            {
                new Diff("Char_Hero", DiffOperation.Equal),
                new Diff("_Spawn", DiffOperation.Insertion)
            };

            // Act
            var result = addStringOp.Rename(name, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}