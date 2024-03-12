using Avalonia.Media.Imaging;

class CollectionElement(string name, Bitmap icon)
{
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public Bitmap Icon
    {
        get
        {
            return _icon;
        }
        set
        {
            _icon = value;
        }
    }

    private string _name = name;
    private Bitmap _icon = icon;
}