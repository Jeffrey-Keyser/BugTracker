using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// Used to load images for programming languages using the Devicon repo
// Found at https://github.com/konpa/devicon

namespace BugTracker.Data
{
    public class LanguageImages
    {

        public String getC()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/C.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getCsharp()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/CSharp.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getGo()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/Go.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getJava()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/Java.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }
    
        public String getJavaScript()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/JavaScript.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getPHP()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/PHP.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getPython()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/Python.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getRuby()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/Ruby.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getSQL()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/SQL.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getSwift()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/Swift.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

        public String getTypeScript()
        {
            string filepath = Path.GetFullPath("Data/DeviconLinks/TypeScript.txt");
            StreamReader sr = new StreamReader(filepath);
            return sr.ReadToEnd();
        }

    }

}
