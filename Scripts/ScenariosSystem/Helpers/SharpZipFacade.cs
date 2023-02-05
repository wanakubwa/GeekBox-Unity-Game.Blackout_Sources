using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Text;

/// <summary>
/// Klasa zwierajaca metody z biblioteki sharpZip dodanej za pomoca NuGat'a.
/// Jest to rodzaj fasady do API z SharpZip'a.
/// 
/// source: https://forum.unity.com/threads/copying-everyhting-under-streamingassets-to-persistentdatapath-on-android.431269/
/// </summary>
public class SharpZipFacade
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public static void ExtractZipFile(string archivePath, string outFolder)
    {
        using (Stream fsInput = File.OpenRead(archivePath))
        using (ZipFile zf = new ZipFile(fsInput))
        {
            foreach (ZipEntry zipEntry in zf)
            {
                if (!zipEntry.IsFile)
                {
                    // Ignore directories
                    continue;
                }

                String entryFileName = zipEntry.Name;
                // to remove the folder from the entry:
                //entryFileName = Path.GetFileName(entryFileName);
                // Optionally match entrynames against a selection list here
                // to skip as desired.
                // The unpacked length is available in the zipEntry.Size property.

                // Manipulate the output filename here as desired.
                var fullZipToPath = Path.Combine(outFolder, entryFileName);
                var directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                {
                    Directory.CreateDirectory(directoryName);
                }

                // 4K is optimum
                var buffer = new byte[4096];

                // Unzip file in buffered chunks. This is just as fast as unpacking
                // to a buffer the full size of the file, but does not waste memory.
                // The "using" will close the stream even if an exception occurs.
                using (var zipStream = zf.GetInputStream(zipEntry))
                using (Stream fsOutput = File.Create(fullZipToPath))
                {
                    StreamUtils.Copy(zipStream, fsOutput, buffer);
                }
            }
        }
    }

    // Compresses the files in the nominated folder, and creates a zip file 
    // on disk named as outPathname.
    public static void CreateZipArchiveFromDirectory(string outPathname, string folderName)
    {
        using (FileStream fsOut = File.Create(outPathname))
        using (var zipStream = new ZipOutputStream(fsOut))
        {
            //0-9, 9 being the highest level of compression
            zipStream.SetLevel(6);

            // This setting will strip the leading part of the folder path in the entries, 
            // to make the entries relative to the starting folder.
            // To include the full path for each entry up to the drive root, assign to 0.
            int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

            CompressFolder(folderName, zipStream, folderOffset);
        }
    }

    // Recursively compresses a folder structure
    private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
    {
        var files = Directory.GetFiles(path);

        foreach (var filename in files)
        {
            var fi = new FileInfo(filename);

            // Make the name in zip based on the folder
            var entryName = filename.Substring(folderOffset);

            // Remove drive from name and fix slash direction
            entryName = ZipEntry.CleanName(entryName);

            var newEntry = new ZipEntry(entryName);

            // Note the zip format stores 2 second granularity
            newEntry.DateTime = fi.LastWriteTime;

            // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003,
            // WinZip 8, Java, and other older code, you need to do one of the following: 
            // Specify UseZip64.Off, or set the Size.
            // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, 
            // you do not need either, but the zip will be in Zip64 format which
            // not all utilities can understand.
            //   zipStream.UseZip64 = UseZip64.Off;
            newEntry.Size = fi.Length;

            zipStream.PutNextEntry(newEntry);

            // Zip the file in buffered chunks
            // the "using" will close the stream even if an exception occurs
            var buffer = new byte[4096];
            using (FileStream fsInput = File.OpenRead(filename))
            {
                StreamUtils.Copy(fsInput, zipStream, buffer);
            }
            zipStream.CloseEntry();
        }

        // Recursively call CompressFolder on all folders in path
        var folders = Directory.GetDirectories(path);
        foreach (var folder in folders)
        {
            CompressFolder(folder, zipStream, folderOffset);
        }
    }

    //CreateTarGZ(@"c:\temp\gzip-test.tar.gz", @"c:\data");
    public static void CreateTarGZFromDirectory(string tgzFilename, string sourceDirectory)
    {
        Stream outStream = File.Create(tgzFilename);
        Stream gzoStream = new GZipOutputStream(outStream);
        TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzoStream);

        // Note that the RootPath is currently case sensitive and must be forward slashes e.g. "c:/temp"
        // and must not end with a slash, otherwise cuts off first char of filename
        // This is scheduled for fix in next release
        tarArchive.RootPath = sourceDirectory.Replace('\\', '/');
        if (tarArchive.RootPath.EndsWith("/"))
            tarArchive.RootPath = tarArchive.RootPath.Remove(tarArchive.RootPath.Length - 1);

        AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);

        tarArchive.Close();
    }

    public static void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
    {
        // Optionally, write an entry for the directory itself.
        // Specify false for recursion here if we will add the directory's files individually.
        //
        TarEntry tarEntry = TarEntry.CreateEntryFromFile(sourceDirectory);
        tarArchive.WriteEntry(tarEntry, false);

        // Write each file to the tar.
        //
        string[] filenames = Directory.GetFiles(sourceDirectory);
        foreach (string filename in filenames)
        {
            tarEntry = TarEntry.CreateEntryFromFile(filename);
            tarArchive.WriteEntry(tarEntry, true);
        }

        if (recurse)
        {
            string[] directories = Directory.GetDirectories(sourceDirectory);
            foreach (string directory in directories)
                AddDirectoryFilesToTar(tarArchive, directory, recurse);
        }
    }

    public static void ExtractTGZ(string gzArchiveName, string destFolder)
    {
        Stream inStream = File.OpenRead(gzArchiveName);
        Stream gzipStream = new GZipInputStream(inStream);

        TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.UTF8);
        tarArchive.ExtractContents(destFolder);
        tarArchive.Close();

        gzipStream.Close();
        inStream.Close();

    }

    #endregion

    #region Enums



    #endregion
}
