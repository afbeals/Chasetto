using System.Collections.ObjectModel;
using System.Linq;

namespace Chasetto
{
    /// <summary>
    /// The view modle for the applications main Directory View
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion Public Properties


        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // Get the logical Drives
            var children = DirectoryStructure.GetLogicalDrives();

            // Create the view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)
                )
            );
        }

        #endregion Constructor

    }
}
