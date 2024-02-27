using System.Windows.Forms;

namespace AutoArchiver.Helpers;

public class FileBrowserDialog
{
    public static string ChooseFolderUsingFolderExplorer()
    {
        FolderBrowserDialog openFileDlg = new FolderBrowserDialog();
        var result = openFileDlg.ShowDialog();
        if (result != DialogResult.Cancel)
        {
            return openFileDlg.SelectedPath;
        }
        else
        {
            return string.Empty;
        }
    }
}
