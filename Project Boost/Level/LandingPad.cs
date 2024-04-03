using Godot;

public partial class LandingPad : CsgBox3D
{
    [Export(PropertyHint.File, "*.tscn")]
    private string filePath;

    public string FilePath
    {
        get
        {
            return filePath;
        }
        set
        {
            filePath = value;
        }
    }
}