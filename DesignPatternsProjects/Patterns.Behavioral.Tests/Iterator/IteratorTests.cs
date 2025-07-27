using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Patterns.Behavioral.Iterator.Tests
{
    public class IteratorTests
    {
        private readonly ITestOutputHelper _output;

        public IteratorTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void PreOrderTraversal_ShouldVisitNodesInCorrectOrder()
        {
            // Arrange
            var tree = CreateTestTree();
            var iterator = tree.GetIterator(TraversalStrategy.PreOrder);
            var expected = new[] { 1, 2, 4, 5, 3, 6, 7 };

            // Act
            var result = GetTraversalResult(iterator);

            // Assert
            Assert.Equal(expected, result);
            _output.WriteLine($"PreOrder traversal: {string.Join(", ", result)}");
        }

        [Fact]
        public void InOrderTraversal_ShouldVisitNodesInCorrectOrder()
        {
            // Arrange
            var tree = CreateTestTree();
            var iterator = tree.GetIterator(TraversalStrategy.InOrder);
            var expected = new[] { 4, 2, 5, 1, 6, 3, 7 };

            // Act
            var result = GetTraversalResult(iterator);

            // Assert
            Assert.Equal(expected, result);
            _output.WriteLine($"InOrder traversal: {string.Join(", ", result)}");
        }

        [Fact]
        public void PostOrderTraversal_ShouldVisitNodesInCorrectOrder()
        {
            // Arrange
            var tree = CreateTestTree();
            var iterator = tree.GetIterator(TraversalStrategy.PostOrder);
            var expected = new[] { 4, 5, 2, 6, 7, 3, 1 };

            // Act
            var result = GetTraversalResult(iterator);

            // Assert
            Assert.Equal(expected, result);
            _output.WriteLine($"PostOrder traversal: {string.Join(", ", result)}");
        }

        [Fact]
        public void LevelOrderTraversal_ShouldVisitNodesInCorrectOrder()
        {
            // Arrange
            var tree = CreateTestTree();
            var iterator = tree.GetIterator(TraversalStrategy.LevelOrder);
            var expected = new[] { 1, 2, 3, 4, 5, 6, 7 };

            // Act
            var result = GetTraversalResult(iterator);

            // Assert
            Assert.Equal(expected, result);
            _output.WriteLine($"LevelOrder traversal: {string.Join(", ", result)}");
        }

        [Fact]
        public void ForEachLoop_ShouldUseDefaultStrategy()
        {
            // Arrange
            var tree = CreateTestTree();
            var expected = new[] { 4, 2, 5, 1, 6, 3, 7 }; // InOrder par défaut

            // Act
            var result = tree.ToArray();

            // Assert
            Assert.Equal(expected, result);
            _output.WriteLine($"Default traversal (InOrder): {string.Join(", ", result)}");
        }

        [Fact]
        public void Iterator_ShouldHandleEmptyTree()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            foreach (var strategy in Enum.GetValues<TraversalStrategy>())
            {
                var iterator = tree.GetIterator(strategy);
                Assert.Empty(GetTraversalResult(iterator));
                _output.WriteLine($"Empty tree traversal with {strategy} strategy: No elements");
            }
        }

        [Fact]
        public void Iterator_ShouldHandleSingleNodeTree()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Add(1);
            var expected = new[] { 1 };

            // Act & Assert
            foreach (var strategy in Enum.GetValues<TraversalStrategy>())
            {
                var iterator = tree.GetIterator(strategy);
                var result = GetTraversalResult(iterator);
                Assert.Equal(expected, result);
                _output.WriteLine($"Single node tree traversal with {strategy} strategy: {string.Join(", ", result)}");
            }
        }

        [Fact]
        public void Iterator_ShouldResetCorrectly()
        {
            // Arrange
            var tree = CreateTestTree();

            // Act & Assert
            foreach (var strategy in Enum.GetValues<TraversalStrategy>())
            {
                var iterator = tree.GetIterator(strategy);
                var firstTraversal = GetTraversalResult(iterator);
                iterator.Reset();
                var secondTraversal = GetTraversalResult(iterator);

                Assert.Equal(firstTraversal, secondTraversal);
                _output.WriteLine($"{strategy} traversal after reset matches initial traversal");
            }
        }

        [Fact]
        public void Iterator_ShouldThrowWhenAccessingCurrentBeforeStarting()
        {
            // Arrange
            var tree = CreateTestTree();

            // Act & Assert
            foreach (var strategy in Enum.GetValues<TraversalStrategy>())
            {
                var iterator = tree.GetIterator(strategy);
                var exception = Assert.Throws<InvalidOperationException>(() => iterator.Current);
                _output.WriteLine($"{strategy} iterator throws when accessing Current before starting: {exception.Message}");
            }
        }

        [Fact]
        public void Iterator_ShouldDisposeCorrectly()
        {
            // Arrange
            var tree = CreateTestTree();

            // Act & Assert
            foreach (var strategy in Enum.GetValues<TraversalStrategy>())
            {
                var iterator = tree.GetIterator(strategy);
                iterator.Dispose();
                var exception = Assert.Throws<ObjectDisposedException>(() => iterator.MoveNext());
                _output.WriteLine($"{strategy} iterator throws when used after disposal: {exception.Message}");
            }
        }

        private static BinaryTree<int> CreateTestTree()
        {
            var tree = new BinaryTree<int>();
            foreach (var value in new[] { 1, 2, 3, 4, 5, 6, 7 })
            {
                tree.Add(value);
            }
            return tree;
        }

        private static int[] GetTraversalResult(ITreeIterator<int> iterator)
        {
            var result = new List<int>();
            while (iterator.MoveNext())
            {
                result.Add(iterator.Current);
            }
            return result.ToArray();
        }
    }
}
