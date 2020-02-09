namespace Core.Data.Entity
{
    /// <summary>
    /// Okul nesnelerimizin miraz alacağı ana sınıfımız.
    /// Tüm nesnelerimizin ortak özelliği olan Id özelliğini barındırır.
    /// </summary>
    public class BaseEntity
    {
        public int Id { get; set; }
    }
}
