using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VscpWorksSharp.Settings
{
    public class ProjectSettings
    {
        static ProjectSettings _settings = null;

        #region Setting fields
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
		
		/// <summary>
		/// Is set to true, a second chanel will be opened to receive events. Otherwise the main channel will be polled for events.
		/// </summary>
		public bool UseBlockingEventReceiver { get; set; }
		#endregion

		// Todo: Create setting that controls how the Class and Type ids are translated to strings. From DB, from the web, etc.

		private ProjectSettings()// Singleton
        {
        }

        #region Save and Load
        public static string GetSettingFilePath()
        {
            // TODO: Change the path to the UserSettings dir. Non-admins can't write in C:\Program Files\VSCP
            string settingsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString());
            settingsPath = Path.Combine(settingsPath, "settings.xml");
            return settingsPath;
        }

        public void Save()
        {
            Serializer.SerializeObject(this, GetSettingFilePath());
        }

        private static ProjectSettings Load()
        {
            if (File.Exists(GetSettingFilePath()))
            {
                return Serializer.DeSerializeObject<ProjectSettings>(GetSettingFilePath());
            }
            else
            {
                ProjectSettings settings = new ProjectSettings();
                // Set default values and save.
                settings.UserName = "admin";
                settings.Password = "secret";
                settings.ConnectionString = "127.0.0.1:9598";
                settings.Save();
                return settings;
            }
        }

        public static ProjectSettings GetSettings()
        {
            if (_settings == null)
            {
                _settings = Load();
            }
            return _settings;
        }
        #endregion
    }
}
