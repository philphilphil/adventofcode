using System;

public static class Day07
{

    public static void Run()
    {
        var input = File.ReadAllLines("../Inputs/07");

        var baseDir = new FileSystemElement("/", Type.Folder);
        var currentDir = baseDir;

        foreach (var line in input.Skip(1))
        {
            if (line == "$ ls")
            {
                // nothing todo here
            }
            else if (line.StartsWith("$ cd"))
            {
                string name = line.Substring(5, line.Length - 5);

                if (name == "..")
                {
                    currentDir = currentDir.Father;
                }
                else
                {
                    currentDir = currentDir.Children?.Where(x => x.Name == name).FirstOrDefault();
                }

            }
            else if (line.StartsWith("dir "))
            {
                string name = line.Substring(4, line.Length - 4);
                var folder = new FileSystemElement(name, Type.Folder);
                folder.Father = currentDir;
                currentDir.AddChild(folder);
            }
            else
            {
                // its a file
                var fileSplit = line.Split(" ");
                var file = new FileSystemElement(fileSplit[1], Type.File);
                int size = int.Parse(fileSplit[0]);
                file.Size = size;
                currentDir.AddChild(file);
            }
        }

        //PrintTree(baseDir);

        // PART 1
        //find folders with total size below 100000
        Console.WriteLine("Answer P1: " + SumUpFoldersForPart1(baseDir));

        // PART 2
        int totalSpace = 70000000;
        int spaceRequiredForUpdate = 30000000;
        int baseDirSpace = baseDir.GetTotalSize();
        int unusedSpace = totalSpace - baseDirSpace;
        int spaceRequired = spaceRequiredForUpdate - unusedSpace;

        List<int> allDirectorySizes = new List<int>();
        getAllDirectorySizes(allDirectorySizes, baseDir);

        int folderSizeToDelete = allDirectorySizes.Where(x => x > spaceRequired).Order().First();
        Console.WriteLine("Answer P2 " + folderSizeToDelete);
    }

    private static void getAllDirectorySizes(List<int> allDirectorySizes, FileSystemElement dir)
    {
        if (dir.Type == Type.Folder)
            allDirectorySizes.Add(dir.GetTotalSize());

        for (int i = 0; i < dir.Children?.Count; i++)
        {
            getAllDirectorySizes(allDirectorySizes, dir.Children[i]);
        }
    }

    public static void PrintTree(FileSystemElement tree, int depth = 0)
    {
        string space = string.Concat(Enumerable.Repeat("  ", depth));
        if (tree.Type == Type.Folder)
        {
            Console.WriteLine(space + " " + tree.Name + " " + tree.GetTotalSize());
        }
        else
        {
            Console.WriteLine(space + " *" + tree.Name + " " + tree.Size);
        }

        for (int i = 0; i < tree?.Children?.Count; i++)
        {
            PrintTree(tree.Children[i], depth + 1);
        }
    }


    public static int SumUpFoldersForPart1(FileSystemElement fse)
    {
        int size = 0;
        if (fse.Children == null) return size;

        foreach (var child in fse.Children.Where(x => x.Type == Type.Folder))
        {
            if (child.GetTotalSize() <= 100000)
                size += child.GetTotalSize();

            size += SumUpFoldersForPart1(child);
        }
        return size;
    }
}

public class FileSystemElement
{
    public int Size { get; set; }
    public string? Name { get; set; }
    public Type Type { get; set; }
    public List<FileSystemElement>? Children { get; set; }
    public FileSystemElement? Father { get; set; }

    public FileSystemElement(string name, Type type)
    {
        this.Name = name;
        this.Type = type;
    }

    public void AddChild(FileSystemElement fse)
    {
        if (this.Children == null) this.Children = new List<FileSystemElement>();
        this.Children.Add(fse);
    }

    public int GetTotalSize()
    {
        int size = 0;
        if (this.Children == null) return size;

        foreach (var child in this.Children)
        {
            size += child.Size;
            size += child.GetTotalSize();
        }

        return size;
    }
}

public enum Type
{
    Folder,
    File
}


