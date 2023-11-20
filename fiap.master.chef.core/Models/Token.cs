namespace fiap.master.chef.core.Models
{
    public  class Token
    {
        public int Id { get; set; }
        public string Refresh {  get; set; }
        public string UserApi {  get; set; }
        public bool Used { get; set; }
        public bool Revoked { get; set; }
        public DateTime DtCreated { get; set; }
    }
}
