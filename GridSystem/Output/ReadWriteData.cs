using GridSystem.Ants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Output
{
    public static class ReadWriteData
    {
        static string path= @"C:\Users\nagib\Desktop\Data.txt";
        //public static string path = System.IO.Directory.GetCurrentDirectory();
        //public static string projectDirectory2 = Directory.GetParent(path).Parent.FullName+ "\\Resources\\ProjectData.txt";
        //public static string path = Environment.CurrentDirectory;
        //public static string projectDirectory = Directory.GetParent(path).Parent.FullName+ "\\Resources\\ProjectData.txt";

        public static bool Write(List<Tactic> tactics)
        {
            bool ok = false;
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    foreach (Tactic tactic in tactics)
                    {
                        if (tactic != null)
                        {
                            string tacticTxt = MakeTacticToString(tactic);
                            file.WriteLine(tacticTxt);
                        }
                    }
                    ok = true;
                }
                return ok;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(path)).Attributes;
                Console.Write("UnAuthorizedAccessException: Unable to access file. ");
                if ((attr & FileAttributes.ReadOnly) > 0)
                    Console.Write("The file is read-only.");
                return false;
            }
        }
        
        public static bool Read(out List<Tactic> tactics)
        {
            try
            {
                tactics = new List<Tactic>();
                string[] txtData = System.IO.File.ReadAllLines(path);
                bool returnMode = false;
                bool allTheSame = false;
                bool towards = false;
                bool from = false;
                int plusPoints = 0;
                int minusPoints = 0;
                int totalTimes = 0;
                double raito = 0;
                foreach (string line in txtData)
                {
                    string[] tempLine = line.Split('*');
                    if (line.Length > 0)
                    {
                        try
                        {
                            //create new tactic
                            //ToDo: cast data types from line

                            Tactic tactic = new Tactic(returnMode, allTheSame, towards, from, plusPoints, minusPoints, totalTimes, raito);
                            tactics.Add(tactic);
                        }
                        catch (FormatException)
                        {
                            return false;
                        }
                    }
                }
                if (tactics.Count <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                tactics = null;
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
                tactics = null;
                return false;
            }
        }

        //make Tactic object into a string, values separated by *
        private static string MakeTacticToString(Tactic tactic)
        {
            //todo remove all spaces
            string tacticTxt = String.Empty;
            return tacticTxt;
        }
    }
}
