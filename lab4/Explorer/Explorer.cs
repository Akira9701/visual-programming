using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using System.IO;
using System.Linq;

namespace Explorer;
    
internal class Explorer : INotifyPropertyChanged
{
    public Explorer()
    {
        _path = "";
        _directory = [];
        SetDirectory();
    }

    public ObservableCollection<CollectionElement> ShownDirectory
    {
        get
        {
            return _directory;
        }
        set
        {
            _ = SetField(ref _directory, value);
        }
    }

    public string CurrentPath
    {
        get
        {
            return _path;
        }
        set
        {
            SetPath(ref _path, value);
        }
    }

    private string _path;

    private ObservableCollection<CollectionElement> _directory;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetDirectory()
    {   
        if (!string.IsNullOrEmpty(_path))
        {
            _directory.Clear();
            string[] directories = Directory.GetDirectories(_path);
            string[] files = Directory.GetFiles(_path);

            _directory.Add(new CollectionElement("..", GetIcon(DeleteLastDirectory(_path))));

            foreach (string directory in directories)
            {
                string name = directory.Replace(_path, "");
                _directory.Add(new CollectionElement(name, GetIcon(AddDirectory(_path, name))));
            }
            foreach (string file in files)
            {
                string name = file.Replace(_path, "");
                _directory.Add(new CollectionElement(name, GetIcon(AddDirectory(_path, name))));
            }
        }
        else
        {
            _directory.Clear();
            List<string> diskNames = DriveInfo.GetDrives().Select(disk => disk.Name).ToList();
            foreach (string name in diskNames)
                _directory.Add(new CollectionElement(name, GetIcon(_path)));
        }
    }

    private string DeleteLastDirectory(string path)
    {
        path = path[..^1];
        if (path.Contains('\\'))
        {
            List<string> splitPath = path.Split('\\').ToList();
            splitPath.RemoveAt(splitPath.Count - 1);
            path = string.Join('\\', splitPath);
            path += "\\";
        }
        else
            path = "";

        return path;
    }

    private string AddDirectory(string path, string directory)
    {
        path += directory;
        if (!directory.Contains('\\'))
            path += "\\";

        return path;
    }

    private void SetPath(ref string _path, string value)
    {
        if (value.Equals(".."))
            _path = DeleteLastDirectory(_path);
        else
        {
            string tempPath = AddDirectory(_path, value);
            if (Directory.Exists(tempPath))
                _path = tempPath;
        }

        SetDirectory();
    }

    private Bitmap GetIcon(string path)
    {
        if (Directory.Exists(path))
            return new Bitmap("images\\folder.png");
        if (string.IsNullOrEmpty(path) || path.Count(ch => ch.Equals("\\")) == 1)
            return new Bitmap("images\\disk.png");
        else
            return new Bitmap("images\\file.png");
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}