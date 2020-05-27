using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// Used to load images for programming languages using the Devicon repo
// Found at https://github.com/konpa/devicon

// TODO: Switch to Relative Path
// See GetFullPath()

namespace BugTracker.Data
{
    public class LanguageImages
    {

        public String getC()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/C.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getCsharp()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/Csharp.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getGo()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/Go.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getJava()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/Java.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }
    
        public String getJavaScript()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/JavaScript.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getPHP()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/PHP.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getPython()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/Python.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getRuby()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/Ruby.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getSQL()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/SQL.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getSwift()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/Swift.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

        public String getTypeScript()
        {
            StreamReader sr = new StreamReader("C:/Users/Jeffrey/source/repos/BugTracker/BugTracker/Data/DeviconLinks/TypeScript.txt");
            string strText = sr.ReadToEnd();
            return strText;
        }

    }

}
