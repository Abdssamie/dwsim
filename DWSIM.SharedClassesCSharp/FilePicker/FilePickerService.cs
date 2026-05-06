using DWSIM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.SharedClassesCSharp.FilePicker
{
    public class FilePickerService : IFilePickerService
    {
        private static FilePickerService _instance;
        public static IFilePickerService GetInstance()
        {
            if (_instance == null)
                _instance = new FilePickerService();

            return _instance;
        }


        private Func<IFilePicker> _filePickerFactory = null;
        public void SetFilePickerFactory(Func<IFilePicker> filePickerFactory)
        {
            _filePickerFactory = filePickerFactory;
        }

        private FilePickerService()
        {

        }

        public IFilePicker GetFilePicker()
        {
            if (_filePickerFactory == null)
            {
                // TODO: Implement a better cross-platform file picker that doesn't depend on Windows.Forms.
                // For now, platform-specific UI must register a factory.
                throw new Exception("FilePicker factory not set. Platform-specific UI must register a factory.");
            }
            return _filePickerFactory();
        }
    }
}
