namespace Client.Models
{
    public abstract class Model<TId>
    {
        public TId Id { get; set; }
    }
}