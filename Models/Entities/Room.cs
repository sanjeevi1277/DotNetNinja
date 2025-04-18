namespace Models.Entities
{
    public class Room
    {
        public string RoomID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int RoomNo { get; set; }
        public int RoomType { get; set; }
        public string Availability { get; set; }
        public string RoomCapacity { get; set; }
        public DateTime Created { get; set; }
    }
}
