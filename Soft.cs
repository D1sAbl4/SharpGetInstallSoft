namespace SharpGetInstallSoft
{
    class Soft
    {

        private string icon;

        private string name;

        private string version;

        private string installLocation;

        private string installDate;

        private string installSource;

        private string publisher;

        public string Icon { get => icon; set => icon = value; }
        public string Name { get => name; set => name = value; }
        public string Version { get => version; set => version = value; }
        public string InstallLocation { get => installLocation; set => installLocation = value; }
        public string InstallDate { get => installDate; set => installDate = value; }
        public string InstallSource { get => installSource; set => installSource = value; }
        public string Publisher { get => publisher; set => publisher = value; }
    }
}
