namespace pendrive_authorization
{
    internal class Program
    {
        static string authorizationFileName = "authorization.txt";
        static string authorizationPassword = "password";

        static void Main(string[] args)
        {

            if (checkAuthorization())
            {
                Console.WriteLine("Authorization was successful...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Authorization failed...");
                Console.ReadKey();
            }
        }

        private static bool checkAuthorization()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if(drive.DriveType == DriveType.Removable)
                {
                    if(findAuthorizationFile(drive.RootDirectory.FullName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool findAuthorizationFile(string drivePath)
        {
            string filePath = Path.Combine(drivePath, authorizationFileName);

            if(File.Exists(filePath))
            {
                if(loadAuthorizationFile(filePath))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool loadAuthorizationFile(string filePath)
        {
            try
            {
                string readedAuthorizationPassword = File.ReadAllText(filePath);

                if(readedAuthorizationPassword == authorizationPassword)
                {
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            return false;
        }
    }
}