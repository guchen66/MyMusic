namespace Music.Shared.Entitys
{
    [SugarTable("LoginInputInfo")]
    public class LoginInputInfo //: AutoIncrementEntity
    {
        [SugarColumn(IsNullable = true, Length = 200)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime LoginTime { get; set; }
    }
}