﻿/**
 * FolderWalker: Walk the folder which containing the image and get the rest images.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Niv
{
    class FolderWalker
    {
        // Supported image format, used to recognize the image files.
        static string SUPPORTED_IMAGE_EXT = ".jpg.jpeg.png.gif.bmp.ico.tif.tiff";

        // The fullname of current loaded folder
        string currentFolderName;

        // Image info list
        List<ImageInfo> imageInfos = new List<ImageInfo>();

        // The index of last displayed image. Used to see if they are adjacent or at the two ends.
        int lastIndex = -1;

        // The index of current displaying image in the image list
        int _currentIndex = -1;
        
        public int currentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                lastIndex = _currentIndex;
                _currentIndex = value;
            }
        }

        // Get the ImageInfo of current image
        public ImageInfo currentImageInfo
        {
            get
            {
                return  imageInfos.Count > 0 ? imageInfos[_currentIndex] : null;
            }
        }

        // The count of images
        public int count
        {
            get
            {
                return imageInfos.Count;
            }
        }

        // Check if a file is a supported format
        public static bool isFormatSupported(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            return SUPPORTED_IMAGE_EXT.IndexOf(ext) > -1;
        }

        // Check if the last image switch is jumping between the ends of image list.
        public bool isJumpBetweenEnds()
        {
            return this.count > 2 && Math.Abs(currentIndex - lastIndex) > 1;
        }

        public void loadFolder(string folderName)
        {
            string folderPath = Path.GetDirectoryName(folderName).ToLower();
            if (folderPath.Equals(currentFolderName)) return;

            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists)
            {
                imageInfos.Clear();
                FileInfo[] fis = di.GetFiles();
                int i = 0;
                foreach (FileInfo fi in fis)
                {
                    string filename = fi.FullName;
                    if (isFormatSupported(filename))
                    {
                        imageInfos.Add(new ImageInfo(filename));
                        if (folderName.ToLower() == filename.ToLower())
                            currentIndex = i;
                        i++;
                    }
                }
            }

            //MessageBox.Show(" Count: " + imageInfos.Count
            //    + "\n Index: " + currentIndex
            //    + "\n folderPath: " + folderPath
            //    + "\n currentFolderName: " + currentFolderName
            //    );

            currentFolderName = folderPath;
        }

        // Switch to the previous image in the list and return if the switch success.
        // Switch will fail if imageList.Count < 2
        public bool switchBackward()
        {
            if (imageInfos.Count < 2) return false;

            currentIndex--;
            if (currentIndex == -1) currentIndex = imageInfos.Count - 1;
            return true;
        }

        // Switch to the next image in the list.
        public bool switchForward()
        {
            if (imageInfos.Count < 2) return false;

            currentIndex++;
            if (currentIndex == imageInfos.Count) currentIndex = 0;
            return true;
        }
        
        // Remove current file info from the list
        public void removeCurrentImageInfo()
        {
            imageInfos.RemoveAt(currentIndex);
            if (currentIndex == imageInfos.Count) currentIndex = 0;
        }

        // EOC
    }
}
