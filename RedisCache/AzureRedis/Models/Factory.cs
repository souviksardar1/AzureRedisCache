namespace AzureRedis.Models
{
    public class Factory
    {
        public int MachineId { get; set; }
        public string Name { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string DeployedLocation { get; set; }
    }
}
