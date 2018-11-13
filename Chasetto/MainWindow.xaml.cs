using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Chasetto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region On Loaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem()
                {
                    // Set the header and path
                    Header = drive,
                    //And the full path
                    Tag = drive
                };

                //Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // Add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }

        #endregion On Loaded

        #region Folder Expanded

        /// <summary>
        /// Handle When a folder is expanded find its sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            var item = (TreeViewItem)sender;

            //if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            //clear dummy data
            item.Items.Clear();

            // Get full path
            var fullPath = (string)item.Tag;

            #endregion Initial Checks

            #region Get Directories

            // Create a blank list for directories
            var directories = new List<string>();

            //try and get directories from folder
            //ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { }

            // For each directory...
            directories.ForEach(directoryPath =>
            {
                //Create dirctory item
                var subItem = new TreeViewItem()
                {
                    //Set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    // And tag as full path
                    Tag = directoryPath
                };

                //Add dummy item so we can expand fodler
                subItem.Items.Add(null);

                // Handle expanding
                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion Get Directories

            #region Get Files

            // Create a blank list for directories
            var files = new List<string>();

            //try and get directories from folder
            //ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch { }

            // For each file...
            files.ForEach(filePath =>
            {
                //Create file item
                var subItem = new TreeViewItem()
                {
                    //Set header as file name
                    Header = GetFileFolderName(filePath),
                    // And tag as full path
                    Tag = filePath
                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion Get Files
        }

        #endregion Folder Expanded

        #region Helpers

        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //if we have no path return empty
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // Make all slahes = back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
            {
                return path;
            }

            // return the name after the last backslash
            return path.Substring(lastIndex + 1);
        }

        #endregion Helpers
    }
}