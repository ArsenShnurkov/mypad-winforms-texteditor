namespace MyPad
{
    public interface ISaveableTab
    {
        string GetFileFullPathAndName();
        void SaveFile(string newNamePathFilenameExt);
    }
}
