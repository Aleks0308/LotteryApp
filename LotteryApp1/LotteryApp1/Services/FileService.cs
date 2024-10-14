using PCLStorage;
using System.Threading.Tasks;

namespace LotteryApp.Services
{
    public class FileService : IFileService
    {
        public async Task WriteAllText(string subfolder, string fileName, string text)
        {
            IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(subfolder,
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(fileName,
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(text);
        }

        public async Task<string> ReadAllText(string subfolder, string fileName)
        {
            IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync(subfolder);
            IFile file = await folder.GetFileAsync(fileName);

            if(file == null) 
                return null;

            return await file.ReadAllTextAsync();
        }
    }
}
