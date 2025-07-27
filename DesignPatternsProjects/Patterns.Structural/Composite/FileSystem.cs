using System;
using System.Collections.Generic;

namespace Patterns.Structural.Composite
{
    /// <summary>
    /// Component - FileSystemItem
    /// </summary>
    public abstract class FileSystemItem
    {
        protected string _name;
        protected string _path;

        public FileSystemItem(string name)
        {
            _name = name;
            _path = name;
        }

        public abstract void Add(FileSystemItem item);
        public abstract void Remove(FileSystemItem item);
        public abstract void Display(string indent = "");
        public abstract int GetSize();

        public string GetPath()
        {
            return _path;
        }

        public void UpdatePath(string parentPath)
        {
            _path = System.IO.Path.Combine(parentPath, _name);
        }
    }

    /// <summary>
    /// Leaf - File
    /// </summary>
    public class File : FileSystemItem
    {
        private int _size;

        public File(string name, int size) : base(name)
        {
            _size = size;
        }

        public override void Add(FileSystemItem item)
        {
            throw new InvalidOperationException("Cannot add items to a file");
        }

        public override void Remove(FileSystemItem item)
        {
            throw new InvalidOperationException("Cannot remove items from a file");
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}File: {_name} ({_size} bytes)");
        }

        public override int GetSize()
        {
            return _size;
        }
    }

    /// <summary>
    /// Composite - Directory
    /// </summary>
    public class Directory : FileSystemItem
    {
        private List<FileSystemItem> _children;

        public Directory(string name) : base(name)
        {
            _children = new List<FileSystemItem>();
        }

        public override void Add(FileSystemItem item)
        {
            _children.Add(item);
            item.UpdatePath(this._path);
        }

        public override void Remove(FileSystemItem item)
        {
            _children.Remove(item);
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}Directory: {_name} ({GetSize()} bytes)");
            foreach (var item in _children)
            {
                item.Display(indent + "  ");
            }
        }

        public override int GetSize()
        {
            int totalSize = 0;
            foreach (var item in _children)
            {
                totalSize += item.GetSize();
            }
            return totalSize;
        }

        public List<FileSystemItem> GetChildren()
        {
            return _children;
        }
    }
}
