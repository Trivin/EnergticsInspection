﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MobileApp.Methods
{
    public static class FTPClient
    {
        static string serverPath = @"ftp://server/folder/";
        static WebClient wc = new WebClient()
        {
            BaseAddress = @"",
            Credentials = new NetworkCredential("login", "password")
        };
        public static void UploadFile(string fileName)
        {
            wc.UploadProgressChanged += new UploadProgressChangedEventHandler(wc_UploadProgressChanged);
            string nameFiles = Path.GetFileName(fileName);
            wc.UploadFileCompleted += new UploadFileCompletedEventHandler(wc_UploadFileCompleted);
            wc.UploadFileAsync(new Uri(serverPath + Path.GetFileName(fileName)), fileName);
        }

        public static void DownloadFile(string fileName)
        {
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(serverPath + Path.GetFileName(fileName)), @"d:/" + Path.GetFileName(fileName));
        }

        static void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

        }

        static void wc_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {

        }

        static void wc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {

        }
    }
}