namespace Entities.Common
{
    public abstract class Entity : AccessControl
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual void EnableEntity()
        {
            CreatedAt = DateTime.Now;
            Active = true;
        }

        public virtual void UpdateEntity()
        {
            UpdatedAt = DateTime.Now;
            Active = true;
        }

        public virtual void DisableEntity()
        {
            Active = false;
        }
    }
    public abstract class AccessControl
    {
        public int AccessCount { get; set; }
        public int AccessUserId { get; set; }
        public DateTime LastAccess { get; set; }

        public virtual void AccessEntity()
        {
            LastAccess = DateTime.Now;
            AccessCount++;
        }
    }
}
