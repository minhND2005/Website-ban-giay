using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DuAn1DbContext : DbContext
    {
        public DuAn1DbContext()
        {
        }
        public DuAn1DbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ham  nay co nhieu cong dung nhung muon fix cuong thi fix o day
            modelBuilder.Entity<Account>().HasData(
                new Account { IdAcc = 1, UserName = "Admin", Password = "123", Sdt = "0943921328" ,Email = "A@gmail.com", ConfirmPassword = "123"},
                new Account { IdAcc  = 2, UserName = "NhanVien1", Password = "123", Sdt = "0987654321", Email = "NV1@gmail.com", ConfirmPassword = "123" }
                
            );

        }
        

    }
}
