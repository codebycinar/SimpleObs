using System.Collections.Generic;

namespace Core.Data.Entity
{
    public class Grade : BaseEntity
    {
        public int Class { get; set; }
        public string Section { get; set; }
        public ICollection<Student> Students { get; set; }

        public override string ToString()
        {
            return Class + Section;
        }
    }
}
